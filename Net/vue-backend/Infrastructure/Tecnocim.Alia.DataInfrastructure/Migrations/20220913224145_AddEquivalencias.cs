using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class AddEquivalencias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquivalenciasEntidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciasEntidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquivalenciasMonedas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciasMoneda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquivalenciasNatintervs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciasNatinterv", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquivalenciasPersonales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciasPersonal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquivalenciasPlazos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciasPlazo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquivalenciasProductos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciasProducto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquivalenciasReales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciasReal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquivalenciasSituopers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciasSituoper", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquivalenciasSolcols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciasSolcol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquivalenciasTipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciasTipo", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 1,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(5508), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(5535), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(5508) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 2,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(5508), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(5622), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(5508) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 3,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(5508), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(5624), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(5508) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 1,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 2,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 3,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 4,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 5,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 6,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 7,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 8,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 9,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 10,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695), new DateTime(2022, 9, 13, 22, 41, 32, 53, DateTimeKind.Utc).AddTicks(4695) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 31, 298, DateTimeKind.Utc).AddTicks(3244), "$2a$11$GCTIlPwsqsG5Z9y6xOF4aO5Yy0OrxHapbLvqyvp4i6XHqPUEZk9q2", new DateTime(2022, 9, 13, 22, 41, 31, 298, DateTimeKind.Utc).AddTicks(3244) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 31, 298, DateTimeKind.Utc).AddTicks(3244), "$2a$11$7r.dUxhCkxbDJEnHDZ9wa.qavaSwmegcWNhpU37l0nslEyQBsCvnK", new DateTime(2022, 9, 13, 22, 41, 31, 298, DateTimeKind.Utc).AddTicks(3244) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 31, 298, DateTimeKind.Utc).AddTicks(3244), "$2a$11$o.ylJYylpVEYTpMdF.urmuO/3aZzSjZfHdJul20VGOzzCai/NYb6q", new DateTime(2022, 9, 13, 22, 41, 31, 298, DateTimeKind.Utc).AddTicks(3244) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 13, 22, 41, 31, 298, DateTimeKind.Utc).AddTicks(3244), "$2a$11$yXQTMpdiymwiFqH/2X1gcuVCGv4.u3ti.TvMho5/CeO7peQOEQ846", new DateTime(2022, 9, 13, 22, 41, 31, 298, DateTimeKind.Utc).AddTicks(3244) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquivalenciasEntidades");

            migrationBuilder.DropTable(
                name: "EquivalenciasMonedas");

            migrationBuilder.DropTable(
                name: "EquivalenciasNatintervs");

            migrationBuilder.DropTable(
                name: "EquivalenciasPersonales");

            migrationBuilder.DropTable(
                name: "EquivalenciasPlazos");

            migrationBuilder.DropTable(
                name: "EquivalenciasProductos");

            migrationBuilder.DropTable(
                name: "EquivalenciasReales");

            migrationBuilder.DropTable(
                name: "EquivalenciasSituopers");

            migrationBuilder.DropTable(
                name: "EquivalenciasSolcols");

            migrationBuilder.DropTable(
                name: "EquivalenciasTipos");

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 1,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2853), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2870), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2853) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 2,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2853), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2872), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2853) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 3,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2853), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2873), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2853) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 1,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 2,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 3,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 4,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 5,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 6,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 7,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 8,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 9,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 10,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511), new DateTime(2022, 9, 8, 18, 55, 0, 316, DateTimeKind.Utc).AddTicks(2511) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 54, 59, 471, DateTimeKind.Utc).AddTicks(6432), "$2a$11$pEJ7vPMxlWpV.HyqQpmfquEJnqkQbSi.eGas2XXIMw75wLZ69QWTu", new DateTime(2022, 9, 8, 18, 54, 59, 471, DateTimeKind.Utc).AddTicks(6432) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 54, 59, 471, DateTimeKind.Utc).AddTicks(6432), "$2a$11$B21..KQMAatzgu.TZtRwsuKMMiodx3q1gf91q.2iBXapdDwZ2T846", new DateTime(2022, 9, 8, 18, 54, 59, 471, DateTimeKind.Utc).AddTicks(6432) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 54, 59, 471, DateTimeKind.Utc).AddTicks(6432), "$2a$11$M/Et.W.PKbC4/n7Gh3A4ie3c5ghV/B04J4pydJm3TwS0f4.pt7zWq", new DateTime(2022, 9, 8, 18, 54, 59, 471, DateTimeKind.Utc).AddTicks(6432) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 18, 54, 59, 471, DateTimeKind.Utc).AddTicks(6432), "$2a$11$B4PyiCSEGVVmracy1afcWuPjPqcOC1GfC1diYPNyXMM7VONJCSKGC", new DateTime(2022, 9, 8, 18, 54, 59, 471, DateTimeKind.Utc).AddTicks(6432) });
        }
    }
}
