using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class deletetransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Transactions_IdTransaction",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Items_IdTransaction",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IdTransaction",
                table: "Items");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "IdTransaction",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    IdTransaction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.IdTransaction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_IdTransaction",
                table: "Items",
                column: "IdTransaction");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Transactions_IdTransaction",
                table: "Items",
                column: "IdTransaction",
                principalTable: "Transactions",
                principalColumn: "IdTransaction",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
