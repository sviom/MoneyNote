using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyNoteLibrary5.Migrations
{
    public partial class removeBankBookRequired5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_BankBooks_BankBookId",
                table: "MoneyItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "BankBookId",
                table: "MoneyItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "BankBookId",
                table: "MoneyItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItems_BankBooks_BankBookId",
                table: "MoneyItems",
                column: "BankBookId",
                principalTable: "BankBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
