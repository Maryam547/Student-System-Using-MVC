using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCD2.Migrations
{
    /// <inheritdoc />
    public partial class updateonDepartments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "departments",
                newName: "DepartmentDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentDescription",
                table: "departments",
                newName: "Description");
        }
    }
}
