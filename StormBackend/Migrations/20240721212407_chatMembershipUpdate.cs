using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StormBackend.Migrations
{
    /// <inheritdoc />
    public partial class chatMembershipUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMemberships_Chats_Id",
                table: "ChatMemberships");

            migrationBuilder.DropIndex(
                name: "IX_ChatMemberships_ChatId",
                table: "ChatMemberships");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ChatMemberships",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMemberships_ChatId",
                table: "ChatMemberships",
                column: "ChatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatMemberships_ChatId",
                table: "ChatMemberships");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ChatMemberships",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMemberships_ChatId",
                table: "ChatMemberships",
                column: "ChatId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMemberships_Chats_Id",
                table: "ChatMemberships",
                column: "Id",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
