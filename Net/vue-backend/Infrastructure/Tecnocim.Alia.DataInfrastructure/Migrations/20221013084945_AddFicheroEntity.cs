using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class AddFicheroEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ficheros",
                columns: table => new
                {
                    FicheroId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Origen = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    FechaContenido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ExtractorId = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fichero", x => x.FicheroId);
                    table.ForeignKey(
                        name: "FK_Ficheros_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ficheros_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ficheros_EmpresaId",
                table: "Ficheros",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ficheros_UsuarioId",
                table: "Ficheros",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ficheros");

        }
    }
}
