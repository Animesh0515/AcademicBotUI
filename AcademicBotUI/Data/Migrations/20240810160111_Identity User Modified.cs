using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicBorUI.Data.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUserModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FieldsOfInterest",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KeySkills",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NotableAchievements",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldsOfInterest",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KeySkills",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NotableAchievements",
                table: "AspNetUsers");
        }
    }
}
