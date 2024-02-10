using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "orders",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "orders",
                newName: "order_status");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "orders",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "orders",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "orders",
                newName: "created_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "created",
                table: "orders",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "order_status",
                table: "orders",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                table: "orders",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "orders",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "orders",
                newName: "CreatedBy");
        }
    }
}
