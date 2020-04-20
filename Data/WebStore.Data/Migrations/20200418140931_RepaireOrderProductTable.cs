using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Data.Migrations
{
    public partial class RepaireOrderProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductItems_OrderProductItems_OrderProductItemOrderId_OrderProductItemProductItemId",
                table: "OrderProductItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderProductItems_OrderProductItemOrderId_OrderProductItemProductItemId",
                table: "OrderProductItems");

            migrationBuilder.DropColumn(
                name: "OrderProductItemOrderId",
                table: "OrderProductItems");

            migrationBuilder.DropColumn(
                name: "OrderProductItemProductItemId",
                table: "OrderProductItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderProductItemOrderId",
                table: "OrderProductItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderProductItemProductItemId",
                table: "OrderProductItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductItems_OrderProductItemOrderId_OrderProductItemProductItemId",
                table: "OrderProductItems",
                columns: new[] { "OrderProductItemOrderId", "OrderProductItemProductItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductItems_OrderProductItems_OrderProductItemOrderId_OrderProductItemProductItemId",
                table: "OrderProductItems",
                columns: new[] { "OrderProductItemOrderId", "OrderProductItemProductItemId" },
                principalTable: "OrderProductItems",
                principalColumns: new[] { "OrderId", "ProductItemId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
