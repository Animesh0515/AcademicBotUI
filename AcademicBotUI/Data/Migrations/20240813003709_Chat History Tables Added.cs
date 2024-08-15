using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicBorUI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChatHistoryTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserChatHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChatHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatHistory",
                columns: table => new
                {
                    ChatHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserInput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserChatHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatHistory", x => x.ChatHistoryId);
                    table.ForeignKey(
                        name: "FK_ChatHistory_UserChatHistory_UserChatHistoryId",
                        column: x => x.UserChatHistoryId,
                        principalTable: "UserChatHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatHistory_UserChatHistoryId",
                table: "ChatHistory",
                column: "UserChatHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatHistory_UserId",
                table: "UserChatHistory",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatHistory");

            migrationBuilder.DropTable(
                name: "UserChatHistory");
        }
    }
}
