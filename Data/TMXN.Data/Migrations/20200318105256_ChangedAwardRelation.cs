using Microsoft.EntityFrameworkCore.Migrations;

namespace TMXN.Data.Migrations
{
    public partial class ChangedAwardRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamId",
                table: "Awards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Awards_TeamId",
                table: "Awards",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awards_Teams_TeamId",
                table: "Awards",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awards_Teams_TeamId",
                table: "Awards");

            migrationBuilder.DropIndex(
                name: "IX_Awards_TeamId",
                table: "Awards");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Awards");
        }
    }
}
