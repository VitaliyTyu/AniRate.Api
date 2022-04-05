using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniRate.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeTitles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeTitles_AnimeCollections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "AnimeCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeCollections_Id",
                table: "AnimeCollections",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimeTitles_CollectionId",
                table: "AnimeTitles",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeTitles_Id",
                table: "AnimeTitles",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeTitles");

            migrationBuilder.DropTable(
                name: "AnimeCollections");
        }
    }
}
