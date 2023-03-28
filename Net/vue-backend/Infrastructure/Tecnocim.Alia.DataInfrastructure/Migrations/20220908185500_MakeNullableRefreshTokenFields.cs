using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class MakeNullableRefreshTokenFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RevokedByIp",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReplacedByToken",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReasonRevoked",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RevokedByIp",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReplacedByToken",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReasonRevoked",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 1,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5928), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 2,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(6035), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 3,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(6037), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 1,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 2,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 3,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 4,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 5,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 6,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 7,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 8,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 9,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 10,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254), "$2a$11$baizctOg5DEOiUBjmXMCRulo7lman0Fz8nztQ1QXLhR79V4LeA076", new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254), "$2a$11$Z.OLJ8Er7eD0mgATrfqxf.Nt1ttIo0V6yVhdDjvyVr..CW/tObh7a", new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254), "$2a$11$QLu01OkX7H5dMDXbVei10.Xl5R5s7OEVhnyKvACUnGU2.diIBgVB2", new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254), "$2a$11$2Jg0bPg/NIMCE2G3wM/Ew.QpJw2KwXySX.xHX3RQUBL5slG/TY2Zm", new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254) });
        }
    }
}
