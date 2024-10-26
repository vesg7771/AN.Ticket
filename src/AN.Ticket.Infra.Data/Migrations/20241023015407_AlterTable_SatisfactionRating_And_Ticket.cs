using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AN.Ticket.Infra.Data.Migrations;

/// <inheritdoc />
public partial class AlterTable_SatisfactionRating_And_Ticket : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_SatisfactionRatings_Tickets_TicketId",
            table: "SatisfactionRatings");

        migrationBuilder.DropPrimaryKey(
            name: "PK_SatisfactionRatings",
            table: "SatisfactionRatings");

        migrationBuilder.AlterColumn<int>(
            name: "Rating",
            table: "SatisfactionRatings",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Comment",
            table: "SatisfactionRatings",
            type: "varchar(1000)",
            maxLength: 1000,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "longtext",
            oldNullable: true)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AddPrimaryKey(
            name: "PK_SatisfactionRatings",
            table: "SatisfactionRatings",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_SatisfactionRatings_TicketId",
            table: "SatisfactionRatings",
            column: "TicketId",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_SatisfactionRatings_Tickets_TicketId",
            table: "SatisfactionRatings",
            column: "TicketId",
            principalTable: "Tickets",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_SatisfactionRatings_Tickets_TicketId",
            table: "SatisfactionRatings");

        migrationBuilder.DropPrimaryKey(
            name: "PK_SatisfactionRatings",
            table: "SatisfactionRatings");

        migrationBuilder.DropIndex(
            name: "IX_SatisfactionRatings_TicketId",
            table: "SatisfactionRatings");

        migrationBuilder.AlterColumn<int>(
            name: "Rating",
            table: "SatisfactionRatings",
            type: "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AlterColumn<string>(
            name: "Comment",
            table: "SatisfactionRatings",
            type: "longtext",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "varchar(1000)",
            oldMaxLength: 1000,
            oldNullable: true)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AddPrimaryKey(
            name: "PK_SatisfactionRatings",
            table: "SatisfactionRatings",
            column: "TicketId");
    }
}
