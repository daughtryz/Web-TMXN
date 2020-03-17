using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMXN.Data.Migrations
{
    public partial class ChangedAppUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_TMXNUsers_TMXNUserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_TMXNUsers_TMXNUserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_TMXNUsers_TMXNUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TMXNUsers_TMXNUserId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "TMXNUsers");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TMXNUserId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_TMXNUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_TMXNUserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_TMXNUserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "TMXNUserId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TMXNUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "TMXNUserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "TMXNUserId",
                table: "AspNetUserClaims");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TMXNUserId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TMXNUserId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TMXNUserId",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TMXNUserId",
                table: "AspNetUserClaims",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TMXNUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMXNUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TMXNUserId",
                table: "Teams",
                column: "TMXNUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_TMXNUserId",
                table: "AspNetUserRoles",
                column: "TMXNUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_TMXNUserId",
                table: "AspNetUserLogins",
                column: "TMXNUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_TMXNUserId",
                table: "AspNetUserClaims",
                column: "TMXNUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TMXNUsers_IsDeleted",
                table: "TMXNUsers",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_TMXNUsers_TMXNUserId",
                table: "AspNetUserClaims",
                column: "TMXNUserId",
                principalTable: "TMXNUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_TMXNUsers_TMXNUserId",
                table: "AspNetUserLogins",
                column: "TMXNUserId",
                principalTable: "TMXNUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_TMXNUsers_TMXNUserId",
                table: "AspNetUserRoles",
                column: "TMXNUserId",
                principalTable: "TMXNUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TMXNUsers_TMXNUserId",
                table: "Teams",
                column: "TMXNUserId",
                principalTable: "TMXNUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
