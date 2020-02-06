using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyNoteAPI.Migrations
{
    public partial class MoneyItmeCategoryFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_MainCategories_MainCategoryId",
                table: "MoneyItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_SubCategories_SubCategoryId",
                table: "MoneyItems");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItems_MainCategories_MainCategoryId",
                table: "MoneyItems",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItems_SubCategories_SubCategoryId",
                table: "MoneyItems",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_MainCategories_MainCategoryId",
                table: "MoneyItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_SubCategories_SubCategoryId",
                table: "MoneyItems");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItems_MainCategories_MainCategoryId",
                table: "MoneyItems",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItems_SubCategories_SubCategoryId",
                table: "MoneyItems",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id");
        }
    }
}
