using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class son : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "UserTable",
                newName: "LogoUrl");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "ProductTable",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "CategoryTable",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoUrl",
                table: "UserTable",
                newName: "Logo");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ProductTable",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "CategoryTable",
                newName: "Image");
        }
    }
}
