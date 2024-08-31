using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class fixOrderAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<float>(
                name: "Total",
                table: "OrderItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Total",
                table: "OrderItems");

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
    }
}
