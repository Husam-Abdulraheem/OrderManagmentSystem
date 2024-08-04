using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTable_CategoryTable_CategoryId",
                table: "ProductTable");

            migrationBuilder.DropColumn(
                name: "CategorieId",
                table: "ProductTable");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProductTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTable_CategoryTable_CategoryId",
                table: "ProductTable",
                column: "CategoryId",
                principalTable: "CategoryTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTable_CategoryTable_CategoryId",
                table: "ProductTable");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProductTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategorieId",
                table: "ProductTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTable_CategoryTable_CategoryId",
                table: "ProductTable",
                column: "CategoryId",
                principalTable: "CategoryTable",
                principalColumn: "Id");
        }
    }
}
