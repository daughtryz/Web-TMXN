using Microsoft.EntityFrameworkCore.Migrations;

namespace TMXN.Data.Migrations
{
    public partial class IdeletableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TMXNUsers_Teams_TeamId",
                table: "TMXNUsers");

            migrationBuilder.DropIndex(
                name: "IX_TMXNUsers_TeamId",
                table: "TMXNUsers");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "TMXNUsers");

            migrationBuilder.AddColumn<string>(
                name: "TMXNUserId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TMXNUserId",
                table: "Teams",
                column: "TMXNUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TMXNUsers_TMXNUserId",
                table: "Teams",
                column: "TMXNUserId",
                principalTable: "TMXNUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TMXNUsers_TMXNUserId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TMXNUserId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TMXNUserId",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "TeamId",
                table: "TMXNUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TMXNUsers_TeamId",
                table: "TMXNUsers",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TMXNUsers_Teams_TeamId",
                table: "TMXNUsers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
