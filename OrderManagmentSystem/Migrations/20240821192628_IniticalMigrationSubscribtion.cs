using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class IniticalMigrationSubscribtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "495bc3b0-8e8a-496d-b412-f6a90790a744");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84283f5b-23d3-43d2-aaea-4363ee91ef5a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b51ad1fc-4859-41de-a8f6-f5a6cd499314");

            migrationBuilder.DropColumn(
                name: "Subscription",
                table: "Suppliers");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "704933b7-8bf5-4f2e-a9ae-f5646ce8a194", null, "Supplier", "SUPPLIER" },
                    { "9f2d1c8c-a0b7-4142-abf1-a5b97d2c91fc", null, "Admin", "ADMIN" },
                    { "d0449676-2b5e-4c95-96ee-496bc0394d7c", null, "Retailer", "RETAILER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SupplierId",
                table: "Subscriptions",
                column: "SupplierId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "704933b7-8bf5-4f2e-a9ae-f5646ce8a194");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f2d1c8c-a0b7-4142-abf1-a5b97d2c91fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0449676-2b5e-4c95-96ee-496bc0394d7c");

            migrationBuilder.AddColumn<string>(
                name: "Subscription",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "495bc3b0-8e8a-496d-b412-f6a90790a744", null, "Admin", "ADMIN" },
                    { "84283f5b-23d3-43d2-aaea-4363ee91ef5a", null, "Supplier", "SUPPLIER" },
                    { "b51ad1fc-4859-41de-a8f6-f5a6cd499314", null, "Retailer", "RETAILER" }
                });
        }
    }
}
