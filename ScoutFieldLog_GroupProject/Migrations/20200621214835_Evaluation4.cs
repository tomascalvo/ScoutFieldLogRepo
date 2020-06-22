using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScoutFieldLog_GroupProject.Migrations
{
    public partial class Evaluation4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartUpCompaniesId = table.Column<int>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Posted = table.Column<DateTime>(nullable: false),
                    Alignments = table.Column<string>(nullable: true),
                    Themes = table.Column<string>(nullable: true),
                    Landscapes = table.Column<string>(nullable: true),
                    TechnologyAreas = table.Column<string>(nullable: true),
                    Uniqueness = table.Column<int>(nullable: false),
                    Team = table.Column<int>(nullable: false),
                    Interest = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_StartUpCompanies_StartUpCompaniesId",
                        column: x => x.StartUpCompaniesId,
                        principalTable: "StartUpCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_StartUpCompaniesId",
                table: "Evaluations",
                column: "StartUpCompaniesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations");
        }
    }
}
