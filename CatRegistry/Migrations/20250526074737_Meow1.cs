using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatRegistry.Migrations
{
    /// <inheritdoc />
    public partial class Meow1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileToDatabase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    KittyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileToDatabase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kittys",
                columns: table => new
                {
                    KittyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KittySpeciesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KittyRegionOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KittyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kittys", x => x.KittyId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileToDatabase");

            migrationBuilder.DropTable(
                name: "Kittys");
        }
    }
}
