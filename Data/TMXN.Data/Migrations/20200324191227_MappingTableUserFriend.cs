using Microsoft.EntityFrameworkCore.Migrations;

namespace TMXN.Data.Migrations
{
    public partial class MappingTableUserFriend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersFriends",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    UserFriendlistId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersFriends", x => new { x.ApplicationUserId, x.UserFriendlistId });
                    table.ForeignKey(
                        name: "FK_UsersFriends_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersFriends_UserFriendlists_UserFriendlistId",
                        column: x => x.UserFriendlistId,
                        principalTable: "UserFriendlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersFriends_UserFriendlistId",
                table: "UsersFriends",
                column: "UserFriendlistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersFriends");
        }
    }
}
