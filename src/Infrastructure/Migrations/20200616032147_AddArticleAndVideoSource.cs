using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddArticleAndVideoSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articleSources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateUserId = table.Column<string>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articleSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "videoSources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateUserId = table.Column<string>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_videoSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationArticleSources",
                columns: table => new
                {
                    SpecificationId = table.Column<int>(nullable: false),
                    ArticleSourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationArticleSources", x => new { x.SpecificationId, x.ArticleSourceId });
                    table.ForeignKey(
                        name: "FK_SpecificationArticleSources_articleSources_ArticleSourceId",
                        column: x => x.ArticleSourceId,
                        principalTable: "articleSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificationArticleSources_Specifications_SpecificationId",
                        column: x => x.SpecificationId,
                        principalTable: "Specifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationVideoSources",
                columns: table => new
                {
                    SpecificationId = table.Column<int>(nullable: false),
                    VideoSourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationVideoSources", x => new { x.SpecificationId, x.VideoSourceId });
                    table.ForeignKey(
                        name: "FK_SpecificationVideoSources_Specifications_SpecificationId",
                        column: x => x.SpecificationId,
                        principalTable: "Specifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificationVideoSources_videoSources_VideoSourceId",
                        column: x => x.VideoSourceId,
                        principalTable: "videoSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationArticleSources_ArticleSourceId",
                table: "SpecificationArticleSources",
                column: "ArticleSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationVideoSources_VideoSourceId",
                table: "SpecificationVideoSources",
                column: "VideoSourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecificationArticleSources");

            migrationBuilder.DropTable(
                name: "SpecificationVideoSources");

            migrationBuilder.DropTable(
                name: "articleSources");

            migrationBuilder.DropTable(
                name: "videoSources");
        }
    }
}
