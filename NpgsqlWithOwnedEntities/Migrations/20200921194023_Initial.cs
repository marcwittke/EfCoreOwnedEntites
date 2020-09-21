using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NpgsqlWithOwnedEntities.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimpleEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ExtendedName = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimpleEntities1",
                columns: table => new
                {
                    SimpleEntityId = table.Column<int>(nullable: false),
                    Name1 = table.Column<string>(maxLength: 120, nullable: true),
                    Number2 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleEntities1", x => x.SimpleEntityId);
                    table.ForeignKey(
                        name: "FK_SimpleEntities1_SimpleEntities_SimpleEntityId",
                        column: x => x.SimpleEntityId,
                        principalTable: "SimpleEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimpleEntities1");

            migrationBuilder.DropTable(
                name: "SimpleEntities");
        }
    }
}
