using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class SoftDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analiticas_Documentos_DocumentoId",
                table: "Analiticas");

            migrationBuilder.DropForeignKey(
                name: "FK_CirbePersonales_Cirbes_CirbeId",
                table: "CirbePersonales");

            migrationBuilder.DropForeignKey(
                name: "FK_CirbeReales_Cirbes_CirbeId",
                table: "CirbeReales");

            migrationBuilder.DropForeignKey(
                name: "FK_Cirbes_Contratos_ContratoId",
                table: "Cirbes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cirbes_Documentos_DocumentoId",
                table: "Cirbes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contabilidades_Documentos_DocumentoId",
                table: "Contabilidades");

            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Empresas_EmpresaId",
                table: "Documentos");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpresaConfiguraciones_Empresas_EmpresaId",
                table: "EmpresaConfiguraciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Operaciones_Documentos_DocumentoId",
                table: "Operaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Pools_Contratos_ContratoId",
                table: "Pools");

            migrationBuilder.DropForeignKey(
                name: "FK_Pools_Documentos_DocumentoId",
                table: "Pools");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratios_Documentos_DocumentoId",
                table: "Ratios");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$s8809Tj/eQkuBC2heOFc1uUZ1fSb0S8NyKC6TMXcFHkEn8F436LB6");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$KaVBprObk/fhGXNwgNwEcOQ5npPvrRU67rJn8w0JcMT.xbGJ2H5dS");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$GKG2591tth3xNEJUiXLYreNT2I1n9bgCuYXkABfceQpKgYhrS9avq");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$UHgdZ8YiahE.sETojNJwVeDx.QuPO9G0loAv/816hGDFc6YGpGUXu");

            migrationBuilder.AddForeignKey(
                name: "FK_Analiticas_Documentos_DocumentoId",
                table: "Analiticas",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CirbePersonales_Cirbes_CirbeId",
                table: "CirbePersonales",
                column: "CirbeId",
                principalTable: "Cirbes",
                principalColumn: "CirbeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CirbeReales_Cirbes_CirbeId",
                table: "CirbeReales",
                column: "CirbeId",
                principalTable: "Cirbes",
                principalColumn: "CirbeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cirbes_Contratos_ContratoId",
                table: "Cirbes",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "ContratoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cirbes_Documentos_DocumentoId",
                table: "Cirbes",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contabilidades_Documentos_DocumentoId",
                table: "Contabilidades",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Empresas_EmpresaId",
                table: "Documentos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmpresaConfiguraciones_Empresas_EmpresaId",
                table: "EmpresaConfiguraciones",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operaciones_Documentos_DocumentoId",
                table: "Operaciones",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pools_Contratos_ContratoId",
                table: "Pools",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "ContratoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pools_Documentos_DocumentoId",
                table: "Pools",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratios_Documentos_DocumentoId",
                table: "Ratios",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analiticas_Documentos_DocumentoId",
                table: "Analiticas");

            migrationBuilder.DropForeignKey(
                name: "FK_CirbePersonales_Cirbes_CirbeId",
                table: "CirbePersonales");

            migrationBuilder.DropForeignKey(
                name: "FK_CirbeReales_Cirbes_CirbeId",
                table: "CirbeReales");

            migrationBuilder.DropForeignKey(
                name: "FK_Cirbes_Contratos_ContratoId",
                table: "Cirbes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cirbes_Documentos_DocumentoId",
                table: "Cirbes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contabilidades_Documentos_DocumentoId",
                table: "Contabilidades");

            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Empresas_EmpresaId",
                table: "Documentos");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpresaConfiguraciones_Empresas_EmpresaId",
                table: "EmpresaConfiguraciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Operaciones_Documentos_DocumentoId",
                table: "Operaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Pools_Contratos_ContratoId",
                table: "Pools");

            migrationBuilder.DropForeignKey(
                name: "FK_Pools_Documentos_DocumentoId",
                table: "Pools");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratios_Documentos_DocumentoId",
                table: "Ratios");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$TbkvNJ0QTZPERyky4bO7xuYe4iIBPstfkccmJ51LL.XvELJI4tUvi");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$ZtXCdTJV6ZQND5JShCAPk.8BOp8RD2Q5Q9yEV3wOa6Xx1v8mcmfC2");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$40a1T5UqEZ3.Fv8HUNgJU.S0H24IydJl3aHhMYldrYGTJOyQAXaCm");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$BI1fFtn.qT4sqjOzwUTg1.qdrblbv0RpUZdpo81pGZWtSfvxnFiiS");

            migrationBuilder.AddForeignKey(
                name: "FK_Analiticas_Documentos_DocumentoId",
                table: "Analiticas",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CirbePersonales_Cirbes_CirbeId",
                table: "CirbePersonales",
                column: "CirbeId",
                principalTable: "Cirbes",
                principalColumn: "CirbeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CirbeReales_Cirbes_CirbeId",
                table: "CirbeReales",
                column: "CirbeId",
                principalTable: "Cirbes",
                principalColumn: "CirbeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cirbes_Contratos_ContratoId",
                table: "Cirbes",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "ContratoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cirbes_Documentos_DocumentoId",
                table: "Cirbes",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contabilidades_Documentos_DocumentoId",
                table: "Contabilidades",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Empresas_EmpresaId",
                table: "Documentos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpresaConfiguraciones_Empresas_EmpresaId",
                table: "EmpresaConfiguraciones",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operaciones_Documentos_DocumentoId",
                table: "Operaciones",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Ratios_Documentos_DocumentoId",
                table: "Ratios",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "DocumentoId");
        }
    }
}
