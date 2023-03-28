using Microsoft.EntityFrameworkCore.Migrations;
using Tecnocim.Alia.DataInfrastructure.Extensions;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class SeedContabilidadConfiguracionesAndUpdateEquivalenciasProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScriptFile("SeedTablaContabilidadConfiguraciones.sql");
            migrationBuilder.RunSqlScriptFile("SeedTablaEquivalenciasProductos.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
