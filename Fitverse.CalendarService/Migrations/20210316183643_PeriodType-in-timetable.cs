using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitverse.CalendarService.Migrations
{
    public partial class PeriodTypeintimetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Timetables_TimetableId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Timetables_ClassTypes_ClassTypeId",
                table: "Timetables");

            migrationBuilder.DropIndex(
                name: "IX_Timetables_ClassTypeId",
                table: "Timetables");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TimetableId",
                table: "Classes");

            migrationBuilder.AlterColumn<int>(
                name: "ClassTypeId",
                table: "Timetables",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodType",
                table: "Timetables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodType",
                table: "Timetables");

            migrationBuilder.AlterColumn<int>(
                name: "ClassTypeId",
                table: "Timetables",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Classes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Timetables_ClassTypeId",
                table: "Timetables",
                column: "ClassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TimetableId",
                table: "Classes",
                column: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Timetables_TimetableId",
                table: "Classes",
                column: "TimetableId",
                principalTable: "Timetables",
                principalColumn: "TimetableId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Timetables_ClassTypes_ClassTypeId",
                table: "Timetables",
                column: "ClassTypeId",
                principalTable: "ClassTypes",
                principalColumn: "ClassTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
