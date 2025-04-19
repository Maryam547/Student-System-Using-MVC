using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCD2.Migrations
{
    /// <inheritdoc />
    public partial class updateonCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesInstructors");

            migrationBuilder.AddColumn<int>(
                name: "InstructorsId",
                table: "courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_courses_InstructorsId",
                table: "courses",
                column: "InstructorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_instructors_InstructorsId",
                table: "courses",
                column: "InstructorsId",
                principalTable: "instructors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_instructors_InstructorsId",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "IX_courses_InstructorsId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "InstructorsId",
                table: "courses");

            migrationBuilder.CreateTable(
                name: "CoursesInstructors",
                columns: table => new
                {
                    coursesId = table.Column<int>(type: "int", nullable: false),
                    instructorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesInstructors", x => new { x.coursesId, x.instructorsId });
                    table.ForeignKey(
                        name: "FK_CoursesInstructors_courses_coursesId",
                        column: x => x.coursesId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesInstructors_instructors_instructorsId",
                        column: x => x.instructorsId,
                        principalTable: "instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesInstructors_instructorsId",
                table: "CoursesInstructors",
                column: "instructorsId");
        }
    }
}
