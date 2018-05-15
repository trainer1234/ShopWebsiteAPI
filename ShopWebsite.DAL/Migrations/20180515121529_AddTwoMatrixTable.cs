using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShopWebsite.DAL.Migrations
{
    public partial class AddTwoMatrixTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemLatentFactorMatrix",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CellValue = table.Column<double>(type: "float", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    UserItemPredictId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLatentFactorMatrix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemLatentFactorMatrix_UserItemPredict_UserItemPredictId",
                        column: x => x.UserItemPredictId,
                        principalTable: "UserItemPredict",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLatentFactorMatrix",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CellValue = table.Column<double>(type: "float", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    UserItemPredictId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLatentFactorMatrix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLatentFactorMatrix_UserItemPredict_UserItemPredictId",
                        column: x => x.UserItemPredictId,
                        principalTable: "UserItemPredict",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemLatentFactorMatrix_UserItemPredictId",
                table: "ItemLatentFactorMatrix",
                column: "UserItemPredictId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLatentFactorMatrix_UserItemPredictId",
                table: "UserLatentFactorMatrix",
                column: "UserItemPredictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemLatentFactorMatrix");

            migrationBuilder.DropTable(
                name: "UserLatentFactorMatrix");
        }
    }
}
