using Microsoft.EntityFrameworkCore.Migrations;

namespace ScoutFieldLog_GroupProject.Migrations
{
    public partial class Evaluation6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Evaluation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Evaluation");
        }
    }
}
