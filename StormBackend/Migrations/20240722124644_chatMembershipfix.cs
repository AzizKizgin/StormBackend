using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StormBackend.Migrations
{
    /// <inheritdoc />
    public partial class chatMembershipfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMemberships_AspNetUsers_UserId",
                table: "ChatMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMemberships_Chats_ChatId",
                table: "ChatMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMemberships_AspNetUsers_UserId",
                table: "GroupMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMemberships_Groups_GroupId",
                table: "GroupMemberships");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMemberships_AspNetUsers_UserId",
                table: "ChatMemberships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMemberships_Chats_ChatId",
                table: "ChatMemberships",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMemberships_AspNetUsers_UserId",
                table: "GroupMemberships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMemberships_Groups_GroupId",
                table: "GroupMemberships",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMemberships_AspNetUsers_UserId",
                table: "ChatMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMemberships_Chats_ChatId",
                table: "ChatMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMemberships_AspNetUsers_UserId",
                table: "GroupMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMemberships_Groups_GroupId",
                table: "GroupMemberships");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMemberships_AspNetUsers_UserId",
                table: "ChatMemberships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMemberships_Chats_ChatId",
                table: "ChatMemberships",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMemberships_AspNetUsers_UserId",
                table: "GroupMemberships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMemberships_Groups_GroupId",
                table: "GroupMemberships",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
