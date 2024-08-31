using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class total : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e74809e-0011-46cd-844f-7a48128798b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5694785-63bb-41d9-ab22-98e7abc5fb77");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6760256-282c-483a-a703-75dfd3cd7d58");

            migrationBuilder.AddColumn<float>(
                name: "TotalCost",
                table: "Orders",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f5b672c-42e0-445b-ae2e-86ffcc75a929", null, "Admin", "ADMIN" },
                    { "7bc64fc5-0114-43ed-9e14-7a1e2485a203", null, "Retailer", "RETAILER" },
                    { "eeaa5ba5-b66b-4091-96c1-1aa0751de098", null, "Supplier", "SUPPLIER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f5b672c-42e0-445b-ae2e-86ffcc75a929");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bc64fc5-0114-43ed-9e14-7a1e2485a203");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eeaa5ba5-b66b-4091-96c1-1aa0751de098");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e74809e-0011-46cd-844f-7a48128798b9", null, "Admin", "ADMIN" },
                    { "c5694785-63bb-41d9-ab22-98e7abc5fb77", null, "Retailer", "RETAILER" },
                    { "c6760256-282c-483a-a703-75dfd3cd7d58", null, "Supplier", "SUPPLIER" }
                });
        }
    }
}
