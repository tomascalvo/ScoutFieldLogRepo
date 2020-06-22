using Microsoft.EntityFrameworkCore.Migrations;


namespace ScoutFieldLog_GroupProject.Migrations
{
    public partial class Evaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Landscape",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landscape", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnologyArea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologyArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Landscap__737584F64EE993CF",
                table: "Landscape",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Technolo__737584F62C4CE5B0",
                table: "TechnologyArea",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Theme__737584F6C6E52742",
                table: "Theme",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Landscape");

            migrationBuilder.DropTable(
                name: "TechnologyArea");

            migrationBuilder.DropTable(
                name: "Theme");
        }
    }
}
