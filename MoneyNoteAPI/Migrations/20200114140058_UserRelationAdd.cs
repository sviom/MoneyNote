using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyNoteAPI.Migrations
{
    public partial class UserRelationAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "MoneyItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyItems_UserId",
                table: "MoneyItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItems_User_UserId",
                table: "MoneyItems",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_User_UserId",
                table: "MoneyItems");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_MoneyItems_UserId",
                table: "MoneyItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MoneyItems");
        }
    }
}
