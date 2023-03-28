using Microsoft.EntityFrameworkCore.Migrations;
using Tecnocim.Alia.DataInfrastructure.Extensions;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class SeedContabilidadConfiguraciones_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScriptFile("SeedTablaContabilidadConfiguraciones_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
