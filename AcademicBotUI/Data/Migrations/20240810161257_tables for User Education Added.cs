using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicBorUI.Data.Migrations
{
    /// <inheritdoc />
    public partial class tablesforUserEducationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEducationBackground",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelOfStudy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldOfStudy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectMarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OverallScore = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEducationBackground", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyLevelSubject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEducationBackgroundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyLevelSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyLevelSubject_UserEducationBackground_UserEducationBackgroundId",
                        column: x => x.UserEducationBackgroundId,
                        principalTable: "UserEducationBackground",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyLevelSubject_UserEducationBackgroundId",
                table: "StudyLevelSubject",
                column: "UserEducationBackgroundId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyLevelSubject");

            migrationBuilder.DropTable(
                name: "UserEducationBackground");
        }
    }
}
