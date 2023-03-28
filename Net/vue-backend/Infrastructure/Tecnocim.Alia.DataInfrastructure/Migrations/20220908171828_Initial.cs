using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CIF = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.EmpresaId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaConfiguraciones",
                columns: table => new
                {
                    EmpresaConfiguracionesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    PorDefecto = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FicheroConfiguracion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaConfiguraciones", x => x.EmpresaConfiguracionesId);
                    table.ForeignKey(
                        name: "FK_EmpresaConfiguraciones_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaId");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId");
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonRevoked = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioEmpresas",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioEmpresa", x => new { x.UsuarioId, x.EmpresaId });
                    table.ForeignKey(
                        name: "FK_UsuarioEmpresas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioEmpresas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "EmpresaId", "CIF", "Contacto", "Created", "Deleted", "Email", "Nombre", "Telefono", "Updated" },
                values: new object[,]
                {
                    { 1, "CIF 1", "Contacto 1", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), null, "info@empresa1.com", "Empresa 1", "654595851", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) },
                    { 2, "CIF 2", "Contacto 2", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), null, "info@empresa2.com", "Empresa 2", "654595852", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) },
                    { 3, "CIF 3", "Contacto 3", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), null, "info@empresa3.com", "Empresa 3", "654595853", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) },
                    { 4, "CIF 4", "Contacto 4", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), null, "info@empresa4.com", "Empresa 4", "654595854", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) },
                    { 5, "CIF 5", "Contacto 5", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), null, "info@empresa5.com", "Empresa 5", "654595855", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) },
                    { 6, "CIF 6", "Contacto 6", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), null, "info@empresa6.com", "Empresa 6", "654595856", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) },
                    { 7, "CIF 7", "Contacto 7", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), null, "info@empresa7.com", "Empresa 7", "654595857", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) },
                    { 8, "CIF 8", "Contacto 8", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), null, "info@empresa8.com", "Empresa 8", "654595858", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) },
                    { 9, "CIF 9", "Contacto 9", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), null, "info@empresa9.com", "Empresa 9", "654595859", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) },
                    { 10, "CIF 10", "Contacto 10", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592), null, "info@empresa10.com", "Empresa 10", "654595860", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5592) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RolId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Cliente" },
                    { 3, "Administrativo" },
                    { 4, "Financiero" }
                });

            migrationBuilder.InsertData(
                table: "EmpresaConfiguraciones",
                columns: new[] { "EmpresaConfiguracionesId", "Created", "Deleted", "EmpresaId", "Fecha", "FicheroConfiguracion", "PorDefecto", "Updated" },
                values: new object[] { 1, new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912), null, 1, new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5928), "fichero 1", true, new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912) });

            migrationBuilder.InsertData(
                table: "EmpresaConfiguraciones",
                columns: new[] { "EmpresaConfiguracionesId", "Created", "Deleted", "EmpresaId", "Fecha", "FicheroConfiguracion", "Updated" },
                values: new object[] { 2, new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912), null, 2, new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(6035), "fichero 2", new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912) });

            migrationBuilder.InsertData(
                table: "EmpresaConfiguraciones",
                columns: new[] { "EmpresaConfiguracionesId", "Created", "Deleted", "EmpresaId", "Fecha", "FicheroConfiguracion", "PorDefecto", "Updated" },
                values: new object[] { 3, new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912), null, 3, new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(6037), "fichero 3", true, new DateTime(2022, 9, 8, 17, 18, 28, 357, DateTimeKind.Utc).AddTicks(5912) });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Apellidos", "Created", "Deleted", "Email", "Nombre", "Password", "RolId", "Updated" },
                values: new object[,]
                {
                    { 1, "Apellidos Admin", new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254), null, "admin@alia.com", "Usuario Admin", "$2a$11$baizctOg5DEOiUBjmXMCRulo7lman0Fz8nztQ1QXLhR79V4LeA076", 1, new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254) },
                    { 2, "Apellidos Cliente 1", new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254), null, "cliente1@alia.com", "Usuario Cliente 1", "$2a$11$Z.OLJ8Er7eD0mgATrfqxf.Nt1ttIo0V6yVhdDjvyVr..CW/tObh7a", 2, new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254) },
                    { 3, "Apellidos Cliente 2", new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254), null, "cliente2@alia.com", "Usuario Cliente 2", "$2a$11$QLu01OkX7H5dMDXbVei10.Xl5R5s7OEVhnyKvACUnGU2.diIBgVB2", 2, new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254) },
                    { 4, "Apellidos Administrativo 1", new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254), null, "administrativo1@alia.com", "Usuario Administrativo 1", "$2a$11$2Jg0bPg/NIMCE2G3wM/Ew.QpJw2KwXySX.xHX3RQUBL5slG/TY2Zm", 3, new DateTime(2022, 9, 8, 17, 18, 27, 451, DateTimeKind.Utc).AddTicks(2254) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaConfiguraciones_EmpresaId",
                table: "EmpresaConfiguraciones",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UsuarioId",
                table: "RefreshTokens",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEmpresas_EmpresaId",
                table: "UsuarioEmpresas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpresaConfiguraciones");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UsuarioEmpresas");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
