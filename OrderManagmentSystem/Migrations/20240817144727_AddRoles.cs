using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39366ad4-dd03-4b14-a856-10091c847d80", null, "Retailer", "RETAILER" },
                    { "b072b06a-ece6-4f96-a2c0-1b17861de828", null, "Supplier", "SUPPLIER" },
                    { "c92f59c2-887b-4d9e-9e6f-0055978ff6b0", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39366ad4-dd03-4b14-a856-10091c847d80");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b072b06a-ece6-4f96-a2c0-1b17861de828");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c92f59c2-887b-4d9e-9e6f-0055978ff6b0");
        }
    }
}
