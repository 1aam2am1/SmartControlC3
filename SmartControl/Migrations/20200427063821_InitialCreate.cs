using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartControl.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    SqlParameterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.SqlParameterId);
                });

            migrationBuilder.CreateTable(
                name: "TimePeriods",
                columns: table => new
                {
                    Begining = table.Column<string>(nullable: false),
                    End = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriods", x => new { x.Begining, x.End });
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    SqlValueInTimeId = table.Column<string>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    SqlParameterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.SqlValueInTimeId);
                    table.ForeignKey(
                        name: "FK_Values_Parameters_SqlParameterId",
                        column: x => x.SqlParameterId,
                        principalTable: "Parameters",
                        principalColumn: "SqlParameterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Values_SqlParameterId",
                table: "Values",
                column: "SqlParameterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimePeriods");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Parameters");
        }
    }
}
