using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCD2.Migrations
{
    /// <inheritdoc />
    public partial class Updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_courses_CoursesId",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_instructors_CoursesId",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "instructors");

            migrationBuilder.AddColumn<int>(
                name: "MinDegree",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesInstructors");

            migrationBuilder.DropColumn(
                name: "MinDegree",
                table: "courses");

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "instructors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_instructors_CoursesId",
                table: "instructors",
                column: "CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_courses_CoursesId",
                table: "instructors",
                column: "CoursesId",
                principalTable: "courses",
                principalColumn: "Id");
        }
    }
}
