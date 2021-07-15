using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyNoteLibrary5.Migrations
{
    public partial class removeBankBookRequired4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_BankBooks_BankBookId",
                table: "MoneyItems");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItems_BankBooks_BankBookId",
                table: "MoneyItems",
                column: "BankBookId",
                principalTable: "BankBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_BankBooks_BankBookId",
                table: "MoneyItems");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItems_BankBooks_BankBookId",
                table: "MoneyItems",
                column: "BankBookId",
                principalTable: "BankBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
