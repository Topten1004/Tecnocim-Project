using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class EnableFKConstraintsEquivalenciasProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                                    name: "FK_Contratos_EquivalenciasProductos_EquivalenciasProductoId",
                                    table: "Contratos",
                                    column: "EquivalenciasProductoId",
                                    principalTable: "EquivalenciasProductos",
                                    principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                                   name: "FK_Cirbes_EquivalenciasProductos_EquivalenciasProductoId",
                                   table: "Cirbes",
                                   column: "EquivalenciasProductoId",
                                   principalTable: "EquivalenciasProductos",
                                   principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
