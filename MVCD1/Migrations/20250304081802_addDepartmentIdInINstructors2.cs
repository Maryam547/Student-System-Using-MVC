using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCD2.Migrations
{
    /// <inheritdoc />
    public partial class addDepartmentIdInINstructors2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_departments_DepartmentsId",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_instructors_DepartmentsId",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "DepartmentsId",
                table: "instructors");

            migrationBuilder.CreateIndex(
                name: "IX_instructors_DepartmentId",
                table: "instructors",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_departments_DepartmentId",
                table: "instructors",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_departments_DepartmentId",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_instructors_DepartmentId",
                table: "instructors");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentsId",
                table: "instructors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_instructors_DepartmentsId",
                table: "instructors",
                column: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_departments_DepartmentsId",
                table: "instructors",
                column: "DepartmentsId",
                principalTable: "departments",
                principalColumn: "Id");
        }
    }
}
