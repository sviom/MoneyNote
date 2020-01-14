using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyNoteAPI.Migrations
{
    public partial class UserRelationAdd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_User_UserId",
                table: "MoneyItems");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItem_User_Relation",
                table: "MoneyItems",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItem_User_Relation",
                table: "MoneyItems");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItems_User_UserId",
                table: "MoneyItems",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
