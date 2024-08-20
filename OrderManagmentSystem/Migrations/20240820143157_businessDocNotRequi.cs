using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class businessDocNotRequi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "BusinessDocument",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "59d21fe9-26f2-465c-b772-cdd45013df01", null, "Retailer", "RETAILER" },
                    { "799bf4b8-0bbd-4208-af55-88370e852a28", null, "Admin", "ADMIN" },
                    { "ec034287-285f-4cd1-bfd6-8260014ce570", null, "Supplier", "SUPPLIER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59d21fe9-26f2-465c-b772-cdd45013df01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "799bf4b8-0bbd-4208-af55-88370e852a28");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec034287-285f-4cd1-bfd6-8260014ce570");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessDocument",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
