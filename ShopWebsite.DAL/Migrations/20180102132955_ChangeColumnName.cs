using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShopWebsite.DAL.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductAmount",
                table: "ProductOrder");

            migrationBuilder.AlterColumn<long>(
                name: "TotalCost",
                table: "ProductOrder",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<long>(
                name: "ProductTotalAmount",
                table: "ProductOrder",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductAmount",
                table: "ProductMapOrderDetail",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "Price",
                table: "Product",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductTotalAmount",
                table: "ProductOrder");

            migrationBuilder.DropColumn(
                name: "ProductAmount",
                table: "ProductMapOrderDetail");

            migrationBuilder.AlterColumn<double>(
                name: "TotalCost",
                table: "ProductOrder",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "ProductAmount",
                table: "ProductOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Product",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
