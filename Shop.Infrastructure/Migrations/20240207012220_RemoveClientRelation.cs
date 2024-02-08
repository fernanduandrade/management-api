using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveClientRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sales_history_clients_client_fk",
                table: "sales_history");

            migrationBuilder.DropIndex(
                name: "IX_sales_history_client_fk",
                table: "sales_history");

            migrationBuilder.DropColumn(
                name: "client_fk",
                table: "sales_history");

            migrationBuilder.RenameColumn(
                name: "ate",
                table: "sales_history",
                newName: "date");

            migrationBuilder.AddColumn<string>(
                name: "client_name",
                table: "sales_history",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_name",
                table: "sales_history");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "sales_history",
                newName: "ate");

            migrationBuilder.AddColumn<Guid>(
                name: "client_fk",
                table: "sales_history",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_sales_history_client_fk",
                table: "sales_history",
                column: "client_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_history_clients_client_fk",
                table: "sales_history",
                column: "client_fk",
                principalTable: "clients",
                principalColumn: "id");
        }
    }
}
