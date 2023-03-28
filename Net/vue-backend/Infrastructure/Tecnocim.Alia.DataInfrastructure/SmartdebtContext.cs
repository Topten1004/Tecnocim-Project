using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Tecnocim.Alia.DataInfrastructure.Comparers;
using Tecnocim.Alia.DataInfrastructure.Converters;
using Tecnocim.Alia.DataInfrastructure.Extensions;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.DataInfrastructure;

public class SmartdebtContext : DbContext
{
    public SmartdebtContext()
    {

    }

    public SmartdebtContext(DbContextOptions<SmartdebtContext> options) : base(options)
    {
    }

    public virtual DbSet<Empresa> Empresas { get; set; } = null!;
    public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
    public virtual DbSet<EmpresaConfiguraciones> EmpresaConfiguraciones { get; set; } = null!;
    public virtual DbSet<Rol> Roles { get; set; } = null!;
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public virtual DbSet<EquivalenciasEntidad> EquivalenciasEntidades { get; set; } = null!;
    public virtual DbSet<EquivalenciasMoneda> EquivalenciasMonedas { get; set; } = null!;
    public virtual DbSet<EquivalenciasNatinterv> EquivalenciasNatintervs { get; set; } = null!;
    public virtual DbSet<EquivalenciasPersonal> EquivalenciasPersonales { get; set; } = null!;
    public virtual DbSet<EquivalenciasPlazo> EquivalenciasPlazos { get; set; } = null!;
    public virtual DbSet<EquivalenciasProducto> EquivalenciasProductos { get; set; } = null!;
    public virtual DbSet<EquivalenciasReal> EquivalenciasReales { get; set; } = null!;
    public virtual DbSet<EquivalenciasSituoper> EquivalenciasSituopers { get; set; } = null!;
    public virtual DbSet<EquivalenciasSolcol> EquivalenciasSolcols { get; set; } = null!;
    public virtual DbSet<EquivalenciasTipo> EquivalenciasTipos { get; set; } = null!;
    public virtual DbSet<EquivalenciasPeriodificacion> EquivalenciasPeriodificaciones { get; set; } = null!;
    public virtual DbSet<Documento> Documentos { get; set; } = null!;
    public virtual DbSet<Ratio> Ratios { get; set; } = null!;
    public virtual DbSet<Operacion> Operaciones { get; set; } = null!;
    public virtual DbSet<Analitica> Analiticas { get; set; } = null!;
    public virtual DbSet<Contabilidad> Contabilidades { get; set; } = null!;
    public virtual DbSet<Contrato> Contratos { get; set; } = null!;
    public virtual DbSet<Pool> Pools { get; set; } = null!;
    public virtual DbSet<Cirbe> Cirbes { get; set; } = null!;
    public virtual DbSet<CirbePersonal> CirbePersonales { get; set; } = null!;
    public virtual DbSet<CirbeReal> CirbeReales { get; set; } = null!;
    public virtual DbSet<Fichero> Ficheros { get; set; } = null!;
    public virtual DbSet<Cuota> Cuotas { get; set; } = null!;
    public virtual DbSet<Interpretacion> Interpretaciones { get; set; } = null;
    public virtual DbSet<ContabilidadConfiguracion> ContabilidadConfiguraciones { get; set; } = null;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SetAllProperties<DateTime>(p => p.HasPrecision(3));
        modelBuilder.SetAllProperties<DateTime?>(p => p.HasPrecision(3));
        modelBuilder.SetAllProperties<DateOnly>(p => p.HasConversion<DateOnlyConverter, DateOnlyComparer>());
        modelBuilder.SetAllProperties<decimal>(p => p.HasPrecision(18, 2));
        modelBuilder.SetAllProperties<decimal?>(p => p.HasPrecision(18, 2));

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Seed();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
}
