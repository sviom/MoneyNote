using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyNoteLibrary5.Migrations
{
    public partial class removeBankBookRequired7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_BankBooks_BankBookId",
                table: "MoneyItems");

            migrationBuilder.AddColumn<Guid>(
                name: "BankBookId1",
                table: "MoneyItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoneyItems_BankBookId1",
                table: "MoneyItems",
                column: "BankBookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItems_BankBooks_BankBookId",
                table: "MoneyItems",
                column: "BankBookId",
                principalTable: "BankBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyItems_BankBooks_BankBookId1",
                table: "MoneyItems",
                column: "BankBookId1",
                principalTable: "BankBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_BankBooks_BankBookId",
                table: "MoneyItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MoneyItems_BankBooks_BankBookId1",
                table: "MoneyItems");

            migrationBuilder.DropIndex(
                name: "IX_MoneyItems_BankBookId1",
                table: "MoneyItems");

            migrationBuilder.DropColumn(
                name: "BankBookId1",
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
