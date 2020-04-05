using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMXN.Data.Migrations
{
    public partial class AddBracketEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Ranklists_RanklistId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "MigTests");

            migrationBuilder.DropTable(
                name: "Ranklists");

            migrationBuilder.DropIndex(
                name: "IX_Teams_RanklistId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "RanklistId",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "BracketId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brackets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    TournamentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brackets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brackets_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_BracketId",
                table: "Teams",
                column: "BracketId");

            migrationBuilder.CreateIndex(
                name: "IX_Brackets_IsDeleted",
                table: "Brackets",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Brackets_TournamentId",
                table: "Brackets",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Brackets_BracketId",
                table: "Teams",
                column: "BracketId",
                principalTable: "Brackets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Brackets_BracketId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Brackets");

            migrationBuilder.DropIndex(
                name: "IX_Teams_BracketId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "BracketId",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "RanklistId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MigTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MigTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ranklists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranklists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_RanklistId",
                table: "Teams",
                column: "RanklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Ranklists_IsDeleted",
                table: "Ranklists",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Ranklists_RanklistId",
                table: "Teams",
                column: "RanklistId",
                principalTable: "Ranklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
