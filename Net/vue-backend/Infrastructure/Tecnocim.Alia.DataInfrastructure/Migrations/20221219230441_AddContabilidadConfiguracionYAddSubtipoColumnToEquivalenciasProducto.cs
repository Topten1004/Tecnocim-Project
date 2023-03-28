using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class AddContabilidadConfiguracionYAddSubtipoColumnToEquivalenciasProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subtipo",
                table: "EquivalenciasProductos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContabilidadConfiguraciones",
                columns: table => new
                {
                    ContabilidadConfiguracionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grupo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prioridad = table.Column<int>(type: "int", nullable: false),
                    Etiqueta = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContabilidadConfiguracion", x => x.ContabilidadConfiguracionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContabilidadConfiguraciones_Concepto",
                table: "ContabilidadConfiguraciones",
                column: "Concepto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContabilidadConfiguraciones");

            migrationBuilder.DropColumn(
                name: "Subtipo",
                table: "EquivalenciasProductos");
        }
    }
}
