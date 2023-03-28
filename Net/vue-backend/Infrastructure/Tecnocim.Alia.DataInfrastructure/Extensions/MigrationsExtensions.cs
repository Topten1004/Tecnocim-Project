using Microsoft.EntityFrameworkCore.Migrations;

namespace Tecnocim.Alia.DataInfrastructure.Extensions
{
    public static class MigrationsExtensions
    {
        public static MigrationBuilder RunSqlScriptFile(this MigrationBuilder builder, string filename)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var scriptsPath = Path.Combine(AppContext.BaseDirectory, "Scripts", filename);
            if (File.Exists(scriptsPath))
            {
                builder.Sql(File.ReadAllText(scriptsPath));
            }
            else
            {
                throw new Exception($"Migration .sql file not found: ${filename}");
            }

            return builder;
        }
    }
}
