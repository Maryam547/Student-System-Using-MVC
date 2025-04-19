using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCD2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInstructorDepartmentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "instructors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "instructors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_instructors_UserId",
                table: "instructors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_AspNetUsers_UserId",
                table: "instructors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_AspNetUsers_UserId",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_instructors_UserId",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "instructors");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "HireDate",
                table: "instructors",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
