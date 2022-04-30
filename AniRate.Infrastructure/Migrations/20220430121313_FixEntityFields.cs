using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniRate.Infrastructure.Migrations
{
    public partial class FixEntityFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeTitles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRating = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Russian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Episodes = table.Column<int>(type: "int", nullable: true),
                    AiredOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleasedOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionHtml = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Russian = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_AnimeTitles_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "AnimeTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Original = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X96 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X48 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_AnimeTitles_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "AnimeTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRating = table.Column<double>(type: "float", nullable: true),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeCollections_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnimeCollectionAnimeTitle",
                columns: table => new
                {
                    AnimeCollectionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnimeTitlesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeCollectionAnimeTitle", x => new { x.AnimeCollectionsId, x.AnimeTitlesId });
                    table.ForeignKey(
                        name: "FK_AnimeCollectionAnimeTitle_AnimeCollections_AnimeCollectionsId",
                        column: x => x.AnimeCollectionsId,
                        principalTable: "AnimeCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeCollectionAnimeTitle_AnimeTitles_AnimeTitlesId",
                        column: x => x.AnimeTitlesId,
                        principalTable: "AnimeTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Id",
                table: "Accounts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimeCollectionAnimeTitle_AnimeTitlesId",
                table: "AnimeCollectionAnimeTitle",
                column: "AnimeTitlesId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeCollections_Id",
                table: "AnimeCollections",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimeCollections_ImageId",
                table: "AnimeCollections",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeTitles_Id",
                table: "AnimeTitles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_AnimeId",
                table: "Genres",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Id",
                table: "Genres",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AnimeId",
                table: "Images",
                column: "AnimeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_Id",
                table: "Images",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AnimeCollectionAnimeTitle");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "AnimeCollections");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "AnimeTitles");
        }
    }
}
