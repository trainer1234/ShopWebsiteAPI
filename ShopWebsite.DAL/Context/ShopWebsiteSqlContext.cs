using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopWebsite.DAL.Models.AccountModels;
using ShopWebsite.DAL.Models.CustomerModels;
using ShopWebsite.DAL.Models.ImageModels;
using ShopWebsite.DAL.Models.LogModels;
using ShopWebsite.DAL.Models.ManufactureModels;
using ShopWebsite.DAL.Models.ProductModels;
using ShopWebsite.DAL.Models.ProductOrderModels;
using ShopWebsite.DAL.Models.PropertyModels;
using ShopWebsite.DAL.Models.SlideModels;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Context
{
    public class ShopWebsiteSqlContext : IdentityDbContext<User>
    {
        private IConfiguration Configuration { get; set; }

        public ShopWebsiteSqlContext(DbContextOptions<ShopWebsiteSqlContext> options) : base(options)
        {
        }

        public ShopWebsiteSqlContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ImageModel> ImageModels { get; set; }
        public DbSet<ManufactureType> ManufactureTypes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<ProductMapOrderDetail> ProductMapOrderDetails { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<CustomerRating> CustomerRatings { get; set; }
        public DbSet<UserItemPredict> UserItemPredicts { get; set; }
        public DbSet<UserHobby> UserHobbies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=ShopWebsite;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("User");
            builder.Entity<ErrorLog>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("ErrorLog");
            builder.Entity<Product>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("Product");
            builder.Entity<ProductImage>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("ProductImage");
            builder.Entity<Manufacture>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("Manufacture");
            builder.Entity<ImageModel>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("ImageModels");
            builder.Entity<ManufactureType>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("ManufactureType");
            builder.Entity<Property>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("Property");
            builder.Entity<ProductProperty>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("ProductProperty");
            builder.Entity<Customer>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("Customer");
            builder.Entity<ProductOrder>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("ProductOrder")
                .HasIndex(model => model.OrderId).IsUnique();
            builder.Entity<ProductMapOrderDetail>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("ProductMapOrderDetail");
            builder.Entity<Slide>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("Slide");
            builder.Entity<CustomerRating>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("CustomerRating");
            builder.Entity<UserItemPredict>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("UserItemPredict");
            builder.Entity<UserHobby>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("UserHobby");
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDisabled"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDisabled"] = true;
                        break;
                }
            }
        }
    }
}
