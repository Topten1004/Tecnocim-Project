using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class AddEquivalenciaPeriodificacionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "EquivalenciasPeriodificacionId",
                table: "Contratos",
                type: "smallint",
                nullable: false,
                defaultValue: (short)1);

            migrationBuilder.CreateTable(
                name: "EquivalenciasPeriodificaciones",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenciasPeriodificacion", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 1,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 2,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 3,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 1,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 2,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 3,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 4,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 5,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 6,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 7,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 8,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 9,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 10,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "EquivalenciasPeriodificaciones",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { (short)1, "Mensual" },
                    { (short)3, "Trimestral" },
                    { (short)4, "Cuatrimestral" },
                    { (short)6, "Semestral" },
                    { (short)12, "Anual" }
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$gHpVomBYOfeOsLUpa4G8POUI9WRUYuFghM.RFGLnw2WvivokopSnu", new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$iWxRkcHk0y7B7Y3uZ4H3CuTWi5e1V5wwr73fxfAixdNIMSw5jEsGu", new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$YH47IWbiAlWrDJBjnXSq1uRNVGIxLuNLNHvS8x3UMlPWhGp4JhdMG", new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$95lukOtwB6N3DS06zY0IaOGIONFrapMbGJ7sS2DgpK8bDk/cYkoqG", new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_EquivalenciasPeriodificacionId",
                table: "Contratos",
                column: "EquivalenciasPeriodificacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_EquivalenciasPeriodificaciones_EquivalenciasPeriodificacionId",
                table: "Contratos",
                column: "EquivalenciasPeriodificacionId",
                principalTable: "EquivalenciasPeriodificaciones",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_EquivalenciasPeriodificaciones_EquivalenciasPeriodificacionId",
                table: "Contratos");

            migrationBuilder.DropTable(
                name: "EquivalenciasPeriodificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_EquivalenciasPeriodificacionId",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "EquivalenciasPeriodificacionId",
                table: "Contratos");

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 1,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(6701), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(6722), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(6701) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 2,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(6701), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(6745), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(6701) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 3,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(6701), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(6747), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(6701) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 1,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 2,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 3,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 4,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 5,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 6,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 7,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 8,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 9,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 10,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935), new DateTime(2022, 9, 21, 22, 23, 41, 822, DateTimeKind.Utc).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 18, DateTimeKind.Utc).AddTicks(4764), "$2a$11$VpgUpk41.fcycIsZlj95audHWI5qxpO74YQlm9N96gY5x1G3f32D6", new DateTime(2022, 9, 21, 22, 23, 41, 18, DateTimeKind.Utc).AddTicks(4764) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 18, DateTimeKind.Utc).AddTicks(4764), "$2a$11$CIB5c7iI2JzFnJ9kiAMRR.LxNlNS12GeLj2NCOLelU6mTrIDLq6g2", new DateTime(2022, 9, 21, 22, 23, 41, 18, DateTimeKind.Utc).AddTicks(4764) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 18, DateTimeKind.Utc).AddTicks(4764), "$2a$11$qRUZQB7A9Az80yRldKAy7.ZH3zcvsBq8xThVJvTW4SuDHOXiG8w12", new DateTime(2022, 9, 21, 22, 23, 41, 18, DateTimeKind.Utc).AddTicks(4764) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 23, 41, 18, DateTimeKind.Utc).AddTicks(4764), "$2a$11$kFuo.QCsNuCo8.TkfD6/CekjjCRhZWP/IFxnLRP7eNdn4kM6qTT.m", new DateTime(2022, 9, 21, 22, 23, 41, 18, DateTimeKind.Utc).AddTicks(4764) });
        }
    }
}
