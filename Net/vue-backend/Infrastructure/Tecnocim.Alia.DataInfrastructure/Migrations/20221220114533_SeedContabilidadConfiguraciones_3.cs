using Microsoft.EntityFrameworkCore.Migrations;
using Tecnocim.Alia.DataInfrastructure.Extensions;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class SeedContabilidadConfiguraciones_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint("FK_Contratos_EquivalenciasProductos_EquivalenciasProductoId", "Contratos");
            migrationBuilder.DropCheckConstraint("FK_Cirbes_EquivalenciasProductos_EquivalenciasProductoId", "Cirbes");

            migrationBuilder.RunSqlScriptFile("SeedTablaContabilidadConfiguraciones_3.sql");
            migrationBuilder.RunSqlScriptFile("SeedTablaEquivalenciasProductos_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
