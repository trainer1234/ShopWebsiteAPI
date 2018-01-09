using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShopWebsite.DAL.Migrations
{
    public partial class AddSlide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slide",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    SlideImageUrl = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slide", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlideImage");

            migrationBuilder.DropTable(
                name: "Slide");
        }
    }
}
