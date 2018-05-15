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

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Manufacture> Manufactures { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ImageModel> ImageModels { get; set; }
        public virtual DbSet<ManufactureType> ManufactureTypes { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<ProductProperty> ProductProperties { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ProductOrder> ProductOrders { get; set; }
        public virtual DbSet<ProductMapOrderDetail> ProductMapOrderDetails { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<CustomerRating> CustomerRatings { get; set; }
        public virtual DbSet<UserItemPredict> UserItemPredicts { get; set; }
        public virtual DbSet<UserHobby> UserHobbies { get; set; }
        public virtual DbSet<UserLatentFactorMatrix> UserLatentFactorMatrices { get; set; }
        public virtual DbSet<ItemLatentFactorMatrix> ItemLatentFactorMatrices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=163.22.17.212;Initial Catalog=ShopWebsite;Integrated Security=False;User ID=sa;Password=abcd@1234;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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
            builder.Entity<UserLatentFactorMatrix>()
               .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("UserLatentFactorMatrix");
            builder.Entity<ItemLatentFactorMatrix>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("ItemLatentFactorMatrix");
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
