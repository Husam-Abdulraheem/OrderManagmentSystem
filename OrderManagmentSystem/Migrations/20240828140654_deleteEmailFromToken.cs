using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class deleteEmailFromToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2731d248-d386-49c1-8ef8-449350f83544");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35d8e4c7-a6b0-4379-992e-0694aaacd177");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1873dc3-9401-4800-a109-e79d79847fd4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f449e39-11ae-412f-9a8a-07e989478c3e", null, "Supplier", "SUPPLIER" },
                    { "2f059022-5663-4d9b-9d59-a114ce092679", null, "Retailer", "RETAILER" },
                    { "ce0ba815-43cb-4b8d-84a3-f9d2c6dfd401", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f449e39-11ae-412f-9a8a-07e989478c3e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f059022-5663-4d9b-9d59-a114ce092679");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce0ba815-43cb-4b8d-84a3-f9d2c6dfd401");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2731d248-d386-49c1-8ef8-449350f83544", null, "Admin", "ADMIN" },
                    { "35d8e4c7-a6b0-4379-992e-0694aaacd177", null, "Supplier", "SUPPLIER" },
                    { "e1873dc3-9401-4800-a109-e79d79847fd4", null, "Retailer", "RETAILER" }
                });
        }
    }
}
