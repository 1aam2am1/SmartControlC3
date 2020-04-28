using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartControl.Migrations
{
    public partial class AddParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SqlParameterId",
                table: "TimePeriods",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SqlParameterId",
                table: "TimePeriods");
        }
    }
}
