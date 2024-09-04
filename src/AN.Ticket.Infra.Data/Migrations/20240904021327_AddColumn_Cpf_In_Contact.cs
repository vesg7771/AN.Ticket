using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AN.Ticket.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumn_Cpf_In_Contact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Contacts",
                type: "varchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Contacts");
        }
    }
}
