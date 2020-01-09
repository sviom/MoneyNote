using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyNoteAPI.Migrations
{
    public partial class MoneyDivisionAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Division",
                table: "MoneyItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Division",
                table: "MoneyItems");
        }
    }
}
