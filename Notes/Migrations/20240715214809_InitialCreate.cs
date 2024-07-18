using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParentNoteId = table.Column<int>(type: "INTEGER", nullable: true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Cancelled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CancelledDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_Notes_ParentNoteId",
                        column: x => x.ParentNoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ParentNoteId",
                table: "Notes",
                column: "ParentNoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
