using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.BLL.Implementations;
using ShopWebsite.Common.Models.ServerOptions;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Data;
using ShopWebsite.DAL.Implementations;
using ShopWebsite.DAL.Models.AccountModels;

namespace ShopWebsiteServerSide
{
    public class Startup
    {
        public IConfiguration _config { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _config = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _config = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            ConnectionStringOption.ConnectionString = _config.GetConnectionString("DefaultConnection");
            ImageDirectoryOption.Original = _config.GetSection("ImageDirectory").GetValue(typeof(string), "Original").ToString();
            ImageUrlOption.Original = _config.GetSection("ImageUrl").GetValue(typeof(string), "Original").ToString();
            PaypalAuthOption.PayPalClientId = _config.GetValue<string>("PaypalClientId");
            PaypalAuthOption.PayPalClientSecret = _config.GetValue<string>("PaypalClientSecret");

            services.AddSingleton(_config);

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IManufactureService, ManufactureService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IProductOrderService, ProductOrderService>();
            services.AddTransient<ISlideService, SlideService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<IRecommenderService, RecommenderService>();
            services.AddTransient<IPaypalService, PaypalService>();

            services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IManufactureRepository, ManufactureRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IProductPropertyRepository, ProductPropertyRepository>();
            services.AddScoped<IManufactureTypeRepository, ManufactureTypeRepository>();
            services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
            services.AddScoped<IProductMapOrderDetailRepository, ProductMapOrderDetailRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISlideRepository, SlideRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            services.AddEntityFrameworkSqlServer().AddDbContext<ShopWebsiteSqlContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ShopWebsiteSqlContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserManager<User>>();

            services.Configure<IdentityOptions>(options =>
            {
                // password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            // => remove default claims
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = _config["Tokens:Issuer"],
                    ValidAudience = _config["Tokens:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ShopWebsiteSqlContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseAuthentication();

            app.UseMvc();
            DbInitializer.Initialize(context);
        }
    }
}
