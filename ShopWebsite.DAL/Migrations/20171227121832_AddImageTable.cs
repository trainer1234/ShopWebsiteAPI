using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShopWebsite.DAL.Migrations
{
    public partial class AddImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ProductImage",
                newName: "ImageModelId");

            migrationBuilder.AlterColumn<string>(
                name: "ImageModelId",
                table: "ProductImage",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ImageModels",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Extension = table.Column<string>(nullable: true),
                    Height = table.Column<int>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ImageModelId",
                table: "ProductImage",
                column: "ImageModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_ImageModels_ImageModelId",
                table: "ProductImage",
                column: "ImageModelId",
                principalTable: "ImageModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_ImageModels_ImageModelId",
                table: "ProductImage");

            migrationBuilder.DropTable(
                name: "ImageModels");

            migrationBuilder.DropIndex(
                name: "IX_ProductImage_ImageModelId",
                table: "ProductImage");

            migrationBuilder.RenameColumn(
                name: "ImageModelId",
                table: "ProductImage",
                newName: "ImageUrl");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "ProductImage",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
