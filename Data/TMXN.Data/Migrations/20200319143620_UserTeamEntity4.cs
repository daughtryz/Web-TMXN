using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMXN.Data.Migrations
{
    public partial class UserTeamEntity4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UsersTeams",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UsersTeams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_UsersTeams_IsDeleted",
                table: "UsersTeams",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersTeams_IsDeleted",
                table: "UsersTeams");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UsersTeams");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UsersTeams");
        }
    }
}
