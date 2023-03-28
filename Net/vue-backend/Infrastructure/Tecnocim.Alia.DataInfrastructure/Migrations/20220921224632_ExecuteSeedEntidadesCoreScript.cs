using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Tecnocim.Alia.DataInfrastructure.Extensions;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class ExecuteSeedEntidadesCoreScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScriptFile("SeedTablasCore.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
