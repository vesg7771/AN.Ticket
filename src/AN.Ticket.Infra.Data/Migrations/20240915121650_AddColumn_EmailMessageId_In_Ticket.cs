using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AN.Ticket.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumn_EmailMessageId_In_Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailMessageId",
                table: "Tickets",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailMessageId",
                table: "Tickets");
        }
    }
}
