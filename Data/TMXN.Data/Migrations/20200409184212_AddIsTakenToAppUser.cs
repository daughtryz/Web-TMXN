using Microsoft.EntityFrameworkCore.Migrations;

namespace TMXN.Data.Migrations
{
    public partial class AddIsTakenToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTaken",
                table: "UserFriendlists");

            migrationBuilder.AddColumn<bool>(
                name: "IsTaken",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTaken",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsTaken",
                table: "UserFriendlists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
