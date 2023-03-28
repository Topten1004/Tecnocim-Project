using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class AddEntidadesCore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    ContratoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Carencia = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Limite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    // Periodificacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Valoracion = table.Column<int>(type: "int", nullable: true),
                    Notas = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: true),
                    Digitalizada = table.Column<short>(type: "smallint", nullable: true),
                    Minimis = table.Column<short>(type: "smallint", nullable: true),
                    EquivalenciasEntidadId = table.Column<int>(type: "int", nullable: false),
                    EquivalenciasMonedaId = table.Column<int>(type: "int", nullable: false),
                    EquivalenciasProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.ContratoId);
                    table.ForeignKey(
                        name: "FK_Contratos_EquivalenciasEntidades_EquivalenciasEntidadId",
                        column: x => x.EquivalenciasEntidadId,
                        principalTable: "EquivalenciasEntidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contratos_EquivalenciasMonedas_EquivalenciasMonedaId",
                        column: x => x.EquivalenciasMonedaId,
                        principalTable: "EquivalenciasMonedas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contratos_EquivalenciasProductos_EquivalenciasProductoId",
                        column: x => x.EquivalenciasProductoId,
                        principalTable: "EquivalenciasProductos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    DocumentoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RutaDocumento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    ExtractorId = table.Column<int>(type: "int", nullable: true),
                    Origen = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.DocumentoId);
                    table.ForeignKey(
                        name: "FK_Documentos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaId");
                });

            migrationBuilder.CreateTable(
                name: "Analiticas",
                columns: table => new
                {
                    AnaliticaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cuenta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Magnitud = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DocumentoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analitica", x => x.AnaliticaId);
                    table.ForeignKey(
                        name: "FK_Analiticas_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "DocumentoId");
                });

            migrationBuilder.CreateTable(
                name: "Cirbes",
                columns: table => new
                {
                    CirbeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Dispuesto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Participantes = table.Column<int>(type: "int", nullable: false),
                    Importes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Demora = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Disponible = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContratoId = table.Column<long>(type: "bigint", nullable: true),
                    DocumentoId = table.Column<long>(type: "bigint", nullable: false),
                    EquivalenciasEntidadId = table.Column<int>(type: "int", nullable: false),
                    EquivalenciasMonedaId = table.Column<int>(type: "int", nullable: false),
                    EquivalenciasNatintervId = table.Column<int>(type: "int", nullable: false),
                    EquivalenciasPlazoId = table.Column<int>(type: "int", nullable: false),
                    EquivalenciasProductoId = table.Column<int>(type: "int", nullable: false),
                    EquivalenciasSituoperId = table.Column<int>(type: "int", nullable: true),
                    EquivalenciasSolcolId = table.Column<int>(type: "int", nullable: true),
                    EquivalenciasTipoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cirbe", x => x.CirbeId);
                    table.ForeignKey(
                        name: "FK_Cirbes_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "ContratoId");
                    table.ForeignKey(
                        name: "FK_Cirbes_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "DocumentoId");
                    table.ForeignKey(
                        name: "FK_Cirbes_EquivalenciasEntidades_EquivalenciasEntidadId",
                        column: x => x.EquivalenciasEntidadId,
                        principalTable: "EquivalenciasEntidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cirbes_EquivalenciasMonedas_EquivalenciasMonedaId",
                        column: x => x.EquivalenciasMonedaId,
                        principalTable: "EquivalenciasMonedas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cirbes_EquivalenciasNatintervs_EquivalenciasNatintervId",
                        column: x => x.EquivalenciasNatintervId,
                        principalTable: "EquivalenciasNatintervs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cirbes_EquivalenciasPlazos_EquivalenciasPlazoId",
                        column: x => x.EquivalenciasPlazoId,
                        principalTable: "EquivalenciasPlazos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cirbes_EquivalenciasProductos_EquivalenciasProductoId",
                        column: x => x.EquivalenciasProductoId,
                        principalTable: "EquivalenciasProductos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cirbes_EquivalenciasSituopers_EquivalenciasSituoperId",
                        column: x => x.EquivalenciasSituoperId,
                        principalTable: "EquivalenciasSituopers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cirbes_EquivalenciasSolcols_EquivalenciasSolcolId",
                        column: x => x.EquivalenciasSolcolId,
                        principalTable: "EquivalenciasSolcols",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cirbes_EquivalenciasTipos_EquivalenciasTipoId",
                        column: x => x.EquivalenciasTipoId,
                        principalTable: "EquivalenciasTipos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contabilidades",
                columns: table => new
                {
                    ContabilidadId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Concepto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Magnitud = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DocumentoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contabilidad", x => x.ContabilidadId);
                    table.ForeignKey(
                        name: "FK_Contabilidades_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "DocumentoId");
                });

            migrationBuilder.CreateTable(
                name: "Operaciones",
                columns: table => new
                {
                    OperacionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Momento = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    TextoOperacion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DocumentoId = table.Column<long>(type: "bigint", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacion", x => x.OperacionId);
                    table.ForeignKey(
                        name: "FK_Operaciones_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "DocumentoId");
                    table.ForeignKey(
                        name: "FK_Operaciones_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaId");
                    table.ForeignKey(
                        name: "FK_Operaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Pool",
                columns: table => new
                {
                    PoolId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cuenta = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Dispuesto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ContratoId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pool", x => x.PoolId);
                    table.ForeignKey(
                        name: "FK_Pool_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "ContratoId");
                    table.ForeignKey(
                        name: "FK_Pool_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "DocumentoId");
                });

            migrationBuilder.CreateTable(
                name: "Ratios",
                columns: table => new
                {
                    RatioId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Concepto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Magnitud = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratio", x => x.RatioId);
                    table.ForeignKey(
                        name: "FK_Ratios_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "DocumentoId");
                });

            migrationBuilder.CreateTable(
                name: "CirbePersonales",
                columns: table => new
                {
                    CirbePersonalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CirbeId = table.Column<int>(type: "int", nullable: false),
                    EquivalenciasPersonalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CirbePersonal", x => x.CirbePersonalId);
                    table.ForeignKey(
                        name: "FK_CirbePersonales_Cirbes_CirbeId",
                        column: x => x.CirbeId,
                        principalTable: "Cirbes",
                        principalColumn: "CirbeId");
                    table.ForeignKey(
                        name: "FK_CirbePersonales_EquivalenciasPersonales_EquivalenciasPersonalId",
                        column: x => x.EquivalenciasPersonalId,
                        principalTable: "EquivalenciasPersonales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CirbeReales",
                columns: table => new
                {
                    CirbeRealId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CirbeId = table.Column<int>(type: "int", nullable: false),
                    EquivalenciasRealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CirbeReal", x => x.CirbeRealId);
                    table.ForeignKey(
                        name: "FK_CirbeReales_Cirbes_CirbeId",
                        column: x => x.CirbeId,
                        principalTable: "Cirbes",
                        principalColumn: "CirbeId");
                    table.ForeignKey(
                        name: "FK_CirbeReales_EquivalenciasReales_EquivalenciasRealId",
                        column: x => x.EquivalenciasRealId,
                        principalTable: "EquivalenciasReales",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Analiticas_DocumentoId",
                table: "Analiticas",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CirbePersonales_CirbeId",
                table: "CirbePersonales",
                column: "CirbeId");

            migrationBuilder.CreateIndex(
                name: "IX_CirbePersonales_EquivalenciasPersonalId",
                table: "CirbePersonales",
                column: "EquivalenciasPersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_CirbeReales_CirbeId",
                table: "CirbeReales",
                column: "CirbeId");

            migrationBuilder.CreateIndex(
                name: "IX_CirbeReales_EquivalenciasRealId",
                table: "CirbeReales",
                column: "EquivalenciasRealId");

            migrationBuilder.CreateIndex(
                name: "IX_Cirbes_ContratoId",
                table: "Cirbes",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cirbes_DocumentoId",
                table: "Cirbes",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cirbes_EquivalenciasEntidadId",
                table: "Cirbes",
                column: "EquivalenciasEntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Cirbes_EquivalenciasMonedaId",
                table: "Cirbes",
                column: "EquivalenciasMonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cirbes_EquivalenciasNatintervId",
                table: "Cirbes",
                column: "EquivalenciasNatintervId");

            migrationBuilder.CreateIndex(
                name: "IX_Cirbes_EquivalenciasPlazoId",
                table: "Cirbes",
                column: "EquivalenciasPlazoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cirbes_EquivalenciasProductoId",
                table: "Cirbes",
                column: "EquivalenciasProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cirbes_EquivalenciasSituoperId",
                table: "Cirbes",
                column: "EquivalenciasSituoperId");

            migrationBuilder.CreateIndex(
                name: "IX_Cirbes_EquivalenciasSolcolId",
                table: "Cirbes",
                column: "EquivalenciasSolcolId");

            migrationBuilder.CreateIndex(
                name: "IX_Cirbes_EquivalenciasTipoId",
                table: "Cirbes",
                column: "EquivalenciasTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contabilidades_DocumentoId",
                table: "Contabilidades",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_EquivalenciasEntidadId",
                table: "Contratos",
                column: "EquivalenciasEntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_EquivalenciasMonedaId",
                table: "Contratos",
                column: "EquivalenciasMonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_EquivalenciasProductoId",
                table: "Contratos",
                column: "EquivalenciasProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_EmpresaId",
                table: "Documentos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_DocumentoId",
                table: "Operaciones",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_EmpresaId",
                table: "Operaciones",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_UsuarioId",
                table: "Operaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pool_ContratoId",
                table: "Pool",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pool_DocumentoId",
                table: "Pool",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratios_DocumentoId",
                table: "Ratios",
                column: "DocumentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analiticas");

            migrationBuilder.DropTable(
                name: "CirbePersonales");

            migrationBuilder.DropTable(
                name: "CirbeReales");

            migrationBuilder.DropTable(
                name: "Contabilidades");

            migrationBuilder.DropTable(
                name: "Operaciones");

            migrationBuilder.DropTable(
                name: "Pool");

            migrationBuilder.DropTable(
                name: "Ratios");

            migrationBuilder.DropTable(
                name: "Cirbes");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 1,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3907), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3923), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3907) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 2,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3907), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3925), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3907) });

            migrationBuilder.UpdateData(
                table: "EmpresaConfiguraciones",
                keyColumn: "EmpresaConfiguracionesId",
                keyValue: 3,
                columns: new[] { "Created", "Fecha", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3907), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3926), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3907) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 1,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 2,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 3,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 4,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 5,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 6,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 7,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 8,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 9,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "EmpresaId",
                keyValue: 10,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478), new DateTime(2022, 9, 17, 22, 48, 48, 170, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 47, 407, DateTimeKind.Utc).AddTicks(9036), "$2a$11$dmV5VCwQZ/jP3iCAfL6JeunZlgxQkvKtEI3OwtdjczG4wUIn2JRg6", new DateTime(2022, 9, 17, 22, 48, 47, 407, DateTimeKind.Utc).AddTicks(9036) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 47, 407, DateTimeKind.Utc).AddTicks(9036), "$2a$11$UBKMAjuSmJ9rNIbFFp0c6emgpYKjhVmDfsoOeQuQS79tFvuCUP31W", new DateTime(2022, 9, 17, 22, 48, 47, 407, DateTimeKind.Utc).AddTicks(9036) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 47, 407, DateTimeKind.Utc).AddTicks(9036), "$2a$11$RlZO599SBTEmYOa7cB0XUuacxJHUPJmXs/uaOGt3bB6U2FENrksOG", new DateTime(2022, 9, 17, 22, 48, 47, 407, DateTimeKind.Utc).AddTicks(9036) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2022, 9, 17, 22, 48, 47, 407, DateTimeKind.Utc).AddTicks(9036), "$2a$11$mflOU0l4BIjPUp0Wwbhoqet6PnCR4R6vsMwFEIeCn/UGWAyrGAZyW", new DateTime(2022, 9, 17, 22, 48, 47, 407, DateTimeKind.Utc).AddTicks(9036) });
        }
    }
}
