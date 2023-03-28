using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class AddNombreColumnToEquivalenciasEntidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "EquivalenciasEntidades",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "EquivalenciasEntidades");
        }
    }
}
