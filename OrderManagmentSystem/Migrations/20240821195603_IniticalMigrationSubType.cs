using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class IniticalMigrationSubType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Type",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Subscriptions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "704933b7-8bf5-4f2e-a9ae-f5646ce8a194", null, "Supplier", "SUPPLIER" },
                    { "9f2d1c8c-a0b7-4142-abf1-a5b97d2c91fc", null, "Admin", "ADMIN" },
                    { "d0449676-2b5e-4c95-96ee-496bc0394d7c", null, "Retailer", "RETAILER" }
                });
        }
    }
}
