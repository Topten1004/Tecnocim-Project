using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class ChangeRatiosMaestroForInterpretacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatioMaestros");

            migrationBuilder.CreateTable(
                name: "Interpretaciones",
                columns: table => new
                {
                    InterpretacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Concepto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ColorPositivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ColorNegativo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IconoPositivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IconoNegativo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tipo = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interpretacion", x => x.InterpretacionId);
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$opb8N6Eq5WEdN2Mw2bbpBOSuFiYBV13J852UliJpyRYhv0c.uNHSW");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$FdEKI9cehhd8bHVkUkqpteQ4aegs6Cc.Ph9uBUhEaqNYdF5cZZ3nS");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$XhYMs5g1qk0XzlBdr/q4q.1/nXSBpeTqPA.8JYEGnIOkUb9HSDtkK");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$/w8TL5EfNAQXZbATfln8X.IaLu.qfyWQGMhNKy11w.FKUpiNRho5G");

            migrationBuilder.CreateIndex(
                name: "IX_Interpretaciones_Concepto",
                table: "Interpretaciones",
                column: "Concepto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interpretaciones");

            migrationBuilder.CreateTable(
                name: "RatioMaestros",
                columns: table => new
                {
                    RatioMaestroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorNegativo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ColorPositivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Concepto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IconoNegativo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IconoPositivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatioMaestro", x => x.RatioMaestroId);
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Dah8t3MpSoGJPDJsvLMY6.uNGJsvZUmdZx8vuZm61ejllA9oictf6");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$qbgrXU/AJp2iR3K9hhaQL.UjhQp.pHTUK.tD0/T6BJhrhIYvW2jDK");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$z/FzsFL70OIbMielSBRT5O.dXzKtWrKoHwHObArELze.PrnHuNtGy");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$dfzpqS3INILOZN53ApEmH.GrGKziDClOzlHl25/jFfCX8xYnjnL9y");
        }
    }
}
