using Microsoft.EntityFrameworkCore.Migrations;
using Tecnocim.Alia.DataInfrastructure.Extensions;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class SeedInterpretaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScriptFile("SeedInterpretaciones.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
