using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnocim.Alia.DataInfrastructure.Migrations
{
    public partial class SeedEquivalencias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasTipos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasSituopers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasReales",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasProductos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasPlazos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasPersonales",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasNatintervs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1, "0001" },
                    { 2, "0002" },
                    { 3, "0003" },
                    { 4, "0004" },
                    { 5, "0005" },
                    { 6, "0006" },
                    { 7, "0007" },
                    { 8, "0008" },
                    { 9, "0009" },
                    { 10, "0010" },
                    { 11, "0011" },
                    { 12, "0012" },
                    { 13, "0013" },
                    { 14, "0014" },
                    { 15, "0015" },
                    { 16, "0016" },
                    { 17, "0017" },
                    { 18, "0018" },
                    { 19, "0019" },
                    { 20, "0020" },
                    { 21, "0021" },
                    { 22, "0022" },
                    { 23, "0023" },
                    { 24, "0024" },
                    { 25, "0025" },
                    { 26, "0026" },
                    { 27, "0027" },
                    { 28, "0028" },
                    { 29, "0029" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 30, "0030" },
                    { 31, "0031" },
                    { 32, "0032" },
                    { 33, "0033" },
                    { 34, "0034" },
                    { 35, "0035" },
                    { 36, "0036" },
                    { 37, "0037" },
                    { 38, "0038" },
                    { 39, "0039" },
                    { 40, "0040" },
                    { 41, "0041" },
                    { 42, "0042" },
                    { 43, "0043" },
                    { 44, "0044" },
                    { 45, "0045" },
                    { 46, "0046" },
                    { 47, "0047" },
                    { 48, "0048" },
                    { 49, "0049" },
                    { 50, "0050" },
                    { 51, "0051" },
                    { 52, "0052" },
                    { 53, "0053" },
                    { 54, "0054" },
                    { 55, "0055" },
                    { 56, "0056" },
                    { 57, "0057" },
                    { 58, "0058" },
                    { 59, "0059" },
                    { 60, "0060" },
                    { 61, "0061" },
                    { 62, "0062" },
                    { 63, "0063" },
                    { 64, "0064" },
                    { 65, "0065" },
                    { 66, "0066" },
                    { 67, "0067" },
                    { 68, "0068" },
                    { 69, "0069" },
                    { 70, "0070" },
                    { 71, "0071" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 72, "0072" },
                    { 73, "0073" },
                    { 74, "0074" },
                    { 75, "0075" },
                    { 76, "0076" },
                    { 77, "0077" },
                    { 78, "0078" },
                    { 79, "0079" },
                    { 80, "0080" },
                    { 81, "0081" },
                    { 82, "0082" },
                    { 83, "0083" },
                    { 84, "0084" },
                    { 85, "0085" },
                    { 86, "0086" },
                    { 87, "0087" },
                    { 88, "0088" },
                    { 89, "0089" },
                    { 90, "0090" },
                    { 91, "0091" },
                    { 92, "0092" },
                    { 93, "0093" },
                    { 94, "0094" },
                    { 95, "0095" },
                    { 96, "0096" },
                    { 97, "0097" },
                    { 98, "0098" },
                    { 99, "0099" },
                    { 100, "0100" },
                    { 101, "0101" },
                    { 102, "0102" },
                    { 103, "0103" },
                    { 104, "0104" },
                    { 105, "0105" },
                    { 106, "0106" },
                    { 107, "0107" },
                    { 108, "0108" },
                    { 109, "0109" },
                    { 110, "0110" },
                    { 111, "0111" },
                    { 112, "0112" },
                    { 113, "0113" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 114, "0114" },
                    { 115, "0115" },
                    { 116, "0116" },
                    { 117, "0117" },
                    { 118, "0118" },
                    { 119, "0119" },
                    { 120, "0120" },
                    { 121, "0121" },
                    { 122, "0122" },
                    { 123, "0123" },
                    { 124, "0124" },
                    { 125, "0125" },
                    { 126, "0126" },
                    { 127, "0127" },
                    { 128, "0128" },
                    { 129, "0129" },
                    { 130, "0130" },
                    { 131, "0131" },
                    { 132, "0132" },
                    { 133, "0133" },
                    { 134, "0134" },
                    { 135, "0135" },
                    { 136, "0136" },
                    { 137, "0137" },
                    { 138, "0138" },
                    { 139, "0139" },
                    { 140, "0140" },
                    { 141, "0141" },
                    { 142, "0142" },
                    { 143, "0143" },
                    { 144, "0144" },
                    { 145, "0145" },
                    { 146, "0146" },
                    { 147, "0147" },
                    { 148, "0148" },
                    { 149, "0149" },
                    { 150, "0150" },
                    { 151, "0151" },
                    { 152, "0152" },
                    { 153, "0153" },
                    { 154, "0154" },
                    { 155, "0155" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 156, "0156" },
                    { 157, "0157" },
                    { 158, "0158" },
                    { 159, "0159" },
                    { 160, "0160" },
                    { 161, "0161" },
                    { 162, "0162" },
                    { 163, "0163" },
                    { 164, "0164" },
                    { 165, "0165" },
                    { 166, "0166" },
                    { 167, "0167" },
                    { 168, "0168" },
                    { 169, "0169" },
                    { 170, "0170" },
                    { 171, "0171" },
                    { 172, "0172" },
                    { 173, "0173" },
                    { 174, "0174" },
                    { 175, "0175" },
                    { 176, "0176" },
                    { 177, "0177" },
                    { 178, "0178" },
                    { 179, "0179" },
                    { 180, "0180" },
                    { 181, "0181" },
                    { 182, "0182" },
                    { 183, "0183" },
                    { 184, "0184" },
                    { 185, "0185" },
                    { 186, "0186" },
                    { 187, "0187" },
                    { 188, "0188" },
                    { 189, "0189" },
                    { 190, "0190" },
                    { 191, "0191" },
                    { 192, "0192" },
                    { 193, "0193" },
                    { 194, "0194" },
                    { 195, "0195" },
                    { 196, "0196" },
                    { 197, "0197" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 198, "0198" },
                    { 199, "0199" },
                    { 200, "0200" },
                    { 201, "0201" },
                    { 202, "0202" },
                    { 203, "0203" },
                    { 204, "0205" },
                    { 205, "0206" },
                    { 206, "0207" },
                    { 207, "0208" },
                    { 208, "0209" },
                    { 209, "0210" },
                    { 210, "0211" },
                    { 211, "0212" },
                    { 212, "0213" },
                    { 213, "0214" },
                    { 214, "0215" },
                    { 215, "0216" },
                    { 216, "0217" },
                    { 217, "0218" },
                    { 218, "0219" },
                    { 219, "0220" },
                    { 220, "0221" },
                    { 221, "0222" },
                    { 222, "0223" },
                    { 223, "0224" },
                    { 224, "0225" },
                    { 225, "0226" },
                    { 226, "0227" },
                    { 227, "0228" },
                    { 228, "0229" },
                    { 229, "0230" },
                    { 230, "0231" },
                    { 231, "0232" },
                    { 232, "0233" },
                    { 233, "0234" },
                    { 234, "0235" },
                    { 235, "0236" },
                    { 236, "0237" },
                    { 237, "0238" },
                    { 238, "0239" },
                    { 239, "0240" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 240, "0241" },
                    { 241, "0242" },
                    { 242, "0243" },
                    { 243, "0482" },
                    { 244, "0483" },
                    { 245, "0484" },
                    { 246, "0485" },
                    { 247, "0486" },
                    { 248, "0487" },
                    { 249, "0488" },
                    { 250, "0489" },
                    { 251, "0490" },
                    { 252, "1000" },
                    { 253, "1001" },
                    { 254, "1002" },
                    { 255, "1003" },
                    { 256, "1004" },
                    { 257, "1005" },
                    { 258, "1101" },
                    { 259, "1102" },
                    { 260, "1103" },
                    { 261, "1104" },
                    { 262, "1105" },
                    { 263, "1106" },
                    { 264, "1107" },
                    { 265, "1108" },
                    { 266, "1109" },
                    { 267, "1110" },
                    { 268, "1111" },
                    { 269, "1112" },
                    { 270, "1113" },
                    { 271, "1114" },
                    { 272, "1115" },
                    { 273, "1116" },
                    { 274, "1117" },
                    { 275, "1118" },
                    { 276, "1119" },
                    { 277, "1120" },
                    { 278, "1121" },
                    { 279, "1122" },
                    { 280, "1123" },
                    { 281, "1124" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 282, "1125" },
                    { 283, "1126" },
                    { 284, "1127" },
                    { 285, "1128" },
                    { 286, "1129" },
                    { 287, "1130" },
                    { 288, "1131" },
                    { 289, "1132" },
                    { 290, "1133" },
                    { 291, "1134" },
                    { 292, "1135" },
                    { 293, "1136" },
                    { 294, "1137" },
                    { 295, "1138" },
                    { 296, "1139" },
                    { 297, "1140" },
                    { 298, "1141" },
                    { 299, "1142" },
                    { 300, "1143" },
                    { 301, "1144" },
                    { 302, "1145" },
                    { 303, "1146" },
                    { 304, "1147" },
                    { 305, "1148" },
                    { 306, "1149" },
                    { 307, "1150" },
                    { 308, "1151" },
                    { 309, "1152" },
                    { 310, "1153" },
                    { 311, "1154" },
                    { 312, "1155" },
                    { 313, "1156" },
                    { 314, "1157" },
                    { 315, "1158" },
                    { 316, "1159" },
                    { 317, "1160" },
                    { 318, "1161" },
                    { 319, "1162" },
                    { 320, "1163" },
                    { 321, "1164" },
                    { 322, "1165" },
                    { 323, "1166" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 324, "1167" },
                    { 325, "1168" },
                    { 326, "1169" },
                    { 327, "1170" },
                    { 328, "1171" },
                    { 329, "1172" },
                    { 330, "1173" },
                    { 331, "1174" },
                    { 332, "1176" },
                    { 333, "1177" },
                    { 334, "1178" },
                    { 335, "1179" },
                    { 336, "1180" },
                    { 337, "1181" },
                    { 338, "1182" },
                    { 339, "1183" },
                    { 340, "1184" },
                    { 341, "1185" },
                    { 342, "1186" },
                    { 343, "1187" },
                    { 344, "1188" },
                    { 345, "1189" },
                    { 346, "1190" },
                    { 347, "1191" },
                    { 348, "1192" },
                    { 349, "1193" },
                    { 350, "1194" },
                    { 351, "1195" },
                    { 352, "1196" },
                    { 353, "1197" },
                    { 354, "1198" },
                    { 355, "1199" },
                    { 356, "1200" },
                    { 357, "1201" },
                    { 358, "1202" },
                    { 359, "1203" },
                    { 360, "1204" },
                    { 361, "1205" },
                    { 362, "1206" },
                    { 363, "1207" },
                    { 364, "1208" },
                    { 365, "1209" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 366, "1210" },
                    { 367, "1211" },
                    { 368, "1212" },
                    { 369, "1213" },
                    { 370, "1214" },
                    { 371, "1215" },
                    { 372, "1216" },
                    { 373, "1217" },
                    { 374, "1218" },
                    { 375, "1219" },
                    { 376, "1220" },
                    { 377, "1221" },
                    { 378, "1222" },
                    { 379, "1223" },
                    { 380, "1224" },
                    { 381, "1225" },
                    { 382, "1226" },
                    { 383, "1227" },
                    { 384, "1228" },
                    { 385, "1229" },
                    { 386, "1230" },
                    { 387, "1231" },
                    { 388, "1232" },
                    { 389, "1233" },
                    { 390, "1234" },
                    { 391, "1235" },
                    { 392, "1236" },
                    { 393, "1237" },
                    { 394, "1238" },
                    { 395, "1239" },
                    { 396, "1240" },
                    { 397, "1241" },
                    { 398, "1242" },
                    { 399, "1243" },
                    { 400, "1244" },
                    { 401, "1245" },
                    { 402, "1246" },
                    { 403, "1247" },
                    { 404, "1248" },
                    { 405, "1249" },
                    { 406, "1250" },
                    { 407, "1251" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 408, "1252" },
                    { 409, "1253" },
                    { 410, "1254" },
                    { 411, "1255" },
                    { 412, "1256" },
                    { 413, "1257" },
                    { 414, "1258" },
                    { 415, "1259" },
                    { 416, "1260" },
                    { 417, "1261" },
                    { 418, "1262" },
                    { 419, "1263" },
                    { 420, "1264" },
                    { 421, "1265" },
                    { 422, "1266" },
                    { 423, "1267" },
                    { 424, "1268" },
                    { 425, "1269" },
                    { 426, "1270" },
                    { 427, "1271" },
                    { 428, "1272" },
                    { 429, "1273" },
                    { 430, "1274" },
                    { 431, "1275" },
                    { 432, "1276" },
                    { 433, "1277" },
                    { 434, "1278" },
                    { 435, "1279" },
                    { 436, "1280" },
                    { 437, "1281" },
                    { 438, "1282" },
                    { 439, "1283" },
                    { 440, "1284" },
                    { 441, "1285" },
                    { 442, "1286" },
                    { 443, "1287" },
                    { 444, "1288" },
                    { 445, "1289" },
                    { 446, "1290" },
                    { 447, "1291" },
                    { 448, "1292" },
                    { 449, "1293" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 450, "1294" },
                    { 451, "1295" },
                    { 452, "1296" },
                    { 453, "1297" },
                    { 454, "1298" },
                    { 455, "1299" },
                    { 456, "1300" },
                    { 457, "1301" },
                    { 458, "1302" },
                    { 459, "1310" },
                    { 460, "1311" },
                    { 461, "1312" },
                    { 462, "1313" },
                    { 463, "1314" },
                    { 464, "1315" },
                    { 465, "1316" },
                    { 466, "1317" },
                    { 467, "1318" },
                    { 468, "1319" },
                    { 469, "1320" },
                    { 470, "1321" },
                    { 471, "1322" },
                    { 472, "1323" },
                    { 473, "1451" },
                    { 474, "1452" },
                    { 475, "1453" },
                    { 476, "1454" },
                    { 477, "1455" },
                    { 478, "1456" },
                    { 479, "1457" },
                    { 480, "1458" },
                    { 481, "1459" },
                    { 482, "1460" },
                    { 483, "1461" },
                    { 484, "1462" },
                    { 485, "1463" },
                    { 486, "1464" },
                    { 487, "1465" },
                    { 488, "1466" },
                    { 489, "1467" },
                    { 490, "1468" },
                    { 491, "1469" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 492, "1470" },
                    { 493, "1471" },
                    { 494, "1472" },
                    { 495, "1473" },
                    { 496, "1474" },
                    { 497, "1475" },
                    { 498, "1476" },
                    { 499, "1477" },
                    { 500, "1478" },
                    { 501, "1479" },
                    { 502, "1480" },
                    { 503, "1481" },
                    { 504, "1482" },
                    { 505, "1483" },
                    { 506, "1484" },
                    { 507, "1485" },
                    { 508, "1486" },
                    { 509, "1487" },
                    { 510, "1488" },
                    { 511, "1489" },
                    { 512, "1490" },
                    { 513, "1491" },
                    { 514, "1492" },
                    { 515, "1493" },
                    { 516, "1494" },
                    { 517, "1495" },
                    { 518, "1496" },
                    { 519, "1497" },
                    { 520, "1498" },
                    { 521, "1499" },
                    { 522, "1500" },
                    { 523, "1501" },
                    { 524, "1502" },
                    { 525, "1503" },
                    { 526, "1504" },
                    { 527, "1505" },
                    { 528, "1506" },
                    { 529, "1507" },
                    { 530, "1508" },
                    { 531, "1509" },
                    { 532, "1510" },
                    { 533, "1511" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 534, "1512" },
                    { 535, "1513" },
                    { 536, "1514" },
                    { 537, "1515" },
                    { 538, "1516" },
                    { 539, "1517" },
                    { 540, "1518" },
                    { 541, "1519" },
                    { 542, "1520" },
                    { 543, "1521" },
                    { 544, "1522" },
                    { 545, "1523" },
                    { 546, "1524" },
                    { 547, "1525" },
                    { 548, "1526" },
                    { 549, "1527" },
                    { 550, "1528" },
                    { 551, "1529" },
                    { 552, "1530" },
                    { 553, "1531" },
                    { 554, "1532" },
                    { 555, "1533" },
                    { 556, "1534" },
                    { 557, "1535" },
                    { 558, "1536" },
                    { 559, "1537" },
                    { 560, "1538" },
                    { 561, "1539" },
                    { 562, "1540" },
                    { 563, "1541" },
                    { 564, "1542" },
                    { 565, "1543" },
                    { 566, "1544" },
                    { 567, "1545" },
                    { 568, "1546" },
                    { 569, "1547" },
                    { 570, "1548" },
                    { 571, "1549" },
                    { 572, "1550" },
                    { 573, "1551" },
                    { 574, "1552" },
                    { 575, "1553" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 576, "1554" },
                    { 577, "1555" },
                    { 578, "1556" },
                    { 579, "1557" },
                    { 580, "1558" },
                    { 581, "1559" },
                    { 582, "1560" },
                    { 583, "1561" },
                    { 584, "1562" },
                    { 585, "1563" },
                    { 586, "1564" },
                    { 587, "1565" },
                    { 588, "1566" },
                    { 589, "1567" },
                    { 590, "1568" },
                    { 591, "1569" },
                    { 592, "1570" },
                    { 593, "1571" },
                    { 594, "1572" },
                    { 595, "1573" },
                    { 596, "1574" },
                    { 597, "1575" },
                    { 598, "1576" },
                    { 599, "1577" },
                    { 600, "1578" },
                    { 601, "1579" },
                    { 602, "1580" },
                    { 603, "1701" },
                    { 604, "1702" },
                    { 605, "1703" },
                    { 606, "1704" },
                    { 607, "1705" },
                    { 608, "1706" },
                    { 609, "1707" },
                    { 610, "1708" },
                    { 611, "1709" },
                    { 612, "1710" },
                    { 613, "1711" },
                    { 614, "1712" },
                    { 615, "1713" },
                    { 616, "1714" },
                    { 617, "1715" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 618, "1716" },
                    { 619, "1717" },
                    { 620, "1718" },
                    { 621, "1719" },
                    { 622, "1720" },
                    { 623, "1721" },
                    { 624, "1722" },
                    { 625, "1723" },
                    { 626, "1724" },
                    { 627, "1725" },
                    { 628, "1726" },
                    { 629, "1727" },
                    { 630, "1728" },
                    { 631, "1729" },
                    { 632, "1730" },
                    { 633, "1731" },
                    { 634, "1732" },
                    { 635, "1733" },
                    { 636, "1734" },
                    { 637, "1735" },
                    { 638, "1736" },
                    { 639, "1737" },
                    { 640, "1738" },
                    { 641, "1739" },
                    { 642, "1740" },
                    { 643, "1741" },
                    { 644, "1742" },
                    { 645, "1743" },
                    { 646, "1744" },
                    { 647, "1745" },
                    { 648, "1746" },
                    { 649, "1747" },
                    { 650, "1748" },
                    { 651, "1749" },
                    { 652, "1750" },
                    { 653, "1751" },
                    { 654, "1752" },
                    { 655, "1753" },
                    { 656, "1754" },
                    { 657, "1755" },
                    { 658, "1756" },
                    { 659, "1757" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 660, "1758" },
                    { 661, "1759" },
                    { 662, "1760" },
                    { 663, "1761" },
                    { 664, "1762" },
                    { 665, "1763" },
                    { 666, "1764" },
                    { 667, "1765" },
                    { 668, "1766" },
                    { 669, "1767" },
                    { 670, "1768" },
                    { 671, "1769" },
                    { 672, "1770" },
                    { 673, "1771" },
                    { 674, "1772" },
                    { 675, "1773" },
                    { 676, "1774" },
                    { 677, "1775" },
                    { 678, "1776" },
                    { 679, "1777" },
                    { 680, "1778" },
                    { 681, "1779" },
                    { 682, "1780" },
                    { 683, "1781" },
                    { 684, "1782" },
                    { 685, "1783" },
                    { 686, "1784" },
                    { 687, "1785" },
                    { 688, "1786" },
                    { 689, "1787" },
                    { 690, "1788" },
                    { 691, "1789" },
                    { 692, "1790" },
                    { 693, "1791" },
                    { 694, "1792" },
                    { 695, "1793" },
                    { 696, "1794" },
                    { 697, "1795" },
                    { 698, "1796" },
                    { 699, "1797" },
                    { 700, "1798" },
                    { 701, "1799" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 702, "2000" },
                    { 703, "2001" },
                    { 704, "2002" },
                    { 705, "2003" },
                    { 706, "2004" },
                    { 707, "2005" },
                    { 708, "2006" },
                    { 709, "2007" },
                    { 710, "2008" },
                    { 711, "2009" },
                    { 712, "2010" },
                    { 713, "2011" },
                    { 714, "2012" },
                    { 715, "2013" },
                    { 716, "2014" },
                    { 717, "2015" },
                    { 718, "2016" },
                    { 719, "2017" },
                    { 720, "2018" },
                    { 721, "2019" },
                    { 722, "2020" },
                    { 723, "2021" },
                    { 724, "2022" },
                    { 725, "2023" },
                    { 726, "2024" },
                    { 727, "2025" },
                    { 728, "2026" },
                    { 729, "2027" },
                    { 730, "2028" },
                    { 731, "2029" },
                    { 732, "2030" },
                    { 733, "2031" },
                    { 734, "2032" },
                    { 735, "2033" },
                    { 736, "2034" },
                    { 737, "2035" },
                    { 738, "2036" },
                    { 739, "2037" },
                    { 740, "2038" },
                    { 741, "2039" },
                    { 742, "2040" },
                    { 743, "2041" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 744, "2042" },
                    { 745, "2043" },
                    { 746, "2044" },
                    { 747, "2045" },
                    { 748, "2046" },
                    { 749, "2047" },
                    { 750, "2048" },
                    { 751, "2049" },
                    { 752, "2050" },
                    { 753, "2051" },
                    { 754, "2052" },
                    { 755, "2053" },
                    { 756, "2054" },
                    { 757, "2055" },
                    { 758, "2056" },
                    { 759, "2057" },
                    { 760, "2058" },
                    { 761, "2059" },
                    { 762, "2060" },
                    { 763, "2061" },
                    { 764, "2062" },
                    { 765, "2063" },
                    { 766, "2064" },
                    { 767, "2065" },
                    { 768, "2066" },
                    { 769, "2067" },
                    { 770, "2068" },
                    { 771, "2069" },
                    { 772, "2070" },
                    { 773, "2071" },
                    { 774, "2072" },
                    { 775, "2073" },
                    { 776, "2074" },
                    { 777, "2075" },
                    { 778, "2076" },
                    { 779, "2077" },
                    { 780, "2078" },
                    { 781, "2079" },
                    { 782, "2080" },
                    { 783, "2081" },
                    { 784, "2082" },
                    { 785, "2083" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 786, "2084" },
                    { 787, "2085" },
                    { 788, "2086" },
                    { 789, "2087" },
                    { 790, "2088" },
                    { 791, "2089" },
                    { 792, "2090" },
                    { 793, "2091" },
                    { 794, "2092" },
                    { 795, "2093" },
                    { 796, "2094" },
                    { 797, "2095" },
                    { 798, "2096" },
                    { 799, "2097" },
                    { 800, "2098" },
                    { 801, "2099" },
                    { 802, "2100" },
                    { 803, "2101" },
                    { 804, "2102" },
                    { 805, "2103" },
                    { 806, "2104" },
                    { 807, "2105" },
                    { 808, "2106" },
                    { 809, "2107" },
                    { 810, "2108" },
                    { 811, "2401" },
                    { 812, "2402" },
                    { 813, "2403" },
                    { 814, "2404" },
                    { 815, "2405" },
                    { 816, "2406" },
                    { 817, "2407" },
                    { 818, "2408" },
                    { 819, "2409" },
                    { 820, "2410" },
                    { 821, "2411" },
                    { 822, "2412" },
                    { 823, "2413" },
                    { 824, "2414" },
                    { 825, "2415" },
                    { 826, "2416" },
                    { 827, "2417" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 828, "2418" },
                    { 829, "2419" },
                    { 830, "2420" },
                    { 831, "2421" },
                    { 832, "2422" },
                    { 833, "2423" },
                    { 834, "2424" },
                    { 835, "2425" },
                    { 836, "2426" },
                    { 837, "2427" },
                    { 838, "2428" },
                    { 839, "2429" },
                    { 840, "2430" },
                    { 841, "2431" },
                    { 842, "2432" },
                    { 843, "2433" },
                    { 844, "3000" },
                    { 845, "3001" },
                    { 846, "3002" },
                    { 847, "3003" },
                    { 848, "3004" },
                    { 849, "3005" },
                    { 850, "3006" },
                    { 851, "3007" },
                    { 852, "3008" },
                    { 853, "3009" },
                    { 854, "3010" },
                    { 855, "3011" },
                    { 856, "3012" },
                    { 857, "3013" },
                    { 858, "3014" },
                    { 859, "3015" },
                    { 860, "3016" },
                    { 861, "3017" },
                    { 862, "3018" },
                    { 863, "3019" },
                    { 864, "3020" },
                    { 865, "3021" },
                    { 866, "3022" },
                    { 867, "3023" },
                    { 868, "3024" },
                    { 869, "3025" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 870, "3026" },
                    { 871, "3027" },
                    { 872, "3028" },
                    { 873, "3029" },
                    { 874, "3030" },
                    { 875, "3031" },
                    { 876, "3032" },
                    { 877, "3033" },
                    { 878, "3034" },
                    { 879, "3035" },
                    { 880, "3036" },
                    { 881, "3037" },
                    { 882, "3038" },
                    { 883, "3039" },
                    { 884, "3040" },
                    { 885, "3041" },
                    { 886, "3042" },
                    { 887, "3043" },
                    { 888, "3044" },
                    { 889, "3045" },
                    { 890, "3046" },
                    { 891, "3047" },
                    { 892, "3048" },
                    { 893, "3049" },
                    { 894, "3050" },
                    { 895, "3051" },
                    { 896, "3052" },
                    { 897, "3053" },
                    { 898, "3054" },
                    { 899, "3055" },
                    { 900, "3056" },
                    { 901, "3057" },
                    { 902, "3058" },
                    { 903, "3059" },
                    { 904, "3060" },
                    { 905, "3061" },
                    { 906, "3062" },
                    { 907, "3063" },
                    { 908, "3064" },
                    { 909, "3065" },
                    { 910, "3066" },
                    { 911, "3067" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 912, "3068" },
                    { 913, "3069" },
                    { 914, "3070" },
                    { 915, "3071" },
                    { 916, "3072" },
                    { 917, "3073" },
                    { 918, "3074" },
                    { 919, "3075" },
                    { 920, "3076" },
                    { 921, "3077" },
                    { 922, "3078" },
                    { 923, "3079" },
                    { 924, "3080" },
                    { 925, "3081" },
                    { 926, "3082" },
                    { 927, "3083" },
                    { 928, "3084" },
                    { 929, "3085" },
                    { 930, "3086" },
                    { 931, "3087" },
                    { 932, "3088" },
                    { 933, "3089" },
                    { 934, "3090" },
                    { 935, "3091" },
                    { 936, "3092" },
                    { 937, "3093" },
                    { 938, "3094" },
                    { 939, "3095" },
                    { 940, "3096" },
                    { 941, "3097" },
                    { 942, "3098" },
                    { 943, "3099" },
                    { 944, "3100" },
                    { 945, "3101" },
                    { 946, "3102" },
                    { 947, "3103" },
                    { 948, "3104" },
                    { 949, "3105" },
                    { 950, "3106" },
                    { 951, "3107" },
                    { 952, "3108" },
                    { 953, "3109" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 954, "3110" },
                    { 955, "3111" },
                    { 956, "3112" },
                    { 957, "3113" },
                    { 958, "3114" },
                    { 959, "3115" },
                    { 960, "3116" },
                    { 961, "3117" },
                    { 962, "3118" },
                    { 963, "3119" },
                    { 964, "3121" },
                    { 965, "3122" },
                    { 966, "3123" },
                    { 967, "3124" },
                    { 968, "3125" },
                    { 969, "3126" },
                    { 970, "3127" },
                    { 971, "3128" },
                    { 972, "3129" },
                    { 973, "3130" },
                    { 974, "3131" },
                    { 975, "3132" },
                    { 976, "3133" },
                    { 977, "3134" },
                    { 978, "3135" },
                    { 979, "3136" },
                    { 980, "3137" },
                    { 981, "3138" },
                    { 982, "3139" },
                    { 983, "3140" },
                    { 984, "3141" },
                    { 985, "3142" },
                    { 986, "3143" },
                    { 987, "3144" },
                    { 988, "3145" },
                    { 989, "3146" },
                    { 990, "3147" },
                    { 991, "3148" },
                    { 992, "3149" },
                    { 993, "3150" },
                    { 994, "3151" },
                    { 995, "3152" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 996, "3153" },
                    { 997, "3154" },
                    { 998, "3155" },
                    { 999, "3156" },
                    { 1000, "3157" },
                    { 1001, "3158" },
                    { 1002, "3159" },
                    { 1003, "3160" },
                    { 1004, "3161" },
                    { 1005, "3162" },
                    { 1006, "3163" },
                    { 1007, "3164" },
                    { 1008, "3165" },
                    { 1009, "3166" },
                    { 1010, "3167" },
                    { 1011, "3168" },
                    { 1012, "3169" },
                    { 1013, "3170" },
                    { 1014, "3171" },
                    { 1015, "3172" },
                    { 1016, "3173" },
                    { 1017, "3174" },
                    { 1018, "3175" },
                    { 1019, "3176" },
                    { 1020, "3177" },
                    { 1021, "3178" },
                    { 1022, "3179" },
                    { 1023, "3180" },
                    { 1024, "3181" },
                    { 1025, "3182" },
                    { 1026, "3183" },
                    { 1027, "3184" },
                    { 1028, "3185" },
                    { 1029, "3186" },
                    { 1030, "3187" },
                    { 1031, "3188" },
                    { 1032, "3189" },
                    { 1033, "3190" },
                    { 1034, "3191" },
                    { 1035, "3498" },
                    { 1036, "3499" },
                    { 1037, "4001" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1038, "4002" },
                    { 1039, "4003" },
                    { 1040, "4004" },
                    { 1041, "4005" },
                    { 1042, "4006" },
                    { 1043, "4007" },
                    { 1044, "4008" },
                    { 1045, "4009" },
                    { 1046, "4010" },
                    { 1047, "4011" },
                    { 1048, "4012" },
                    { 1049, "4013" },
                    { 1050, "4301" },
                    { 1051, "4302" },
                    { 1052, "4303" },
                    { 1053, "4304" },
                    { 1054, "4305" },
                    { 1055, "4307" },
                    { 1056, "4308" },
                    { 1057, "4309" },
                    { 1058, "4310" },
                    { 1059, "4311" },
                    { 1060, "4312" },
                    { 1061, "4313" },
                    { 1062, "4314" },
                    { 1063, "4315" },
                    { 1064, "4316" },
                    { 1065, "4317" },
                    { 1066, "4318" },
                    { 1067, "4319" },
                    { 1068, "4320" },
                    { 1069, "4321" },
                    { 1070, "4322" },
                    { 1071, "4323" },
                    { 1072, "4324" },
                    { 1073, "4325" },
                    { 1074, "4326" },
                    { 1075, "4327" },
                    { 1076, "4328" },
                    { 1077, "4329" },
                    { 1078, "4330" },
                    { 1079, "4331" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1080, "4332" },
                    { 1081, "4333" },
                    { 1082, "4334" },
                    { 1083, "4335" },
                    { 1084, "4336" },
                    { 1085, "4337" },
                    { 1086, "4338" },
                    { 1087, "4339" },
                    { 1088, "4340" },
                    { 1089, "4341" },
                    { 1090, "4342" },
                    { 1091, "4343" },
                    { 1092, "4344" },
                    { 1093, "4345" },
                    { 1094, "4347" },
                    { 1095, "4348" },
                    { 1096, "4349" },
                    { 1097, "4350" },
                    { 1098, "4351" },
                    { 1099, "4352" },
                    { 1100, "4353" },
                    { 1101, "4354" },
                    { 1102, "4355" },
                    { 1103, "4356" },
                    { 1104, "4357" },
                    { 1105, "4358" },
                    { 1106, "4359" },
                    { 1107, "4360" },
                    { 1108, "4361" },
                    { 1109, "4362" },
                    { 1110, "4363" },
                    { 1111, "4364" },
                    { 1112, "4365" },
                    { 1113, "4366" },
                    { 1114, "4367" },
                    { 1115, "4368" },
                    { 1116, "4369" },
                    { 1117, "4370" },
                    { 1118, "4371" },
                    { 1119, "4372" },
                    { 1120, "4373" },
                    { 1121, "4374" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1122, "4375" },
                    { 1123, "4376" },
                    { 1124, "4377" },
                    { 1125, "4378" },
                    { 1126, "4379" },
                    { 1127, "4380" },
                    { 1128, "4381" },
                    { 1129, "4382" },
                    { 1130, "4383" },
                    { 1131, "4384" },
                    { 1132, "4385" },
                    { 1133, "4386" },
                    { 1134, "4387" },
                    { 1135, "4388" },
                    { 1136, "4389" },
                    { 1137, "4390" },
                    { 1138, "4391" },
                    { 1139, "4392" },
                    { 1140, "4393" },
                    { 1141, "4394" },
                    { 1142, "4395" },
                    { 1143, "4396" },
                    { 1144, "4397" },
                    { 1145, "4398" },
                    { 1146, "4399" },
                    { 1147, "4400" },
                    { 1148, "4401" },
                    { 1149, "4402" },
                    { 1150, "4403" },
                    { 1151, "4404" },
                    { 1152, "4405" },
                    { 1153, "4406" },
                    { 1154, "4407" },
                    { 1155, "4408" },
                    { 1156, "4409" },
                    { 1157, "4410" },
                    { 1158, "4411" },
                    { 1159, "4412" },
                    { 1160, "4413" },
                    { 1161, "4414" },
                    { 1162, "4415" },
                    { 1163, "4416" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1164, "4417" },
                    { 1165, "4418" },
                    { 1166, "4419" },
                    { 1167, "4420" },
                    { 1168, "4421" },
                    { 1169, "4422" },
                    { 1170, "4423" },
                    { 1171, "4424" },
                    { 1172, "4425" },
                    { 1173, "4426" },
                    { 1174, "4427" },
                    { 1175, "4428" },
                    { 1176, "4429" },
                    { 1177, "4430" },
                    { 1178, "4431" },
                    { 1179, "4432" },
                    { 1180, "4433" },
                    { 1181, "4434" },
                    { 1182, "4435" },
                    { 1183, "4436" },
                    { 1184, "4437" },
                    { 1185, "4438" },
                    { 1186, "4439" },
                    { 1187, "4440" },
                    { 1188, "4441" },
                    { 1189, "4442" },
                    { 1190, "4443" },
                    { 1191, "4444" },
                    { 1192, "4445" },
                    { 1193, "4446" },
                    { 1194, "4447" },
                    { 1195, "4448" },
                    { 1196, "4449" },
                    { 1197, "4450" },
                    { 1198, "4451" },
                    { 1199, "4452" },
                    { 1200, "4453" },
                    { 1201, "4454" },
                    { 1202, "4455" },
                    { 1203, "4456" },
                    { 1204, "4457" },
                    { 1205, "4458" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1206, "4459" },
                    { 1207, "4460" },
                    { 1208, "4461" },
                    { 1209, "4462" },
                    { 1210, "4463" },
                    { 1211, "4464" },
                    { 1212, "4465" },
                    { 1213, "4466" },
                    { 1214, "4467" },
                    { 1215, "4468" },
                    { 1216, "4469" },
                    { 1217, "4470" },
                    { 1218, "4471" },
                    { 1219, "4472" },
                    { 1220, "4473" },
                    { 1221, "4474" },
                    { 1222, "4475" },
                    { 1223, "4476" },
                    { 1224, "4477" },
                    { 1225, "4478" },
                    { 1226, "4479" },
                    { 1227, "4480" },
                    { 1228, "4481" },
                    { 1229, "4482" },
                    { 1230, "4483" },
                    { 1231, "4484" },
                    { 1232, "4485" },
                    { 1233, "4486" },
                    { 1234, "4487" },
                    { 1235, "4488" },
                    { 1236, "4489" },
                    { 1237, "4490" },
                    { 1238, "4491" },
                    { 1239, "4492" },
                    { 1240, "4493" },
                    { 1241, "4494" },
                    { 1242, "4495" },
                    { 1243, "4496" },
                    { 1244, "4497" },
                    { 1245, "4498" },
                    { 1246, "4499" },
                    { 1247, "4630" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1248, "4631" },
                    { 1249, "4632" },
                    { 1250, "4633" },
                    { 1251, "4701" },
                    { 1252, "4702" },
                    { 1253, "4703" },
                    { 1254, "4704" },
                    { 1255, "4705" },
                    { 1256, "4706" },
                    { 1257, "4707" },
                    { 1258, "4708" },
                    { 1259, "4709" },
                    { 1260, "4710" },
                    { 1261, "4711" },
                    { 1262, "4712" },
                    { 1263, "4713" },
                    { 1264, "4714" },
                    { 1265, "4715" },
                    { 1266, "4716" },
                    { 1267, "4717" },
                    { 1268, "4718" },
                    { 1269, "4719" },
                    { 1270, "4720" },
                    { 1271, "4721" },
                    { 1272, "4722" },
                    { 1273, "4723" },
                    { 1274, "4724" },
                    { 1275, "4725" },
                    { 1276, "4726" },
                    { 1277, "4727" },
                    { 1278, "4728" },
                    { 1279, "4729" },
                    { 1280, "4730" },
                    { 1281, "4731" },
                    { 1282, "4732" },
                    { 1283, "4733" },
                    { 1284, "4734" },
                    { 1285, "4735" },
                    { 1286, "4736" },
                    { 1287, "4737" },
                    { 1288, "4738" },
                    { 1289, "4739" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1290, "4740" },
                    { 1291, "4741" },
                    { 1292, "4742" },
                    { 1293, "4743" },
                    { 1294, "4744" },
                    { 1295, "4745" },
                    { 1296, "4746" },
                    { 1297, "4747" },
                    { 1298, "4748" },
                    { 1299, "4749" },
                    { 1300, "4750" },
                    { 1301, "4751" },
                    { 1302, "4752" },
                    { 1303, "4753" },
                    { 1304, "4754" },
                    { 1305, "4755" },
                    { 1306, "4756" },
                    { 1307, "4757" },
                    { 1308, "4758" },
                    { 1309, "4759" },
                    { 1310, "4760" },
                    { 1311, "4761" },
                    { 1312, "4762" },
                    { 1313, "4763" },
                    { 1314, "4764" },
                    { 1315, "4765" },
                    { 1316, "4766" },
                    { 1317, "4767" },
                    { 1318, "4768" },
                    { 1319, "4769" },
                    { 1320, "4770" },
                    { 1321, "4771" },
                    { 1322, "4772" },
                    { 1323, "4773" },
                    { 1324, "4774" },
                    { 1325, "4775" },
                    { 1326, "4776" },
                    { 1327, "4777" },
                    { 1328, "4778" },
                    { 1329, "4779" },
                    { 1330, "4780" },
                    { 1331, "4781" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1332, "4782" },
                    { 1333, "4783" },
                    { 1334, "4784" },
                    { 1335, "4785" },
                    { 1336, "4786" },
                    { 1337, "4787" },
                    { 1338, "4788" },
                    { 1339, "4789" },
                    { 1340, "4790" },
                    { 1341, "4791" },
                    { 1342, "4792" },
                    { 1343, "4793" },
                    { 1344, "4794" },
                    { 1345, "4795" },
                    { 1346, "4796" },
                    { 1347, "4797" },
                    { 1348, "4798" },
                    { 1349, "4799" },
                    { 1350, "4800" },
                    { 1351, "4801" },
                    { 1352, "4802" },
                    { 1353, "4803" },
                    { 1354, "4804" },
                    { 1355, "4805" },
                    { 1356, "4806" },
                    { 1357, "4807" },
                    { 1358, "4808" },
                    { 1359, "4809" },
                    { 1360, "4810" },
                    { 1361, "4811" },
                    { 1362, "4812" },
                    { 1363, "4813" },
                    { 1364, "4814" },
                    { 1365, "4815" },
                    { 1366, "4816" },
                    { 1367, "4817" },
                    { 1368, "4818" },
                    { 1369, "4819" },
                    { 1370, "4820" },
                    { 1371, "4821" },
                    { 1372, "4822" },
                    { 1373, "4823" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1374, "4824" },
                    { 1375, "4825" },
                    { 1376, "4826" },
                    { 1377, "4827" },
                    { 1378, "4828" },
                    { 1379, "4829" },
                    { 1380, "4830" },
                    { 1381, "4831" },
                    { 1382, "4832" },
                    { 1383, "4833" },
                    { 1384, "4834" },
                    { 1385, "4835" },
                    { 1386, "4836" },
                    { 1387, "4837" },
                    { 1388, "4838" },
                    { 1389, "4839" },
                    { 1390, "6701" },
                    { 1391, "6702" },
                    { 1392, "6703" },
                    { 1393, "6704" },
                    { 1394, "6705" },
                    { 1395, "6706" },
                    { 1396, "6707" },
                    { 1397, "6709" },
                    { 1398, "6710" },
                    { 1399, "6711" },
                    { 1400, "6712" },
                    { 1401, "6713" },
                    { 1402, "6714" },
                    { 1403, "6715" },
                    { 1404, "6716" },
                    { 1405, "6717" },
                    { 1406, "6718" },
                    { 1407, "6719" },
                    { 1408, "6720" },
                    { 1409, "6802" },
                    { 1410, "6803" },
                    { 1411, "6804" },
                    { 1412, "6805" },
                    { 1413, "6806" },
                    { 1414, "6807" },
                    { 1415, "6808" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1416, "6809" },
                    { 1417, "6810" },
                    { 1418, "6811" },
                    { 1419, "6812" },
                    { 1420, "6813" },
                    { 1421, "6814" },
                    { 1422, "6815" },
                    { 1423, "6816" },
                    { 1424, "6817" },
                    { 1425, "6818" },
                    { 1426, "6819" },
                    { 1427, "6820" },
                    { 1428, "6821" },
                    { 1429, "6822" },
                    { 1430, "6823" },
                    { 1431, "6824" },
                    { 1432, "6825" },
                    { 1433, "6826" },
                    { 1434, "6827" },
                    { 1435, "6828" },
                    { 1436, "6829" },
                    { 1437, "6830" },
                    { 1438, "6831" },
                    { 1439, "6832" },
                    { 1440, "6833" },
                    { 1441, "6834" },
                    { 1442, "6835" },
                    { 1443, "6836" },
                    { 1444, "6837" },
                    { 1445, "6838" },
                    { 1446, "6839" },
                    { 1447, "6840" },
                    { 1448, "6841" },
                    { 1449, "6842" },
                    { 1450, "6843" },
                    { 1451, "6844" },
                    { 1452, "6845" },
                    { 1453, "6846" },
                    { 1454, "6847" },
                    { 1455, "6848" },
                    { 1456, "6849" },
                    { 1457, "6850" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1458, "6851" },
                    { 1459, "6852" },
                    { 1460, "6854" },
                    { 1461, "6855" },
                    { 1462, "6856" },
                    { 1463, "6857" },
                    { 1464, "6858" },
                    { 1465, "6859" },
                    { 1466, "6860" },
                    { 1467, "6861" },
                    { 1468, "6862" },
                    { 1469, "6863" },
                    { 1470, "6866" },
                    { 1471, "6867" },
                    { 1472, "6868" },
                    { 1473, "6869" },
                    { 1474, "6870" },
                    { 1475, "6871" },
                    { 1476, "6872" },
                    { 1477, "6873" },
                    { 1478, "6874" },
                    { 1479, "6875" },
                    { 1480, "6876" },
                    { 1481, "6877" },
                    { 1482, "6878" },
                    { 1483, "6879" },
                    { 1484, "6880" },
                    { 1485, "6881" },
                    { 1486, "6882" },
                    { 1487, "6883" },
                    { 1488, "6884" },
                    { 1489, "6885" },
                    { 1490, "6886" },
                    { 1491, "6887" },
                    { 1492, "6888" },
                    { 1493, "6889" },
                    { 1494, "6890" },
                    { 1495, "6891" },
                    { 1496, "6892" },
                    { 1497, "6893" },
                    { 1498, "6894" },
                    { 1499, "6895" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1500, "6896" },
                    { 1501, "6897" },
                    { 1502, "6898" },
                    { 1503, "6899" },
                    { 1504, "6900" },
                    { 1505, "6901" },
                    { 1506, "6902" },
                    { 1507, "6903" },
                    { 1508, "6904" },
                    { 1509, "6905" },
                    { 1510, "6906" },
                    { 1511, "6907" },
                    { 1512, "6908" },
                    { 1513, "6909" },
                    { 1514, "6910" },
                    { 1515, "6911" },
                    { 1516, "6912" },
                    { 1517, "6913" },
                    { 1518, "6914" },
                    { 1519, "6915" },
                    { 1520, "6916" },
                    { 1521, "7861" },
                    { 1522, "7862" },
                    { 1523, "7863" },
                    { 1524, "7864" },
                    { 1525, "7865" },
                    { 1526, "7866" },
                    { 1527, "7867" },
                    { 1528, "7868" },
                    { 1529, "7869" },
                    { 1530, "8201" },
                    { 1531, "8202" },
                    { 1532, "8203" },
                    { 1533, "8204" },
                    { 1534, "8205" },
                    { 1535, "8206" },
                    { 1536, "8207" },
                    { 1537, "8208" },
                    { 1538, "8209" },
                    { 1539, "8210" },
                    { 1540, "8211" },
                    { 1541, "8212" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1542, "8214" },
                    { 1543, "8215" },
                    { 1544, "8216" },
                    { 1545, "8217" },
                    { 1546, "8218" },
                    { 1547, "8219" },
                    { 1548, "8220" },
                    { 1549, "8221" },
                    { 1550, "8222" },
                    { 1551, "8223" },
                    { 1552, "8224" },
                    { 1553, "8225" },
                    { 1554, "8226" },
                    { 1555, "8227" },
                    { 1556, "8228" },
                    { 1557, "8229" },
                    { 1558, "8230" },
                    { 1559, "8231" },
                    { 1560, "8232" },
                    { 1561, "8233" },
                    { 1562, "8234" },
                    { 1563, "8235" },
                    { 1564, "8236" },
                    { 1565, "8237" },
                    { 1566, "8238" },
                    { 1567, "8239" },
                    { 1568, "8240" },
                    { 1569, "8300" },
                    { 1570, "8302" },
                    { 1571, "8303" },
                    { 1572, "8304" },
                    { 1573, "8305" },
                    { 1574, "8306" },
                    { 1575, "8307" },
                    { 1576, "8308" },
                    { 1577, "8309" },
                    { 1578, "8310" },
                    { 1579, "8311" },
                    { 1580, "8312" },
                    { 1581, "8313" },
                    { 1582, "8314" },
                    { 1583, "8315" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1584, "8316" },
                    { 1585, "8317" },
                    { 1586, "8318" },
                    { 1587, "8319" },
                    { 1588, "8320" },
                    { 1589, "8321" },
                    { 1590, "8322" },
                    { 1591, "8323" },
                    { 1592, "8324" },
                    { 1593, "8325" },
                    { 1594, "8326" },
                    { 1595, "8327" },
                    { 1596, "8328" },
                    { 1597, "8329" },
                    { 1598, "8330" },
                    { 1599, "8331" },
                    { 1600, "8332" },
                    { 1601, "8333" },
                    { 1602, "8334" },
                    { 1603, "8335" },
                    { 1604, "8336" },
                    { 1605, "8337" },
                    { 1606, "8338" },
                    { 1607, "8339" },
                    { 1608, "8340" },
                    { 1609, "8341" },
                    { 1610, "8342" },
                    { 1611, "8343" },
                    { 1612, "8344" },
                    { 1613, "8345" },
                    { 1614, "8346" },
                    { 1615, "8347" },
                    { 1616, "8348" },
                    { 1617, "8349" },
                    { 1618, "8350" },
                    { 1619, "8351" },
                    { 1620, "8352" },
                    { 1621, "8353" },
                    { 1622, "8354" },
                    { 1623, "8355" },
                    { 1624, "8356" },
                    { 1625, "8357" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1626, "8358" },
                    { 1627, "8359" },
                    { 1628, "8360" },
                    { 1629, "8361" },
                    { 1630, "8362" },
                    { 1631, "8363" },
                    { 1632, "8364" },
                    { 1633, "8365" },
                    { 1634, "8366" },
                    { 1635, "8367" },
                    { 1636, "8368" },
                    { 1637, "8369" },
                    { 1638, "8370" },
                    { 1639, "8371" },
                    { 1640, "8372" },
                    { 1641, "8373" },
                    { 1642, "8374" },
                    { 1643, "8375" },
                    { 1644, "8376" },
                    { 1645, "8377" },
                    { 1646, "8378" },
                    { 1647, "8379" },
                    { 1648, "8380" },
                    { 1649, "8381" },
                    { 1650, "8382" },
                    { 1651, "8383" },
                    { 1652, "8384" },
                    { 1653, "8385" },
                    { 1654, "8386" },
                    { 1655, "8387" },
                    { 1656, "8388" },
                    { 1657, "8389" },
                    { 1658, "8390" },
                    { 1659, "8391" },
                    { 1660, "8392" },
                    { 1661, "8393" },
                    { 1662, "8394" },
                    { 1663, "8395" },
                    { 1664, "8396" },
                    { 1665, "8397" },
                    { 1666, "8398" },
                    { 1667, "8399" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1668, "8400" },
                    { 1669, "8401" },
                    { 1670, "8402" },
                    { 1671, "8403" },
                    { 1672, "8404" },
                    { 1673, "8405" },
                    { 1674, "8406" },
                    { 1675, "8407" },
                    { 1676, "8408" },
                    { 1677, "8409" },
                    { 1678, "8410" },
                    { 1679, "8411" },
                    { 1680, "8412" },
                    { 1681, "8413" },
                    { 1682, "8414" },
                    { 1683, "8415" },
                    { 1684, "8416" },
                    { 1685, "8417" },
                    { 1686, "8418" },
                    { 1687, "8419" },
                    { 1688, "8420" },
                    { 1689, "8421" },
                    { 1690, "8422" },
                    { 1691, "8423" },
                    { 1692, "8424" },
                    { 1693, "8425" },
                    { 1694, "8426" },
                    { 1695, "8427" },
                    { 1696, "8428" },
                    { 1697, "8429" },
                    { 1698, "8430" },
                    { 1699, "8431" },
                    { 1700, "8432" },
                    { 1701, "8433" },
                    { 1702, "8434" },
                    { 1703, "8435" },
                    { 1704, "8436" },
                    { 1705, "8437" },
                    { 1706, "8438" },
                    { 1707, "8439" },
                    { 1708, "8440" },
                    { 1709, "8441" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1710, "8442" },
                    { 1711, "8443" },
                    { 1712, "8444" },
                    { 1713, "8445" },
                    { 1714, "8446" },
                    { 1715, "8447" },
                    { 1716, "8448" },
                    { 1717, "8449" },
                    { 1718, "8450" },
                    { 1719, "8451" },
                    { 1720, "8452" },
                    { 1721, "8453" },
                    { 1722, "8454" },
                    { 1723, "8455" },
                    { 1724, "8456" },
                    { 1725, "8457" },
                    { 1726, "8458" },
                    { 1727, "8459" },
                    { 1728, "8460" },
                    { 1729, "8461" },
                    { 1730, "8462" },
                    { 1731, "8463" },
                    { 1732, "8464" },
                    { 1733, "8465" },
                    { 1734, "8466" },
                    { 1735, "8467" },
                    { 1736, "8468" },
                    { 1737, "8469" },
                    { 1738, "8470" },
                    { 1739, "8471" },
                    { 1740, "8472" },
                    { 1741, "8473" },
                    { 1742, "8474" },
                    { 1743, "8475" },
                    { 1744, "8476" },
                    { 1745, "8477" },
                    { 1746, "8478" },
                    { 1747, "8479" },
                    { 1748, "8480" },
                    { 1749, "8481" },
                    { 1750, "8482" },
                    { 1751, "8483" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1752, "8484" },
                    { 1753, "8485" },
                    { 1754, "8486" },
                    { 1755, "8487" },
                    { 1756, "8488" },
                    { 1757, "8489" },
                    { 1758, "8490" },
                    { 1759, "8492" },
                    { 1760, "8493" },
                    { 1761, "8494" },
                    { 1762, "8495" },
                    { 1763, "8496" },
                    { 1764, "8497" },
                    { 1765, "8498" },
                    { 1766, "8499" },
                    { 1767, "8500" },
                    { 1768, "8501" },
                    { 1769, "8502" },
                    { 1770, "8503" },
                    { 1771, "8504" },
                    { 1772, "8505" },
                    { 1773, "8506" },
                    { 1774, "8507" },
                    { 1775, "8508" },
                    { 1776, "8509" },
                    { 1777, "8510" },
                    { 1778, "8511" },
                    { 1779, "8512" },
                    { 1780, "8513" },
                    { 1781, "8514" },
                    { 1782, "8515" },
                    { 1783, "8516" },
                    { 1784, "8517" },
                    { 1785, "8518" },
                    { 1786, "8519" },
                    { 1787, "8520" },
                    { 1788, "8521" },
                    { 1789, "8522" },
                    { 1790, "8523" },
                    { 1791, "8524" },
                    { 1792, "8525" },
                    { 1793, "8526" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1794, "8527" },
                    { 1795, "8528" },
                    { 1796, "8529" },
                    { 1797, "8530" },
                    { 1798, "8531" },
                    { 1799, "8532" },
                    { 1800, "8533" },
                    { 1801, "8534" },
                    { 1802, "8535" },
                    { 1803, "8536" },
                    { 1804, "8537" },
                    { 1805, "8538" },
                    { 1806, "8539" },
                    { 1807, "8540" },
                    { 1808, "8541" },
                    { 1809, "8542" },
                    { 1810, "8543" },
                    { 1811, "8544" },
                    { 1812, "8545" },
                    { 1813, "8546" },
                    { 1814, "8547" },
                    { 1815, "8548" },
                    { 1816, "8550" },
                    { 1817, "8551" },
                    { 1818, "8552" },
                    { 1819, "8553" },
                    { 1820, "8554" },
                    { 1821, "8555" },
                    { 1822, "8556" },
                    { 1823, "8557" },
                    { 1824, "8558" },
                    { 1825, "8559" },
                    { 1826, "8560" },
                    { 1827, "8561" },
                    { 1828, "8562" },
                    { 1829, "8563" },
                    { 1830, "8564" },
                    { 1831, "8565" },
                    { 1832, "8566" },
                    { 1833, "8567" },
                    { 1834, "8568" },
                    { 1835, "8569" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1836, "8570" },
                    { 1837, "8571" },
                    { 1838, "8572" },
                    { 1839, "8573" },
                    { 1840, "8574" },
                    { 1841, "8575" },
                    { 1842, "8576" },
                    { 1843, "8577" },
                    { 1844, "8578" },
                    { 1845, "8579" },
                    { 1846, "8580" },
                    { 1847, "8581" },
                    { 1848, "8582" },
                    { 1849, "8583" },
                    { 1850, "8584" },
                    { 1851, "8585" },
                    { 1852, "8586" },
                    { 1853, "8587" },
                    { 1854, "8588" },
                    { 1855, "8589" },
                    { 1856, "8590" },
                    { 1857, "8591" },
                    { 1858, "8592" },
                    { 1859, "8593" },
                    { 1860, "8594" },
                    { 1861, "8595" },
                    { 1862, "8596" },
                    { 1863, "8597" },
                    { 1864, "8598" },
                    { 1865, "8599" },
                    { 1866, "8600" },
                    { 1867, "8601" },
                    { 1868, "8602" },
                    { 1869, "8603" },
                    { 1870, "8604" },
                    { 1871, "8605" },
                    { 1872, "8606" },
                    { 1873, "8607" },
                    { 1874, "8608" },
                    { 1875, "8609" },
                    { 1876, "8610" },
                    { 1877, "8611" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1878, "8612" },
                    { 1879, "8613" },
                    { 1880, "8614" },
                    { 1881, "8615" },
                    { 1882, "8616" },
                    { 1883, "8617" },
                    { 1884, "8618" },
                    { 1885, "8619" },
                    { 1886, "8620" },
                    { 1887, "8621" },
                    { 1888, "8622" },
                    { 1889, "8623" },
                    { 1890, "8624" },
                    { 1891, "8625" },
                    { 1892, "8626" },
                    { 1893, "8627" },
                    { 1894, "8628" },
                    { 1895, "8629" },
                    { 1896, "8630" },
                    { 1897, "8631" },
                    { 1898, "8632" },
                    { 1899, "8633" },
                    { 1900, "8634" },
                    { 1901, "8635" },
                    { 1902, "8636" },
                    { 1903, "8637" },
                    { 1904, "8638" },
                    { 1905, "8639" },
                    { 1906, "8640" },
                    { 1907, "8641" },
                    { 1908, "8642" },
                    { 1909, "8643" },
                    { 1910, "8644" },
                    { 1911, "8645" },
                    { 1912, "8646" },
                    { 1913, "8647" },
                    { 1914, "8648" },
                    { 1915, "8649" },
                    { 1916, "8650" },
                    { 1917, "8651" },
                    { 1918, "8652" },
                    { 1919, "8653" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1920, "8654" },
                    { 1921, "8655" },
                    { 1922, "8656" },
                    { 1923, "8657" },
                    { 1924, "8658" },
                    { 1925, "8659" },
                    { 1926, "8660" },
                    { 1927, "8661" },
                    { 1928, "8662" },
                    { 1929, "8663" },
                    { 1930, "8664" },
                    { 1931, "8665" },
                    { 1932, "8666" },
                    { 1933, "8667" },
                    { 1934, "8668" },
                    { 1935, "8669" },
                    { 1936, "8670" },
                    { 1937, "8671" },
                    { 1938, "8672" },
                    { 1939, "8673" },
                    { 1940, "8674" },
                    { 1941, "8675" },
                    { 1942, "8676" },
                    { 1943, "8677" },
                    { 1944, "8678" },
                    { 1945, "8679" },
                    { 1946, "8680" },
                    { 1947, "8681" },
                    { 1948, "8682" },
                    { 1949, "8683" },
                    { 1950, "8684" },
                    { 1951, "8685" },
                    { 1952, "8686" },
                    { 1953, "8687" },
                    { 1954, "8688" },
                    { 1955, "8689" },
                    { 1956, "8690" },
                    { 1957, "8691" },
                    { 1958, "8692" },
                    { 1959, "8693" },
                    { 1960, "8694" },
                    { 1961, "8695" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 1962, "8696" },
                    { 1963, "8697" },
                    { 1964, "8698" },
                    { 1965, "8699" },
                    { 1966, "8700" },
                    { 1967, "8701" },
                    { 1968, "8702" },
                    { 1969, "8703" },
                    { 1970, "8704" },
                    { 1971, "8705" },
                    { 1972, "8706" },
                    { 1973, "8707" },
                    { 1974, "8708" },
                    { 1975, "8709" },
                    { 1976, "8710" },
                    { 1977, "8711" },
                    { 1978, "8712" },
                    { 1979, "8713" },
                    { 1980, "8714" },
                    { 1981, "8715" },
                    { 1982, "8716" },
                    { 1983, "8717" },
                    { 1984, "8718" },
                    { 1985, "8719" },
                    { 1986, "8720" },
                    { 1987, "8721" },
                    { 1988, "8722" },
                    { 1989, "8723" },
                    { 1990, "8724" },
                    { 1991, "8725" },
                    { 1992, "8726" },
                    { 1993, "8727" },
                    { 1994, "8728" },
                    { 1995, "8729" },
                    { 1996, "8730" },
                    { 1997, "8731" },
                    { 1998, "8732" },
                    { 1999, "8733" },
                    { 2000, "8734" },
                    { 2001, "8735" },
                    { 2002, "8736" },
                    { 2003, "8737" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 2004, "8738" },
                    { 2005, "8739" },
                    { 2006, "8740" },
                    { 2007, "8741" },
                    { 2008, "8742" },
                    { 2009, "8743" },
                    { 2010, "8744" },
                    { 2011, "8745" },
                    { 2012, "8746" },
                    { 2013, "8747" },
                    { 2014, "8748" },
                    { 2015, "8749" },
                    { 2016, "8750" },
                    { 2017, "8751" },
                    { 2018, "8752" },
                    { 2019, "8753" },
                    { 2020, "8754" },
                    { 2021, "8755" },
                    { 2022, "8756" },
                    { 2023, "8757" },
                    { 2024, "8758" },
                    { 2025, "8759" },
                    { 2026, "8760" },
                    { 2027, "8761" },
                    { 2028, "8762" },
                    { 2029, "8763" },
                    { 2030, "8764" },
                    { 2031, "8765" },
                    { 2032, "8766" },
                    { 2033, "8767" },
                    { 2034, "8768" },
                    { 2035, "8769" },
                    { 2036, "8770" },
                    { 2037, "8771" },
                    { 2038, "8772" },
                    { 2039, "8773" },
                    { 2040, "8774" },
                    { 2041, "8775" },
                    { 2042, "8776" },
                    { 2043, "8777" },
                    { 2044, "8778" },
                    { 2045, "8779" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 2046, "8780" },
                    { 2047, "8781" },
                    { 2048, "8782" },
                    { 2049, "8783" },
                    { 2050, "8784" },
                    { 2051, "8785" },
                    { 2052, "8786" },
                    { 2053, "8787" },
                    { 2054, "8788" },
                    { 2055, "8789" },
                    { 2056, "8790" },
                    { 2057, "8791" },
                    { 2058, "8792" },
                    { 2059, "8793" },
                    { 2060, "8794" },
                    { 2061, "8795" },
                    { 2062, "8796" },
                    { 2063, "8797" },
                    { 2064, "8798" },
                    { 2065, "8799" },
                    { 2066, "8800" },
                    { 2067, "8801" },
                    { 2068, "8802" },
                    { 2069, "8803" },
                    { 2070, "8804" },
                    { 2071, "8805" },
                    { 2072, "8806" },
                    { 2073, "8807" },
                    { 2074, "8808" },
                    { 2075, "8809" },
                    { 2076, "8810" },
                    { 2077, "8811" },
                    { 2078, "8812" },
                    { 2079, "8813" },
                    { 2080, "8814" },
                    { 2081, "8815" },
                    { 2082, "8816" },
                    { 2083, "8817" },
                    { 2084, "8818" },
                    { 2085, "8819" },
                    { 2086, "8820" },
                    { 2087, "8821" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 2088, "8822" },
                    { 2089, "8823" },
                    { 2090, "8824" },
                    { 2091, "8825" },
                    { 2092, "8826" },
                    { 2093, "8827" },
                    { 2094, "8828" },
                    { 2095, "8829" },
                    { 2096, "8830" },
                    { 2097, "8831" },
                    { 2098, "8832" },
                    { 2099, "8833" },
                    { 2100, "8834" },
                    { 2101, "8835" },
                    { 2102, "8836" },
                    { 2103, "8837" },
                    { 2104, "8838" },
                    { 2105, "8839" },
                    { 2106, "8840" },
                    { 2107, "8841" },
                    { 2108, "8901" },
                    { 2109, "8902" },
                    { 2110, "8903" },
                    { 2111, "8904" },
                    { 2112, "8905" },
                    { 2113, "8906" },
                    { 2114, "8907" },
                    { 2115, "8908" },
                    { 2116, "8909" },
                    { 2117, "8910" },
                    { 2118, "8911" },
                    { 2119, "8912" },
                    { 2120, "8913" },
                    { 2121, "8914" },
                    { 2122, "8915" },
                    { 2123, "8916" },
                    { 2124, "8917" },
                    { 2125, "8918" },
                    { 2126, "9801" },
                    { 2127, "9802" },
                    { 2128, "9803" },
                    { 2129, "9804" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 2130, "9805" },
                    { 2131, "9806" },
                    { 2132, "9807" },
                    { 2133, "9808" },
                    { 2134, "9809" },
                    { 2135, "9810" },
                    { 2136, "9811" },
                    { 2137, "9812" },
                    { 2138, "9813" },
                    { 2139, "9814" },
                    { 2140, "9815" },
                    { 2141, "9816" },
                    { 2142, "9817" },
                    { 2143, "9818" },
                    { 2144, "9819" },
                    { 2145, "9820" },
                    { 2146, "9821" },
                    { 2147, "9822" },
                    { 2148, "9823" },
                    { 2149, "9824" },
                    { 2150, "9825" },
                    { 2151, "9826" },
                    { 2152, "9827" },
                    { 2153, "9828" },
                    { 2154, "9829" },
                    { 2155, "9830" },
                    { 2156, "9831" },
                    { 2157, "9832" },
                    { 2158, "9833" },
                    { 2159, "9834" },
                    { 2160, "9836" },
                    { 2161, "9837" },
                    { 2162, "9838" },
                    { 2163, "9839" },
                    { 2164, "9840" },
                    { 2165, "9841" },
                    { 2166, "9842" },
                    { 2167, "9843" },
                    { 2168, "9844" },
                    { 2169, "9845" },
                    { 2170, "9846" },
                    { 2171, "9847" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasEntidades",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { 2172, "9848" },
                    { 2173, "9849" },
                    { 2174, "9850" },
                    { 2175, "9851" },
                    { 2176, "9890" },
                    { 2177, "9891" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasMonedas",
                columns: new[] { "Id", "Tipo" },
                values: new object[,]
                {
                    { 1, "AED" },
                    { 2, "AOA" },
                    { 3, "ARS" },
                    { 4, "ATS" },
                    { 5, "AUD" },
                    { 6, "BEF" },
                    { 7, "BGN" },
                    { 8, "BRL" },
                    { 9, "CAD" },
                    { 10, "CHF" },
                    { 11, "CLP" },
                    { 12, "CNY" },
                    { 13, "COP" },
                    { 14, "CYP" },
                    { 15, "CZK" },
                    { 16, "DEM" },
                    { 17, "DKK" },
                    { 18, "DZD" },
                    { 19, "EEK" },
                    { 20, "EGP" },
                    { 21, "ESB" },
                    { 22, "ESP" },
                    { 23, "EUR" },
                    { 24, "FIM" },
                    { 25, "FRF" },
                    { 26, "GBP" },
                    { 27, "GRD" },
                    { 28, "HKD" },
                    { 29, "HRK" },
                    { 30, "HUF" },
                    { 31, "IDR" },
                    { 32, "IEP" },
                    { 33, "ILS" },
                    { 34, "INR" },
                    { 35, "ISK" },
                    { 36, "ITL" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasMonedas",
                columns: new[] { "Id", "Tipo" },
                values: new object[,]
                {
                    { 37, "JPY" },
                    { 38, "KES" },
                    { 39, "KRW" },
                    { 40, "KWD" },
                    { 41, "L00" },
                    { 42, "LBP" },
                    { 43, "LTL" },
                    { 44, "LUF" },
                    { 45, "LVL" },
                    { 46, "MAD" },
                    { 47, "MTL" },
                    { 48, "MXN" },
                    { 49, "MYR" },
                    { 50, "NLG" },
                    { 51, "NOK" },
                    { 52, "NZD" },
                    { 53, "PEN" },
                    { 54, "PHP" },
                    { 55, "PKR" },
                    { 56, "PLN" },
                    { 57, "PTE" },
                    { 58, "QAR" },
                    { 59, "RON" },
                    { 60, "RUB" },
                    { 61, "RUR" },
                    { 62, "SAR" },
                    { 63, "SEK" },
                    { 64, "SGD" },
                    { 65, "SIT" },
                    { 66, "SKK" },
                    { 67, "THB" },
                    { 68, "TND" },
                    { 69, "TRY" },
                    { 70, "US2" },
                    { 71, "USD" },
                    { 72, "VEB" },
                    { 73, "VEF" },
                    { 74, "XAF" },
                    { 75, "XEU" },
                    { 76, "ZAR" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasNatintervs",
                columns: new[] { "Id", "Descripcion", "Tipo" },
                values: new object[,]
                {
                    { 1, null, "T11" },
                    { 2, null, "T12" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasNatintervs",
                columns: new[] { "Id", "Descripcion", "Tipo" },
                values: new object[,]
                {
                    { 3, null, "T13" },
                    { 4, null, "T14" },
                    { 5, null, "T15" },
                    { 6, null, "T16" },
                    { 7, null, "T17" },
                    { 8, null, "T19" },
                    { 9, null, "T20" },
                    { 10, null, "T21" },
                    { 11, null, "T22" },
                    { 12, null, "T23" },
                    { 13, null, "T24" },
                    { 14, null, "T25" },
                    { 15, null, "T26" },
                    { 16, null, "T27" },
                    { 17, null, "T28" },
                    { 18, null, "T29" },
                    { 19, null, "T30" },
                    { 20, null, "T31" },
                    { 21, null, "T32" },
                    { 22, null, "T69" },
                    { 23, null, "T71" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasPersonales",
                columns: new[] { "Id", "Descripcion", "Tipo" },
                values: new object[,]
                {
                    { 1, null, "G00" },
                    { 2, null, "G01" },
                    { 3, null, "G02" },
                    { 4, null, "G03" },
                    { 5, null, "G04" },
                    { 6, null, "G05" },
                    { 7, null, "G06" },
                    { 8, null, "G07" },
                    { 9, null, "G08" },
                    { 10, null, "G09" },
                    { 11, null, "G10" },
                    { 12, null, "G11" },
                    { 13, null, "G12" },
                    { 14, null, "G13" },
                    { 15, null, "G14" },
                    { 16, null, "G15" },
                    { 17, null, "G16" },
                    { 18, null, "G17" },
                    { 19, null, "G18" },
                    { 20, null, "G19" },
                    { 21, null, "G20" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasPersonales",
                columns: new[] { "Id", "Descripcion", "Tipo" },
                values: new object[,]
                {
                    { 22, null, "G21" },
                    { 23, null, "G22" },
                    { 24, null, "G33" },
                    { 25, null, "E14" },
                    { 26, null, "E15" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasPlazos",
                columns: new[] { "Id", "Descripcion", "Tipo" },
                values: new object[,]
                {
                    { 1, null, "L01" },
                    { 2, null, "L02" },
                    { 3, null, "L03" },
                    { 4, null, "L04" },
                    { 5, null, "L05" },
                    { 6, null, "L06" },
                    { 7, null, "L07" },
                    { 8, null, "L08" },
                    { 9, null, "L09" },
                    { 10, null, "L10" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasProductos",
                columns: new[] { "Id", "Descripcion", "Tipo" },
                values: new object[,]
                {
                    { 1, null, "aval" },
                    { 2, null, "click" },
                    { 3, null, "compras" },
                    { 4, null, "documentario" },
                    { 5, null, "hipoteca" },
                    { 6, null, "leasing" },
                    { 7, null, "multilinea" },
                    { 8, null, "poliza" },
                    { 9, null, "prestamo" },
                    { 10, null, "sin" },
                    { 11, null, "tarjeta" },
                    { 12, null, "ventas" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasReales",
                columns: new[] { "Id", "Descripcion", "Tipo" },
                values: new object[,]
                {
                    { 1, null, "G00" },
                    { 2, null, "G01" },
                    { 3, null, "G02" },
                    { 4, null, "G03" },
                    { 5, null, "G04" },
                    { 6, null, "G05" },
                    { 7, null, "G06" },
                    { 8, null, "G07" },
                    { 9, null, "G08" },
                    { 10, null, "G09" },
                    { 11, null, "G10" },
                    { 12, null, "G11" },
                    { 13, null, "G12" },
                    { 14, null, "G13" },
                    { 15, null, "G14" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasReales",
                columns: new[] { "Id", "Descripcion", "Tipo" },
                values: new object[,]
                {
                    { 16, null, "G15" },
                    { 17, null, "G16" },
                    { 18, null, "G17" },
                    { 19, null, "G18" },
                    { 20, null, "G19" },
                    { 21, null, "G20" },
                    { 22, null, "G21" },
                    { 23, null, "G22" },
                    { 24, null, "G33" },
                    { 25, null, "E14" },
                    { 26, null, "E15" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasSituopers",
                columns: new[] { "Id", "Descripcion", "Tipo" },
                values: new object[,]
                {
                    { 1, null, "I15" },
                    { 2, null, "I16" },
                    { 3, null, "I17" },
                    { 4, null, "I18" },
                    { 5, null, "I19" },
                    { 6, null, "I20" },
                    { 7, null, "I21" },
                    { 8, null, "I22" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasSolcols",
                columns: new[] { "Id", "Tipo" },
                values: new object[,]
                {
                    { 1, "T33" },
                    { 2, "T34" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasTipos",
                columns: new[] { "Id", "Descripcion", "Tipo" },
                values: new object[,]
                {
                    { 1, null, "V19" },
                    { 2, null, "V24" },
                    { 3, null, "V25" },
                    { 4, null, "V26" },
                    { 5, null, "V27" },
                    { 6, null, "V28" },
                    { 7, null, "V29" },
                    { 8, null, "V30" },
                    { 9, null, "V31" },
                    { 10, null, "V32" },
                    { 11, null, "V33" },
                    { 12, null, "V34" },
                    { 13, null, "V35" },
                    { 14, null, "V36" },
                    { 15, null, "V37" },
                    { 16, null, "V38" },
                    { 17, null, "V39" },
                    { 18, null, "V40" },
                    { 19, null, "V41" },
                    { 20, null, "V42" },
                    { 21, null, "V43" }
                });

            migrationBuilder.InsertData(
                table: "EquivalenciasTipos",
                columns: new[] { "Id", "Descripcion", "Tipo" },
                values: new object[,]
                {
                    { 22, null, "V44" },
                    { 23, null, "V45" },
                    { 24, null, "V46" },
                    { 25, null, "V47" },
                    { 26, null, "V48" },
                    { 27, null, "V49" },
                    { 28, null, "V51" },
                    { 29, null, "V52" },
                    { 30, null, "V53" },
                    { 31, null, "V54" },
                    { 32, null, "V55" },
                    { 33, null, "V56" },
                    { 34, null, "V57" },
                    { 35, null, "V58" },
                    { 36, null, "V59" },
                    { 37, null, "V60" },
                    { 38, null, "V61" },
                    { 39, null, "V62" },
                    { 40, null, "V63" },
                    { 41, null, "V64" },
                    { 42, null, "V65" },
                    { 43, null, "V66" },
                    { 44, null, "V67" },
                    { 45, null, "V68" },
                    { 46, null, "V69" },
                    { 47, null, "V70" },
                    { 48, null, "V71" },
                    { 49, null, "V72" },
                    { 50, null, "VA0" },
                    { 51, null, "VB1" },
                    { 52, null, "VB2" },
                    { 53, null, "VB3" },
                    { 54, null, "VB4" },
                    { 55, null, "VBD" }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 397);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 398);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 399);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 409);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 413);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 419);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 421);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 424);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 425);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 426);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 429);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 430);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 431);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 432);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 434);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 435);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 437);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 438);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 439);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 440);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 441);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 442);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 443);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 444);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 445);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 447);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 448);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 449);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 450);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 451);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 452);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 453);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 454);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 455);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 456);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 459);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 460);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 461);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 462);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 463);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 464);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 465);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 466);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 467);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 468);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 469);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 470);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 471);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 472);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 473);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 474);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 475);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 476);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 477);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 478);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 479);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 480);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 481);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 482);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 483);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 484);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 485);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 486);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 487);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 488);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 489);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 490);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 491);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 492);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 493);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 494);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 495);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 496);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 497);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 498);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 499);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 513);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 515);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 516);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 517);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 518);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 519);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 520);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 521);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 522);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 523);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 524);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 525);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 526);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 527);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 528);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 529);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 531);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 532);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 533);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 534);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 535);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 536);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 537);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 538);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 539);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 540);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 541);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 542);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 543);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 544);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 545);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 546);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 547);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 548);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 549);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 550);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 551);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 552);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 553);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 554);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 555);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 556);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 557);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 558);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 559);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 560);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 561);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 562);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 563);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 564);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 565);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 566);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 567);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 568);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 569);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 570);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 571);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 572);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 573);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 574);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 575);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 576);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 577);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 578);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 579);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 580);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 581);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 582);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 583);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 584);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 585);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 586);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 587);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 588);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 589);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 590);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 591);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 592);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 593);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 594);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 595);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 596);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 597);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 598);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 599);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 600);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 601);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 602);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 603);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 604);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 605);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 606);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 607);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 608);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 609);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 610);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 611);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 612);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 613);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 614);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 615);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 616);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 617);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 618);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 619);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 620);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 621);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 622);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 623);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 624);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 625);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 626);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 627);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 628);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 629);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 630);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 631);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 632);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 633);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 634);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 635);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 636);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 637);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 638);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 639);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 640);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 641);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 642);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 643);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 644);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 645);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 646);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 647);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 648);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 649);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 650);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 651);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 652);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 653);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 654);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 655);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 656);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 657);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 658);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 659);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 660);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 661);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 662);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 663);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 664);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 665);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 666);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 667);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 668);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 669);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 670);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 671);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 672);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 673);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 674);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 675);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 676);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 677);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 678);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 679);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 680);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 681);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 682);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 683);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 684);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 685);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 686);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 687);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 688);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 689);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 690);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 691);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 692);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 693);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 694);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 695);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 696);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 697);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 698);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 699);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 700);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 701);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 702);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 703);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 704);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 705);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 706);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 707);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 708);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 709);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 710);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 711);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 712);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 713);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 714);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 715);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 716);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 717);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 718);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 719);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 720);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 721);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 722);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 723);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 724);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 725);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 726);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 727);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 728);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 729);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 730);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 731);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 732);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 733);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 734);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 735);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 736);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 737);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 738);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 739);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 740);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 741);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 742);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 743);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 744);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 745);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 746);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 747);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 748);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 749);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 750);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 751);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 752);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 753);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 754);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 755);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 756);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 757);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 758);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 759);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 760);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 761);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 762);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 763);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 764);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 765);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 766);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 767);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 768);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 769);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 770);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 771);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 772);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 773);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 774);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 775);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 776);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 777);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 778);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 779);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 780);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 781);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 782);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 783);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 784);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 785);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 786);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 787);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 788);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 789);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 790);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 791);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 792);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 793);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 794);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 795);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 796);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 797);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 798);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 799);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 800);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 801);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 802);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 803);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 804);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 805);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 806);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 807);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 808);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 809);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 810);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 811);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 812);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 813);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 814);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 815);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 816);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 817);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 818);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 819);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 820);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 821);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 822);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 823);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 824);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 825);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 826);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 827);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 828);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 829);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 830);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 831);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 832);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 833);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 834);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 835);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 836);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 837);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 838);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 839);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 840);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 841);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 842);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 843);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 844);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 845);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 846);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 847);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 848);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 849);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 850);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 851);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 852);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 853);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 854);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 855);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 856);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 857);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 858);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 859);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 860);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 861);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 862);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 863);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 864);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 865);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 866);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 867);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 868);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 869);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 870);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 871);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 872);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 873);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 874);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 875);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 876);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 877);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 878);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 879);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 880);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 881);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 882);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 883);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 884);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 885);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 886);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 887);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 888);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 889);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 890);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 891);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 892);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 893);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 894);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 895);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 896);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 897);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 898);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 899);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 900);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 901);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 902);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 903);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 904);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 905);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 906);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 907);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 908);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 909);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 910);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 911);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 912);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 913);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 914);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 915);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 916);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 917);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 918);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 919);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 920);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 921);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 922);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 923);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 924);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 925);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 926);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 927);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 928);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 929);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 930);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 931);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 932);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 933);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 934);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 935);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 936);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 937);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 938);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 939);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 940);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 941);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 942);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 943);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 944);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 945);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 946);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 947);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 948);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 949);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 950);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 951);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 952);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 953);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 954);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 955);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 956);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 957);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 958);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 959);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 960);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 961);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 962);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 963);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 964);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 965);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 966);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 967);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 968);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 969);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 970);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 971);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 972);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 973);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 974);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 975);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 976);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 977);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 978);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 979);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 980);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 981);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 982);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 983);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 984);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 985);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 986);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 987);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 988);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 989);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 990);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 991);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 992);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 993);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 994);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 995);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 996);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 997);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 998);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 999);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1000);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1026);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1027);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1028);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1029);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1030);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1034);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1035);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1036);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1037);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1038);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1039);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1040);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1041);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1042);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1043);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1044);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1045);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1046);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1047);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1048);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1049);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1050);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1051);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1052);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1053);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1054);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1055);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1056);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1057);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1058);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1059);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1060);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1061);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1062);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1063);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1064);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1065);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1066);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1067);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1068);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1069);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1070);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1071);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1072);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1073);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1074);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1075);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1076);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1077);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1078);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1079);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1080);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1081);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1082);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1083);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1084);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1085);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1086);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1087);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1088);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1089);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1090);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1091);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1092);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1093);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1094);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1095);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1096);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1097);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1098);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1099);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1100);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1101);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1102);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1103);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1104);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1105);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1106);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1107);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1108);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1109);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1110);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1111);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1112);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1113);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1114);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1115);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1116);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1117);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1118);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1119);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1120);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1121);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1122);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1123);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1124);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1125);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1126);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1127);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1128);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1129);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1130);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1131);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1132);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1133);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1134);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1135);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1136);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1137);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1138);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1139);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1140);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1141);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1142);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1143);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1144);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1145);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1146);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1147);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1148);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1149);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1150);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1151);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1152);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1153);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1154);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1155);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1156);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1157);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1158);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1159);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1160);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1161);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1162);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1163);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1164);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1165);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1166);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1167);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1168);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1169);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1170);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1171);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1172);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1173);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1174);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1175);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1176);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1177);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1178);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1179);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1180);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1181);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1182);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1183);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1184);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1185);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1186);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1187);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1188);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1189);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1190);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1191);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1192);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1193);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1194);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1195);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1196);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1197);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1198);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1199);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1200);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1201);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1202);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1203);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1204);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1205);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1206);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1207);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1208);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1209);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1210);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1211);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1212);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1213);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1214);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1215);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1216);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1217);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1218);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1219);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1220);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1221);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1222);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1223);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1224);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1225);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1226);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1227);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1228);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1229);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1230);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1231);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1232);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1233);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1234);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1235);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1236);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1237);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1238);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1239);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1240);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1241);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1242);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1243);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1244);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1245);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1246);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1247);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1248);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1249);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1250);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1251);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1252);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1253);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1254);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1255);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1256);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1257);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1258);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1259);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1260);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1261);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1262);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1263);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1264);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1265);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1266);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1267);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1268);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1269);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1270);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1271);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1272);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1273);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1274);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1275);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1276);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1277);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1278);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1279);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1280);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1281);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1282);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1283);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1284);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1285);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1286);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1287);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1288);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1289);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1290);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1291);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1292);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1293);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1294);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1295);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1296);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1297);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1298);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1299);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1300);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1301);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1302);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1303);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1304);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1305);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1306);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1307);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1308);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1309);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1310);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1311);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1312);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1313);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1314);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1315);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1316);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1317);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1318);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1319);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1320);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1321);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1322);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1323);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1324);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1325);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1326);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1327);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1328);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1329);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1330);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1331);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1332);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1333);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1334);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1335);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1336);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1337);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1338);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1339);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1340);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1341);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1342);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1343);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1344);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1345);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1346);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1347);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1348);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1349);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1350);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1351);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1352);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1353);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1354);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1355);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1356);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1357);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1358);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1359);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1360);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1361);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1362);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1363);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1364);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1365);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1366);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1367);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1368);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1369);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1370);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1371);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1372);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1373);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1374);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1375);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1376);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1377);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1378);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1379);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1380);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1381);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1382);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1383);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1384);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1385);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1386);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1387);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1388);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1389);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1390);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1391);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1392);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1393);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1394);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1395);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1396);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1397);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1398);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1399);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1400);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1401);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1402);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1403);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1404);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1405);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1406);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1407);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1408);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1409);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1410);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1411);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1412);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1413);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1414);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1415);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1416);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1417);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1418);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1419);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1420);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1421);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1422);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1423);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1424);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1425);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1426);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1427);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1428);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1429);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1430);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1431);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1432);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1433);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1434);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1435);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1436);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1437);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1438);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1439);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1440);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1441);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1442);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1443);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1444);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1445);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1446);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1447);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1448);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1449);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1450);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1451);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1452);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1453);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1454);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1455);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1456);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1457);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1458);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1459);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1460);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1461);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1462);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1463);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1464);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1465);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1466);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1467);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1468);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1469);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1470);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1471);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1472);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1473);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1474);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1475);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1476);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1477);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1478);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1479);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1480);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1481);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1482);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1483);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1484);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1485);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1486);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1487);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1488);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1489);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1490);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1491);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1492);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1493);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1494);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1495);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1496);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1497);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1498);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1499);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1500);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1501);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1502);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1503);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1504);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1505);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1506);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1507);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1508);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1509);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1510);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1511);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1512);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1513);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1514);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1515);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1516);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1517);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1518);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1519);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1520);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1521);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1522);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1523);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1524);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1525);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1526);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1527);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1528);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1529);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1530);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1531);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1532);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1533);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1534);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1535);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1536);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1537);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1538);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1539);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1540);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1541);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1542);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1543);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1544);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1545);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1546);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1547);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1548);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1549);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1550);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1551);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1552);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1553);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1554);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1555);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1556);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1557);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1558);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1559);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1560);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1561);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1562);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1563);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1564);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1565);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1566);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1567);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1568);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1569);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1570);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1571);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1572);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1573);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1574);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1575);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1576);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1577);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1578);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1579);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1580);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1581);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1582);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1583);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1584);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1585);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1586);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1587);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1588);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1589);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1590);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1591);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1592);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1593);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1594);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1595);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1596);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1597);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1598);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1599);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1600);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1601);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1602);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1603);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1604);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1605);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1606);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1607);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1608);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1609);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1610);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1611);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1612);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1613);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1614);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1615);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1616);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1617);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1618);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1619);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1620);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1621);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1622);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1623);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1624);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1625);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1626);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1627);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1628);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1629);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1630);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1631);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1632);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1633);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1634);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1635);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1636);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1637);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1638);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1639);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1640);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1641);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1642);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1643);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1644);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1645);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1646);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1647);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1648);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1649);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1650);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1651);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1652);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1653);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1654);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1655);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1656);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1657);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1658);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1659);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1660);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1661);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1662);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1663);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1664);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1665);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1666);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1667);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1668);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1669);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1670);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1671);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1672);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1673);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1674);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1675);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1676);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1677);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1678);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1679);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1680);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1681);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1682);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1683);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1684);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1685);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1686);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1687);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1688);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1689);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1690);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1691);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1692);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1693);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1694);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1695);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1696);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1697);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1698);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1699);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1700);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1701);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1702);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1703);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1704);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1705);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1706);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1707);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1708);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1709);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1710);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1711);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1712);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1713);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1714);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1715);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1716);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1717);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1718);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1719);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1720);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1721);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1722);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1723);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1724);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1725);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1726);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1727);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1728);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1729);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1730);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1731);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1732);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1733);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1734);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1735);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1736);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1737);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1738);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1739);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1740);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1741);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1742);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1743);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1744);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1745);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1746);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1747);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1748);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1749);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1750);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1751);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1752);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1753);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1754);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1755);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1756);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1757);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1758);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1759);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1760);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1761);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1762);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1763);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1764);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1765);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1766);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1767);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1768);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1769);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1770);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1771);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1772);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1773);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1774);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1775);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1776);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1777);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1778);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1779);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1780);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1781);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1782);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1783);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1784);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1785);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1786);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1787);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1788);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1789);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1790);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1791);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1792);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1793);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1794);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1795);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1796);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1797);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1798);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1799);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1800);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1801);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1802);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1803);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1804);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1805);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1806);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1807);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1808);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1809);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1810);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1811);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1812);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1813);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1814);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1815);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1816);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1817);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1818);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1819);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1820);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1821);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1822);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1823);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1824);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1825);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1826);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1827);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1828);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1829);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1830);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1831);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1832);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1833);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1834);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1835);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1836);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1837);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1838);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1839);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1840);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1841);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1842);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1843);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1844);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1845);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1846);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1847);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1848);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1849);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1850);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1851);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1852);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1853);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1854);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1855);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1856);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1857);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1858);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1859);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1860);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1861);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1862);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1863);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1864);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1865);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1866);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1867);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1868);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1869);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1870);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1871);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1872);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1873);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1874);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1875);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1876);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1877);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1878);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1879);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1880);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1881);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1882);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1883);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1884);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1885);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1886);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1887);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1888);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1889);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1890);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1891);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1892);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1893);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1894);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1895);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1896);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1897);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1898);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1899);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1900);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1901);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1902);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1903);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1904);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1905);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1906);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1907);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1908);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1909);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1910);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1911);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1912);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1913);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1914);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1915);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1916);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1917);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1918);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1919);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1920);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1921);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1922);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1923);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1924);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1925);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1926);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1927);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1928);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1929);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1930);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1931);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1932);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1933);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1934);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1935);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1936);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1937);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1938);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1939);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1940);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1941);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1942);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1943);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1944);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1945);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1946);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1947);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1948);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1949);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1950);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1951);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1952);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1953);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1954);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1955);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1956);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1957);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1958);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1959);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1960);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1961);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1962);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1963);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1964);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1965);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1966);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1967);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1968);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1969);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1970);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1971);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1972);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1973);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1974);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1975);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1976);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1977);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1978);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1979);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1980);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1981);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1982);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1983);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1984);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1985);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1986);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1987);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1988);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1989);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1990);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1991);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1992);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1993);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1994);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1995);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1996);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1997);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1998);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 1999);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2000);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2001);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2002);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2003);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2004);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2005);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2006);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2007);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2008);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2009);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2010);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2011);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2012);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2013);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2014);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2015);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2016);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2017);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2018);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2019);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2020);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2021);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2022);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2023);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2025);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2026);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2027);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2028);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2029);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2030);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2031);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2032);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2033);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2034);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2035);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2036);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2037);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2038);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2039);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2040);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2041);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2042);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2043);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2044);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2045);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2046);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2047);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2048);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2049);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2050);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2051);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2052);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2053);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2054);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2055);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2056);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2057);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2058);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2059);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2060);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2061);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2062);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2063);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2064);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2065);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2066);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2067);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2068);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2069);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2070);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2071);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2072);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2073);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2074);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2075);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2076);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2077);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2078);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2079);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2080);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2081);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2082);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2083);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2084);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2085);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2086);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2087);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2088);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2089);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2090);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2091);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2092);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2093);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2094);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2095);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2096);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2097);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2098);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2099);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2100);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2101);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2102);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2103);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2104);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2105);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2106);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2107);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2108);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2109);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2110);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2111);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2112);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2113);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2114);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2115);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2116);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2117);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2118);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2119);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2120);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2121);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2122);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2123);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2124);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2125);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2126);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2127);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2128);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2129);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2130);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2131);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2132);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2133);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2134);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2135);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2136);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2137);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2138);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2139);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2140);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2141);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2142);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2143);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2144);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2145);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2146);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2147);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2148);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2149);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2150);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2151);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2152);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2153);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2154);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2155);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2156);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2157);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2158);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2159);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2160);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2161);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2162);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2163);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2164);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2165);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2166);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2167);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2168);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2169);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2170);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2171);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2172);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2173);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2174);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2175);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2176);

            migrationBuilder.DeleteData(
                table: "EquivalenciasEntidades",
                keyColumn: "Id",
                keyValue: 2177);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "EquivalenciasMonedas",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "EquivalenciasNatintervs",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPersonales",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPlazos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPlazos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPlazos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPlazos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPlazos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPlazos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPlazos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPlazos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPlazos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EquivalenciasPlazos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EquivalenciasProductos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "EquivalenciasReales",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "EquivalenciasSituopers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquivalenciasSituopers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquivalenciasSituopers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquivalenciasSituopers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquivalenciasSituopers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquivalenciasSituopers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EquivalenciasSituopers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EquivalenciasSituopers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EquivalenciasSolcols",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquivalenciasSolcols",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "EquivalenciasTipos",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasTipos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasSituopers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasReales",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasProductos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasPlazos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasPersonales",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EquivalenciasNatintervs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

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
    }
}
