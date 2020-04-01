using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMXN.Data.Migrations
{
    public partial class AddPointsToTeamsAndRanklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RanklistId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ranklists",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Ranklists_RanklistId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Ranklists");

            migrationBuilder.DropIndex(
                name: "IX_Teams_RanklistId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "RanklistId",
                table: "Teams");
        }
    }
}
