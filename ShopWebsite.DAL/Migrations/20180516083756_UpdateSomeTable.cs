using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShopWebsite.DAL.Migrations
{
    public partial class UpdateSomeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLatentFactorMatrix_UserItemPredict_UserItemPredictId",
                table: "ItemLatentFactorMatrix");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLatentFactorMatrix_UserItemPredict_UserItemPredictId",
                table: "UserLatentFactorMatrix");

            migrationBuilder.DropIndex(
                name: "IX_UserLatentFactorMatrix_UserItemPredictId",
                table: "UserLatentFactorMatrix");

            migrationBuilder.DropIndex(
                name: "IX_ItemLatentFactorMatrix_UserItemPredictId",
                table: "ItemLatentFactorMatrix");

            migrationBuilder.DropColumn(
                name: "UserItemPredictId",
                table: "UserLatentFactorMatrix");

            migrationBuilder.DropColumn(
                name: "UserItemPredictId",
                table: "ItemLatentFactorMatrix");

            migrationBuilder.AddColumn<long>(
                name: "PurchaseCounter",
                table: "Product",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "View",
                table: "Product",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseCounter",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "View",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "UserItemPredictId",
                table: "UserLatentFactorMatrix",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserItemPredictId",
                table: "ItemLatentFactorMatrix",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLatentFactorMatrix_UserItemPredictId",
                table: "UserLatentFactorMatrix",
                column: "UserItemPredictId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLatentFactorMatrix_UserItemPredictId",
                table: "ItemLatentFactorMatrix",
                column: "UserItemPredictId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLatentFactorMatrix_UserItemPredict_UserItemPredictId",
                table: "ItemLatentFactorMatrix",
                column: "UserItemPredictId",
                principalTable: "UserItemPredict",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLatentFactorMatrix_UserItemPredict_UserItemPredictId",
                table: "UserLatentFactorMatrix",
                column: "UserItemPredictId",
                principalTable: "UserItemPredict",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
