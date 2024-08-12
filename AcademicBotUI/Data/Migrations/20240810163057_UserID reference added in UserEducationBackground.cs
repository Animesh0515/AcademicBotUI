using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicBorUI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserIDreferenceaddedinUserEducationBackground : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserEducationBackground",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserEducationBackground_UserId",
                table: "UserEducationBackground",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEducationBackground_AspNetUsers_UserId",
                table: "UserEducationBackground",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEducationBackground_AspNetUsers_UserId",
                table: "UserEducationBackground");

            migrationBuilder.DropIndex(
                name: "IX_UserEducationBackground_UserId",
                table: "UserEducationBackground");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserEducationBackground");
        }
    }
}
