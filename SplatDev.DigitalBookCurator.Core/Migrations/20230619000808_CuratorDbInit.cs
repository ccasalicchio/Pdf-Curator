using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplatDev.DigitalBookCurator.Core.Migrations
{
    /// <inheritdoc />
    public partial class CuratorDbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Author = table.Column<string>(type: "TEXT", nullable: true),
                    Creator = table.Column<string>(type: "TEXT", nullable: true),
                    Keywords = table.Column<string>(type: "TEXT", nullable: true),
                    Producer = table.Column<string>(type: "TEXT", nullable: true),
                    Subject = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Introduction = table.Column<string>(type: "ntext", maxLength: 500, nullable: true),
                    Version = table.Column<string>(type: "TEXT", nullable: true),
                    Pages = table.Column<int>(type: "INTEGER", nullable: true),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    Format = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    Thumbnail = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_FileName_Title",
                table: "Books",
                columns: new[] { "FileName", "Title" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
