using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyNoteLibrary5.Migrations
{
    public partial class removeBankBookRequired2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_MoneyItems_BankBooks_BankBookId", "MoneyItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
