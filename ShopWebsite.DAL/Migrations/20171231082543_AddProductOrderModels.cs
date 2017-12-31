using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShopWebsite.DAL.Migrations
{
    public partial class AddProductOrderModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrder",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<int>(nullable: false),
                    ProductAmount = table.Column<int>(nullable: false),
                    TotalCost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrderDetail",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    ProductMapOrderDetailId = table.Column<string>(nullable: true),
                    ProductOrderId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrderDetail_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOrderDetail_ProductOrder_ProductOrderId",
                        column: x => x.ProductOrderId,
                        principalTable: "ProductOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductMapOrderDetail",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    ProductOrderDetailId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMapOrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMapOrderDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductMapOrderDetail_ProductOrderDetail_ProductOrderDetailId",
                        column: x => x.ProductOrderDetailId,
                        principalTable: "ProductOrderDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductMapOrderDetail_ProductId",
                table: "ProductMapOrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMapOrderDetail_ProductOrderDetailId",
                table: "ProductMapOrderDetail",
                column: "ProductOrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDetail_CustomerId",
                table: "ProductOrderDetail",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDetail_ProductOrderId",
                table: "ProductOrderDetail",
                column: "ProductOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductMapOrderDetail");

            migrationBuilder.DropTable(
                name: "ProductOrderDetail");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "ProductOrder");
        }
    }
}
