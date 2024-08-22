using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class deleteBcrypt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12e3e059-57ab-4561-9b89-6d628bc2120c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf062f97-b71b-416c-9b9d-c09d4f9690dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3d10be7-dcfe-4ce2-906d-6e0bc6f26183");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "12e3e059-57ab-4561-9b89-6d628bc2120c", null, "Admin", "ADMIN" },
                    { "bf062f97-b71b-416c-9b9d-c09d4f9690dd", null, "Supplier", "SUPPLIER" },
                    { "d3d10be7-dcfe-4ce2-906d-6e0bc6f26183", null, "Retailer", "RETAILER" }
                });
        }
    }
}
