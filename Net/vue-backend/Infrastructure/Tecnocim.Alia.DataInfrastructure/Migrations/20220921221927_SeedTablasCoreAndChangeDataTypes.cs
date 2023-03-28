using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class SeedTablasCoreAndChangeDataTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pool_Contratos_ContratoId",
                table: "Pool");

            migrationBuilder.DropForeignKey(
                name: "FK_Pool_Documentos_DocumentoId",
                table: "Pool");

            migrationBuilder.RenameTable(
                name: "Pool",
                newName: "Pools");

            migrationBuilder.RenameIndex(
                name: "IX_Pool_DocumentoId",
                table: "Pools",
                newName: "IX_Pools_DocumentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pool_ContratoId",
                table: "Pools",
                newName: "IX_Pools_ContratoId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Magnitud",
                table: "Ratios",
                type: "decimal(26,18)",
                precision: 26,
                scale: 18,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Documentos",
                type: "bit",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<bool>(
                name: "Minimis",
                table: "Contratos",
                type: "bit",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Digitalizada",
                table: "Contratos",
                type: "bit",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Magnitud",
                table: "Contabilidades",
                type: "decimal(26,18)",
                precision: 26,
                scale: 18,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Magnitud",
                table: "Analiticas",
                type: "decimal(26,18)",
                precision: 26,
                scale: 18,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 1,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3972), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(4023), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3972) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 2,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3972), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(4026), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3972) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 3,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3972), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(4027), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3972) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 1,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 2,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 3,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 4,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 5,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 6,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 7,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 8,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 9,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 10,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173), new DateTime(2022, 9, 21, 21, 54, 32, 802, DateTimeKind.Utc).AddTicks(3173) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 6, DateTimeKind.Utc).AddTicks(6378), "$2a$11$WjuTTrFEcXCsjngMCfERVe8J8afBheZOn97GJ9kDL7/aWih.dDgNa", new DateTime(2022, 9, 21, 21, 54, 32, 6, DateTimeKind.Utc).AddTicks(6378) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 6, DateTimeKind.Utc).AddTicks(6378), "$2a$11$ojrKfAFSYlPhySBQbrL5.uLCZ8MLoVtsCCL1TX7gjsWYo2hyFlnqW", new DateTime(2022, 9, 21, 21, 54, 32, 6, DateTimeKind.Utc).AddTicks(6378) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 6, DateTimeKind.Utc).AddTicks(6378), "$2a$11$Rh0szPwDR3Mzk6AsYHi9We56zw3NmYm5YO1JG35A7XViqxILqYEmW", new DateTime(2022, 9, 21, 21, 54, 32, 6, DateTimeKind.Utc).AddTicks(6378) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 21, 21, 54, 32, 6, DateTimeKind.Utc).AddTicks(6378), "$2a$11$5fT.cqXyi7GCDZ3lvrJhwechA2awW97FQRfa9y7juzCEVZMIOmjGa", new DateTime(2022, 9, 21, 21, 54, 32, 6, DateTimeKind.Utc).AddTicks(6378) });

            migrationBuilder.AddForeignKey(
                name: "FK_Pools_Contratos_ContratoId",
                table: "Pools",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "ContratoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pools_Documentos_DocumentoId",
                table: "Pools",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pools_Contratos_ContratoId",
                table: "Pools");

            migrationBuilder.DropForeignKey(
                name: "FK_Pools_Documentos_DocumentoId",
                table: "Pools");

            migrationBuilder.RenameTable(
                name: "Pools",
                newName: "Pool");

            migrationBuilder.RenameIndex(
                name: "IX_Pools_DocumentoId",
                table: "Pool",
                newName: "IX_Pool_DocumentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pools_ContratoId",
                table: "Pool",
                newName: "IX_Pool_ContratoId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Magnitud",
                table: "Ratios",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(26,18)",
                oldPrecision: 26,
                oldScale: 18);

            migrationBuilder.AlterColumn<short>(
                name: "Status",
                table: "Documentos",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<short>(
                name: "Minimis",
                table: "Contratos",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "Digitalizada",
                table: "Contratos",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Magnitud",
                table: "Contabilidades",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(26,18)",
                oldPrecision: 26,
                oldScale: 18,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Magnitud",
                table: "Analiticas",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(26,18)",
                oldPrecision: 26,
                oldScale: 18,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 1,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(2082), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(2102), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(2082) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 2,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(2082), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(2111), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(2082) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 3,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(2082), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(2112), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(2082) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 1,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 2,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 3,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 4,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 5,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 6,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 7,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 8,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 9,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 10,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494), new DateTime(2022, 9, 20, 22, 29, 49, 356, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 48, 530, DateTimeKind.Utc).AddTicks(9605), "$2a$11$KiakzVHt.QHBbcfZNr9qI.AtgxKs8qc0KfTOm8UFgbzKiv.aTewM2", new DateTime(2022, 9, 20, 22, 29, 48, 530, DateTimeKind.Utc).AddTicks(9605) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 48, 530, DateTimeKind.Utc).AddTicks(9605), "$2a$11$CtwKj3p7ZKN79KyG3gWCnugezBu.IYnvEM4IsuGKizmngL8s88hqm", new DateTime(2022, 9, 20, 22, 29, 48, 530, DateTimeKind.Utc).AddTicks(9605) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 48, 530, DateTimeKind.Utc).AddTicks(9605), "$2a$11$dd..u9QcLWzz4gtP34LhueoyqV9x3EDsAXp9NAaIhOKB1qXr/KWeq", new DateTime(2022, 9, 20, 22, 29, 48, 530, DateTimeKind.Utc).AddTicks(9605) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 20, 22, 29, 48, 530, DateTimeKind.Utc).AddTicks(9605), "$2a$11$dN59JNz7q1Keir0b62gZ0OrdhOkMmaXkzn96RywKb.sFqLMHJ7e7C", new DateTime(2022, 9, 20, 22, 29, 48, 530, DateTimeKind.Utc).AddTicks(9605) });

            migrationBuilder.AddForeignKey(
                name: "FK_Pool_Contratos_ContratoId",
                table: "Pool",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "ContratoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pool_Documentos_DocumentoId",
                table: "Pool",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId");
        }
    }
}
