﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ShopWebsite.Common.Models.Enums;
using ShopWebsite.DAL.Context;
using System;

namespace ShopWebsite.DAL.Migrations
{
    [DbContext(typeof(ShopWebsiteSqlContext))]
    [Migration("20171231082543_AddProductOrderModels")]
    partial class AddProductOrderModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.AccountModels.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AuthToken");

                    b.Property<string>("AvatarUrl");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDisabled");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.CustomerModels.Customer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ImageModels.ImageModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Extension");

                    b.Property<int>("Height");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.Property<int>("Width");

                    b.HasKey("Id");

                    b.ToTable("ImageModels");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.LogModels.ErrorLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<bool>("IsDisabled");

                    b.HasKey("Id");

                    b.ToTable("ErrorLog");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ManufactureModels.Manufacture", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Manufacture");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ManufactureModels.ManufactureType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("ManufactureId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ManufactureId");

                    b.ToTable("ManufactureType");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ProductModels.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("ManufactureId");

                    b.Property<int>("ManufactureYear");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<int>("ProductSpecificType");

                    b.Property<bool>("PromotionAvailable");

                    b.Property<double>("PromotionRate");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ManufactureId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ProductModels.ProductImage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageModelId");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ImageModelId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ProductModels.ProductProperty", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("ProductId");

                    b.Property<string>("PropertyDetail");

                    b.Property<string>("PropertyId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("PropertyId");

                    b.ToTable("ProductProperty");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ProductOrderModels.ProductMapOrderDetail", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("ProductId");

                    b.Property<string>("ProductOrderDetailId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductOrderDetailId");

                    b.ToTable("ProductMapOrderDetail");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ProductOrderModels.ProductOrder", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDisabled");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("OrderId");

                    b.Property<int>("OrderStatus");

                    b.Property<int>("ProductAmount");

                    b.Property<double>("TotalCost");

                    b.HasKey("Id");

                    b.ToTable("ProductOrder");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ProductOrderModels.ProductOrderDetail", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerId");

                    b.Property<bool>("IsDisabled");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("OrderId");

                    b.Property<string>("ProductMapOrderDetailId");

                    b.Property<string>("ProductOrderId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductOrderId");

                    b.ToTable("ProductOrderDetail");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.PropertyModels.Property", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Property");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ShopWebsite.DAL.Models.AccountModels.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ShopWebsite.DAL.Models.AccountModels.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShopWebsite.DAL.Models.AccountModels.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ShopWebsite.DAL.Models.AccountModels.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ManufactureModels.ManufactureType", b =>
                {
                    b.HasOne("ShopWebsite.DAL.Models.ManufactureModels.Manufacture", "Manufacture")
                        .WithMany("ManufactureTypes")
                        .HasForeignKey("ManufactureId");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ProductModels.Product", b =>
                {
                    b.HasOne("ShopWebsite.DAL.Models.ManufactureModels.Manufacture", "Manufacture")
                        .WithMany()
                        .HasForeignKey("ManufactureId");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ProductModels.ProductImage", b =>
                {
                    b.HasOne("ShopWebsite.DAL.Models.ImageModels.ImageModel", "ImageModel")
                        .WithMany()
                        .HasForeignKey("ImageModelId");

                    b.HasOne("ShopWebsite.DAL.Models.ProductModels.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ProductModels.ProductProperty", b =>
                {
                    b.HasOne("ShopWebsite.DAL.Models.ProductModels.Product", "Product")
                        .WithMany("ProductProperties")
                        .HasForeignKey("ProductId");

                    b.HasOne("ShopWebsite.DAL.Models.PropertyModels.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ProductOrderModels.ProductMapOrderDetail", b =>
                {
                    b.HasOne("ShopWebsite.DAL.Models.ProductModels.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("ShopWebsite.DAL.Models.ProductOrderModels.ProductOrderDetail", "ProductOrderDetail")
                        .WithMany("ProductMapOrderDetails")
                        .HasForeignKey("ProductOrderDetailId");
                });

            modelBuilder.Entity("ShopWebsite.DAL.Models.ProductOrderModels.ProductOrderDetail", b =>
                {
                    b.HasOne("ShopWebsite.DAL.Models.CustomerModels.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("ShopWebsite.DAL.Models.ProductOrderModels.ProductOrder", "ProductOrder")
                        .WithMany("ProductOrderDetails")
                        .HasForeignKey("ProductOrderId");
                });
#pragma warning restore 612, 618
        }
    }
}
