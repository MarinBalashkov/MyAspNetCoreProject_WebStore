using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Data.Migrations
{
    public partial class RenameRequestsToUsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GetRequestsToUs",
                table: "GetRequestsToUs");

            migrationBuilder.RenameTable(
                name: "GetRequestsToUs",
                newName: "RequestsToUs");

            migrationBuilder.RenameIndex(
                name: "IX_GetRequestsToUs_IsDeleted",
                table: "RequestsToUs",
                newName: "IX_RequestsToUs_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestsToUs",
                table: "RequestsToUs",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestsToUs",
                table: "RequestsToUs");

            migrationBuilder.RenameTable(
                name: "RequestsToUs",
                newName: "GetRequestsToUs");

            migrationBuilder.RenameIndex(
                name: "IX_RequestsToUs_IsDeleted",
                table: "GetRequestsToUs",
                newName: "IX_GetRequestsToUs_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetRequestsToUs",
                table: "GetRequestsToUs",
                column: "Id");
        }
    }
}
