using Microsoft.EntityFrameworkCore.Migrations;

namespace TMXN.Data.Migrations
{
    public partial class TournamentsExtraColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Organizer",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TournamentGameType",
                table: "Tournaments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Organizer",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TournamentGameType",
                table: "Tournaments");
        }
    }
}
