using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class RenameBaseScheduleToDefaultSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseSchedules");

            migrationBuilder.CreateTable(
                name: "DefaultSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateUserId = table.Column<string>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Sequence = table.Column<int>(nullable: false),
                    DisplayFlag = table.Column<bool>(nullable: false),
                    TvProgramId = table.Column<int>(nullable: false),
                    CornerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefaultSchedules_Corners_CornerId",
                        column: x => x.CornerId,
                        principalTable: "Corners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DefaultSchedules_TvPrograms_TvProgramId",
                        column: x => x.TvProgramId,
                        principalTable: "TvPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DefaultSchedules_CornerId",
                table: "DefaultSchedules",
                column: "CornerId");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultSchedules_TvProgramId_Sequence",
                table: "DefaultSchedules",
                columns: new[] { "TvProgramId", "Sequence" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultSchedules");

            migrationBuilder.CreateTable(
                name: "BaseSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CornerId = table.Column<int>(type: "int", nullable: true),
                    CreateUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisplayFlag = table.Column<bool>(type: "bit", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    TvProgramId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseSchedules_Corners_CornerId",
                        column: x => x.CornerId,
                        principalTable: "Corners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseSchedules_TvPrograms_TvProgramId",
                        column: x => x.TvProgramId,
                        principalTable: "TvPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseSchedules_CornerId",
                table: "BaseSchedules",
                column: "CornerId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSchedules_TvProgramId_Sequence",
                table: "BaseSchedules",
                columns: new[] { "TvProgramId", "Sequence" },
                unique: true);
        }
    }
}
