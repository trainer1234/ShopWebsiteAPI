using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShopWebsite.DAL.Migrations
{
    public partial class UpdateRatingPredictionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Customer_CustomerId",
                table: "ProductOrder");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "ProductOrder",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrder_CustomerId",
                table: "ProductOrder",
                newName: "IX_ProductOrder_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "User",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerProductFeedback",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProductFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerProductFeedback_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerProductFeedback_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProductRating",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    PredictedRating = table.Column<double>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    View = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProductRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerProductRating_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerProductRating_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProductFeedback_ProductId",
                table: "CustomerProductFeedback",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProductFeedback_UserId",
                table: "CustomerProductFeedback",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProductRating_ProductId",
                table: "CustomerProductRating",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProductRating_UserId",
                table: "CustomerProductRating",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_User_UserId",
                table: "ProductOrder",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_User_UserId",
                table: "ProductOrder");

            migrationBuilder.DropTable(
                name: "CustomerProductFeedback");

            migrationBuilder.DropTable(
                name: "CustomerProductRating");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProductOrder",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrder_UserId",
                table: "ProductOrder",
                newName: "IX_ProductOrder_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Customer_CustomerId",
                table: "ProductOrder",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
