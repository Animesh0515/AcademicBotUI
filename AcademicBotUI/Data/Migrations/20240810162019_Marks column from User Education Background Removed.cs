using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicBorUI.Data.Migrations
{
    /// <inheritdoc />
    public partial class MarkscolumnfromUserEducationBackgroundRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectMarks",
                table: "UserEducationBackground");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubjectMarks",
                table: "UserEducationBackground",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
