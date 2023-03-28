using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class AddAuditableEntityToCore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Ratios",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Ratios",
                type: "datetime2(3)",
                precision: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Ratios",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<long>(
                name: "ContratoId",
                table: "Pools",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Pools",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Pools",
                type: "datetime2(3)",
                precision: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Pools",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Operaciones",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Operaciones",
                type: "datetime2(3)",
                precision: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Operaciones",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Documentos",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Documentos",
                type: "datetime2(3)",
                precision: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Documentos",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Contratos",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Contratos",
                type: "datetime2(3)",
                precision: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Contratos",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Contabilidades",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Contabilidades",
                type: "datetime2(3)",
                precision: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Contabilidades",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Cirbes",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Cirbes",
                type: "datetime2(3)",
                precision: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Cirbes",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "CirbeReales",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "CirbeReales",
                type: "datetime2(3)",
                precision: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "CirbeReales",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "CirbePersonales",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "CirbePersonales",
                type: "datetime2(3)",
                precision: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "CirbePersonales",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Analiticas",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Analiticas",
                type: "datetime2(3)",
                precision: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Analiticas",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Ratios");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Ratios");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Ratios");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Pools");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Pools");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Pools");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Operaciones");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Operaciones");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Operaciones");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Contabilidades");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Contabilidades");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Contabilidades");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Cirbes");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Cirbes");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Cirbes");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "CirbeReales");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CirbeReales");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "CirbeReales");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "CirbePersonales");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CirbePersonales");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "CirbePersonales");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Analiticas");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Analiticas");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Analiticas");

            migrationBuilder.AlterColumn<long>(
                name: "ContratoId",
                table: "Pools",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$gHpVomBYOfeOsLUpa4G8POUI9WRUYuFghM.RFGLnw2WvivokopSnu");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$iWxRkcHk0y7B7Y3uZ4H3CuTWi5e1V5wwr73fxfAixdNIMSw5jEsGu");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$YH47IWbiAlWrDJBjnXSq1uRNVGIxLuNLNHvS8x3UMlPWhGp4JhdMG");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$95lukOtwB6N3DS06zY0IaOGIONFrapMbGJ7sS2DgpK8bDk/cYkoqG");
        }
    }
}
