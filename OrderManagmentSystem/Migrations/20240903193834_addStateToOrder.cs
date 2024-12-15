using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class addStateToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c2d0579-99e6-4b4d-abf8-6e5add860cdc", null, "Admin", "ADMIN" },
                    { "628d9106-5628-40d2-8570-664998d96787", null, "Supplier", "SUPPLIER" },
                    { "f3c0bd98-2bef-4e14-94c9-ee7f095b4ca5", null, "Retailer", "RETAILER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c2d0579-99e6-4b4d-abf8-6e5add860cdc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "628d9106-5628-40d2-8570-664998d96787");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3c0bd98-2bef-4e14-94c9-ee7f095b4ca5");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Orders");

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
    }
}
