using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcGame.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Datawydania = table.Column<DateTime>(name: "Data_wydania", type: "datetime2", nullable: false),
                    Gatunek = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Wydawca = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
