using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitverse.CalendarService.Migrations
{
    public partial class TimetableAddandclassEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_ClassTypes_ClassTypeId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_ClassTypeId",
                table: "Classes");

            migrationBuilder.AlterColumn<int>(
                name: "ClassTypeId",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimetableId",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Timetables",
                columns: table => new
                {
                    TimetableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassTypeId = table.Column<int>(type: "int", nullable: true),
                    StartingDate = table.Column<DateTime>(type: "Date", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ClassesStartingTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetables", x => x.TimetableId);
                    table.ForeignKey(
                        name: "FK_Timetables_ClassTypes_ClassTypeId",
                        column: x => x.ClassTypeId,
                        principalTable: "ClassTypes",
                        principalColumn: "ClassTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TimetableId",
                table: "Classes",
                column: "TimetableId");

            migrationBuilder.CreateIndex(
                name: "IX_Timetables_ClassTypeId",
                table: "Timetables",
                column: "ClassTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Timetables_TimetableId",
                table: "Classes",
                column: "TimetableId",
                principalTable: "Timetables",
                principalColumn: "TimetableId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Timetables_TimetableId",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "Timetables");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TimetableId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "TimetableId",
                table: "Classes");

            migrationBuilder.AlterColumn<int>(
                name: "ClassTypeId",
                table: "Classes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassTypeId",
                table: "Classes",
                column: "ClassTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_ClassTypes_ClassTypeId",
                table: "Classes",
                column: "ClassTypeId",
                principalTable: "ClassTypes",
                principalColumn: "ClassTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
