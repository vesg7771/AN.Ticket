using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AN.Ticket.Infra.Data.Migrations;

/// <inheritdoc />
public partial class AddTable_TicketMessage_And_AlterTableTicketAndActivity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Description",
            table: "Tickets");

        migrationBuilder.AlterColumn<Guid>(
            name: "TicketId1",
            table: "Activities",
            type: "char(36)",
            nullable: true,
            collation: "ascii_general_ci",
            oldClrType: typeof(Guid),
            oldType: "char(36)")
            .OldAnnotation("Relational:Collation", "ascii_general_ci");

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Activities",
            type: "varchar(500)",
            maxLength: 500,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "varchar(500)",
            oldMaxLength: 500)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AddColumn<Guid>(
            name: "ContactId",
            table: "Activities",
            type: "char(36)",
            nullable: true,
            collation: "ascii_general_ci");

        migrationBuilder.AddColumn<TimeSpan>(
            name: "Duration",
            table: "Activities",
            type: "time(6)",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "Priority",
            table: "Activities",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<string>(
            name: "Subject",
            table: "Activities",
            type: "varchar(200)",
            maxLength: 200,
            nullable: true)
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "TicketMessage",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                TicketId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Message = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Sender = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                SentAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TicketMessage", x => x.Id);
                table.ForeignKey(
                    name: "FK_TicketMessage_Tickets_TicketId",
                    column: x => x.TicketId,
                    principalTable: "Tickets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Activities_ContactId",
            table: "Activities",
            column: "ContactId");

        migrationBuilder.CreateIndex(
            name: "IX_TicketMessage_TicketId",
            table: "TicketMessage",
            column: "TicketId");

        migrationBuilder.AddForeignKey(
            name: "FK_Activities_Contacts_ContactId",
            table: "Activities",
            column: "ContactId",
            principalTable: "Contacts",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Activities_Contacts_ContactId",
            table: "Activities");

        migrationBuilder.DropTable(
            name: "TicketMessage");

        migrationBuilder.DropIndex(
            name: "IX_Activities_ContactId",
            table: "Activities");

        migrationBuilder.DropColumn(
            name: "ContactId",
            table: "Activities");

        migrationBuilder.DropColumn(
            name: "Duration",
            table: "Activities");

        migrationBuilder.DropColumn(
            name: "Priority",
            table: "Activities");

        migrationBuilder.DropColumn(
            name: "Subject",
            table: "Activities");

        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "Tickets",
            type: "longtext",
            nullable: true)
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<Guid>(
            name: "TicketId1",
            table: "Activities",
            type: "char(36)",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
            collation: "ascii_general_ci",
            oldClrType: typeof(Guid),
            oldType: "char(36)",
            oldNullable: true)
            .OldAnnotation("Relational:Collation", "ascii_general_ci");

        migrationBuilder.UpdateData(
            table: "Activities",
            keyColumn: "Description",
            keyValue: null,
            column: "Description",
            value: "");

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Activities",
            type: "varchar(500)",
            maxLength: 500,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(500)",
            oldMaxLength: 500,
            oldNullable: true)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");
    }
}
