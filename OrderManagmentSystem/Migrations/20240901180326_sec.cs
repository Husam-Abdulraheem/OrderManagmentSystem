using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class sec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01c53e74-2919-466b-9a07-c6ab603ff31c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c43c416-4011-4f31-8073-a1b99ddd6a31");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c67c165c-01d0-46e1-88b8-3d81780e7d5b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0df76dfd-9f61-4b60-8d29-133384e7f817", null, "Supplier", "SUPPLIER" },
                    { "1c428db9-0c25-490c-9349-487f61ecbc08", null, "Retailer", "RETAILER" },
                    { "73a0da53-6b80-449a-af01-5477aa3260e2", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0df76dfd-9f61-4b60-8d29-133384e7f817");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c428db9-0c25-490c-9349-487f61ecbc08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73a0da53-6b80-449a-af01-5477aa3260e2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01c53e74-2919-466b-9a07-c6ab603ff31c", null, "Admin", "ADMIN" },
                    { "5c43c416-4011-4f31-8073-a1b99ddd6a31", null, "Supplier", "SUPPLIER" },
                    { "c67c165c-01d0-46e1-88b8-3d81780e7d5b", null, "Retailer", "RETAILER" }
                });
        }
    }
}
