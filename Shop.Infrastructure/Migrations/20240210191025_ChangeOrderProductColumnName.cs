using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderProductColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_products_orders_order_id",
                table: "order_products");

            migrationBuilder.DropForeignKey(
                name: "FK_order_products_products_ProductId",
                table: "order_products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "order_products",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_products_ProductId",
                table: "order_products",
                newName: "IX_order_products_product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_products_orders_product_id",
                table: "order_products",
                column: "product_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_products_products_order_id",
                table: "order_products",
                column: "order_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_products_orders_product_id",
                table: "order_products");

            migrationBuilder.DropForeignKey(
                name: "FK_order_products_products_order_id",
                table: "order_products");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "order_products",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_order_products_product_id",
                table: "order_products",
                newName: "IX_order_products_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_products_orders_order_id",
                table: "order_products",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_products_products_ProductId",
                table: "order_products",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
