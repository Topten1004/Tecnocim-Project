using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class AddPlazosAmortizacionToContrato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuota_Contratos_ContratoId",
                table: "Cuota");

            migrationBuilder.RenameTable(
                name: "Cuota",
                newName: "Cuotas");

            migrationBuilder.AddColumn<int>(
                name: "PlazosAmortizacion",
                table: "Contratos",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cuotas_Contratos_ContratoId",
                table: "Cuotas",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "ContratoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuotas_Contratos_ContratoId",
                table: "Cuotas");

            migrationBuilder.DropColumn(
                name: "PlazosAmortizacion",
                table: "Contratos");

            migrationBuilder.RenameTable(
                name: "Cuotas",
                newName: "Cuota");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuota_Contratos_ContratoId",
                table: "Cuota",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "ContratoId");
        }
    }
}
