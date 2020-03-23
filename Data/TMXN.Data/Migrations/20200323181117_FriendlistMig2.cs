using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMXN.Data.Migrations
{
    public partial class FriendlistMig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FriendlistId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Friendlists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendlists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FriendlistId",
                table: "AspNetUsers",
                column: "FriendlistId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendlists_IsDeleted",
                table: "Friendlists",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Friendlists_FriendlistId",
                table: "AspNetUsers",
                column: "FriendlistId",
                principalTable: "Friendlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Friendlists_FriendlistId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Friendlists");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FriendlistId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FriendlistId",
                table: "AspNetUsers");
        }
    }
}
