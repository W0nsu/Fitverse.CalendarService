using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitverse.CalendarService.Migrations
{
    public partial class ClassTypeIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClassTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MemberId",
                table: "Members",
                column: "MemberId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Members_MemberId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClassTypes");
        }
    }
}
