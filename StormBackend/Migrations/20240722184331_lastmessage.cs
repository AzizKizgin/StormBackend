﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StormBackend.Migrations
{
    /// <inheritdoc />
    public partial class lastmessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatId1",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId1",
                table: "Messages",
                column: "ChatId1",
                unique: true,
                filter: "[ChatId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId1",
                table: "Messages",
                column: "ChatId1",
                principalTable: "Chats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ChatId1",
                table: "Messages");
        }
    }
}
