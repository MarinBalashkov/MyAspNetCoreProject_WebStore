using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Data.Migrations
{
    public partial class changeSomeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "OrderProductItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "OrderProductItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderProductItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderProductItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "OrderProductItems",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FavoritesProducts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CategoriesProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "CategoriesProducts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CategoriesProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CategoriesProducts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "CategoriesProducts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductItems_IsDeleted",
                table: "OrderProductItems",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesProducts_IsDeleted",
                table: "CategoriesProducts",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderProductItems_IsDeleted",
                table: "OrderProductItems");

            migrationBuilder.DropIndex(
                name: "IX_CategoriesProducts_IsDeleted",
                table: "CategoriesProducts");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OrderProductItems");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "OrderProductItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderProductItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderProductItems");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "OrderProductItems");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CategoriesProducts");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CategoriesProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CategoriesProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CategoriesProducts");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CategoriesProducts");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FavoritesProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
