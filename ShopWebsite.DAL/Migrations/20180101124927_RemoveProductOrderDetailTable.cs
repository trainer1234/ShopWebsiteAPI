using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShopWebsite.DAL.Migrations
{
    public partial class RemoveProductOrderDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMapOrderDetail_ProductOrderDetail_ProductOrderDetailId",
                table: "ProductMapOrderDetail");

            migrationBuilder.DropTable(
                name: "ProductOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProductMapOrderDetail_ProductOrderDetailId",
                table: "ProductMapOrderDetail");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "ProductOrder",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductOrderDetailId",
                table: "ProductMapOrderDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductOrderId",
                table: "ProductMapOrderDetail",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Remain",
                table: "Product",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_CustomerId",
                table: "ProductOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMapOrderDetail_ProductOrderId",
                table: "ProductMapOrderDetail",
                column: "ProductOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMapOrderDetail_ProductOrder_ProductOrderId",
                table: "ProductMapOrderDetail",
                column: "ProductOrderId",
                principalTable: "ProductOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Customer_CustomerId",
                table: "ProductOrder",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMapOrderDetail_ProductOrder_ProductOrderId",
                table: "ProductMapOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Customer_CustomerId",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrder_CustomerId",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductMapOrderDetail_ProductOrderId",
                table: "ProductMapOrderDetail");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ProductOrder");

            migrationBuilder.DropColumn(
                name: "ProductOrderId",
                table: "ProductMapOrderDetail");

            migrationBuilder.DropColumn(
                name: "Remain",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "ProductOrderDetailId",
                table: "ProductMapOrderDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMapOrderDetail_ProductOrderDetail_ProductOrderDetailId",
                table: "ProductMapOrderDetail",
                column: "ProductOrderDetailId",
                principalTable: "ProductOrderDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
