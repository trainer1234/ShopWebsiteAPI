using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShopWebsite.DAL.Migrations
{
    public partial class RemoveSlideImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlideImage");

            migrationBuilder.AddColumn<string>(
                name: "ImageModelId",
                table: "Slide",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slide_ImageModelId",
                table: "Slide",
                column: "ImageModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slide_ImageModels_ImageModelId",
                table: "Slide",
                column: "ImageModelId",
                principalTable: "ImageModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slide_ImageModels_ImageModelId",
                table: "Slide");

            migrationBuilder.DropIndex(
                name: "IX_Slide_ImageModelId",
                table: "Slide");

            migrationBuilder.DropColumn(
                name: "ImageModelId",
                table: "Slide");

            migrationBuilder.CreateTable(
                name: "SlideImage",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ImageModelId = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    SlideId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlideImage_ImageModels_ImageModelId",
                        column: x => x.ImageModelId,
                        principalTable: "ImageModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SlideImage_Slide_SlideId",
                        column: x => x.SlideId,
                        principalTable: "Slide",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlideImage_ImageModelId",
                table: "SlideImage",
                column: "ImageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SlideImage_SlideId",
                table: "SlideImage",
                column: "SlideId");
        }
    }
}
