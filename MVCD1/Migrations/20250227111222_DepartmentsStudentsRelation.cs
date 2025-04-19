using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCD2.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentsStudentsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentsId",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_DepartmentsId",
                table: "students",
                column: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_departments_DepartmentsId",
                table: "students",
                column: "DepartmentsId",
                principalTable: "departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_departments_DepartmentsId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_DepartmentsId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "DepartmentsId",
                table: "students");
        }
    }
}
