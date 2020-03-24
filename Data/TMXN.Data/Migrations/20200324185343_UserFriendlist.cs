using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMXN.Data.Migrations
{
    public partial class UserFriendlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserFriendlistId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserFriendlists",
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
                    table.PrimaryKey("PK_UserFriendlists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserFriendlistId",
                table: "AspNetUsers",
                column: "UserFriendlistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriendlists_IsDeleted",
                table: "UserFriendlists",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserFriendlists_UserFriendlistId",
                table: "AspNetUsers",
                column: "UserFriendlistId",
                principalTable: "UserFriendlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserFriendlists_UserFriendlistId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserFriendlists");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserFriendlistId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserFriendlistId",
                table: "AspNetUsers");
        }
    }
}
