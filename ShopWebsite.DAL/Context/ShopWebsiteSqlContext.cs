using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopWebsite.Common.Models.ServerOptions;
using ShopWebsite.DAL.Models.AccountModels;
using ShopWebsite.DAL.Models.LogModels;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;
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
        public DbSet<ProductSpecificType> ProductSpecificTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(ConnectionStringOption.ConnectionString);
            optionsBuilder.UseSqlServer("Server=.;Database=ShopWebsite;Trusted_Connection=True;MultipleActiveResultSets=true");
            //optionsBuilder.UseNpgsql("Server=163.22.17.198;Port=5432;Database=EED;Username=ncnuee;Password=cn4101");
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EED;Trusted_Connection=True;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer("Data Source=163.22.17.198;Initial Catalog=EED;Integrated Security=False;User ID=LoveTripDatabaseManager;Password=abc@123;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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
            builder.Entity<ProductSpecificType>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("ProductSpecificType");
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
