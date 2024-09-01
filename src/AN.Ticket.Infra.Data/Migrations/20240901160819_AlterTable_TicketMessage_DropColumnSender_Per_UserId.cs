using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AN.Ticket.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTable_TicketMessage_DropColumnSender_Per_UserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sender",
                table: "TicketMessage");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TicketMessage",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessage_UserId",
                table: "TicketMessage",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessage_Users_UserId",
                table: "TicketMessage",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessage_Users_UserId",
                table: "TicketMessage");

            migrationBuilder.DropIndex(
                name: "IX_TicketMessage_UserId",
                table: "TicketMessage");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TicketMessage");

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "TicketMessage",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
