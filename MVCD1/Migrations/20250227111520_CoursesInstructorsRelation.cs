using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCD2.Migrations
{
    /// <inheritdoc />
    public partial class CoursesInstructorsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
