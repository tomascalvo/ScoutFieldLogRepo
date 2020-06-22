using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScoutFieldLog_GroupProject.Migrations
{
    public partial class Evaluation5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.CreateTable(
                name: "Evaluation",
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
                    Potential = table.Column<int>(nullable: false),
                    Interest = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluation");

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alignments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interest = table.Column<int>(type: "int", nullable: false),
                    Landscapes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Posted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartUpCompaniesId = table.Column<int>(type: "int", nullable: false),
                    Team = table.Column<int>(type: "int", nullable: false),
                    TechnologyAreas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Themes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uniqueness = table.Column<int>(type: "int", nullable: false)
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
    }
}
