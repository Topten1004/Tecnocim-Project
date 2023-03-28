using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class AddRatioMaestros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RatioMaestros",
                columns: table => new
                {
                    RatioMaestroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Concepto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ColorPositivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ColorNegativo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IconoPositivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IconoNegativo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatioMaestro", x => x.RatioMaestroId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatioMaestros");
        }
    }
}
