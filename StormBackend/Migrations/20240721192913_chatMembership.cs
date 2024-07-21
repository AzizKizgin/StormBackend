using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StormBackend.Migrations
{
    /// <inheritdoc />
    public partial class chatMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_User1Id",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_User2Id",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AspNetUsers_ContactUserId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmojiReactions_AspNetUsers_UserId",
                table: "EmojiReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_EmojiReactions_Messages_MessageId",
                table: "EmojiReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMemberships_AspNetUsers_UserId",
                table: "GroupMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMemberships_Groups_GroupId",
                table: "GroupMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Chats_User1Id",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_User2Id",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "User1Id",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "User2Id",
                table: "Chats");

            migrationBuilder.CreateTable(
                name: "ChatMemberships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsMuted = table.Column<bool>(type: "bit", nullable: false),
                    IsPinned = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMemberships_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMemberships_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMemberships_Chats_Id",
                        column: x => x.Id,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMemberships_ChatId",
                table: "ChatMemberships",
                column: "ChatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMemberships_UserId",
                table: "ChatMemberships",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_AspNetUsers_ContactUserId",
                table: "Contacts",
                column: "ContactUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmojiReactions_AspNetUsers_UserId",
                table: "EmojiReactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmojiReactions_Messages_MessageId",
                table: "EmojiReactions",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AspNetUsers_ContactUserId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmojiReactions_AspNetUsers_UserId",
                table: "EmojiReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_EmojiReactions_Messages_MessageId",
                table: "EmojiReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMemberships_AspNetUsers_UserId",
                table: "GroupMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMemberships_Groups_GroupId",
                table: "GroupMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "ChatMemberships");

            migrationBuilder.AddColumn<string>(
                name: "User1Id",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User2Id",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_User1Id",
                table: "Chats",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_User2Id",
                table: "Chats",
                column: "User2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_User1Id",
                table: "Chats",
                column: "User1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_User2Id",
                table: "Chats",
                column: "User2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_AspNetUsers_ContactUserId",
                table: "Contacts",
                column: "ContactUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmojiReactions_AspNetUsers_UserId",
                table: "EmojiReactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmojiReactions_Messages_MessageId",
                table: "EmojiReactions",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMemberships_AspNetUsers_UserId",
                table: "GroupMemberships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMemberships_Groups_GroupId",
                table: "GroupMemberships",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
