using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tecnocim.Alia.DataInfrastructure.Repositories;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception($"A {nameof(connectionString)} is required");
        }

        services.AddDbContext<SmartdebtContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
            options.EnableSensitiveDataLogging();
        }, ServiceLifetime.Transient);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRolRepository, RolRepository>();
        services.AddScoped<IEmpresaConfiguracionesRepository, EmpresaConfiguracionesRepository>();
        services.AddScoped<IEquivalenciasEntidadRepository, EquivalenciasEntidadRepository>();
        services.AddScoped<IEquivalenciasMonedaRepository, EquivalenciasMonedaRepository>();
        services.AddScoped<IEquivalenciasNatintervRepository, EquivalenciasNatintervRepository>();
        services.AddScoped<IEquivalenciasPersonalRepository, EquivalenciasPersonalRepository>();
        services.AddScoped<IEquivalenciasPlazoRepository, EquivalenciasPlazoRepository>();
        services.AddScoped<IEquivalenciasProductoRepository, EquivalenciasProductoRepository>();
        services.AddScoped<IEquivalenciasRealRepository, EquivalenciasRealRepository>();
        services.AddScoped<IEquivalenciasSituoperRepository, EquivalenciasSituoperRepository>();
        services.AddScoped<IEquivalenciasSolcolRepository, EquivalenciasSolcolRepository>();
        services.AddScoped<IEquivalenciasTipoRepository, EquivalenciasTipoRepository>();
        services.AddScoped<IEquivalenciasPeriodificacionRepository, EquivalenciasPeriodificacionRepository>();
        services.AddScoped<IContratoRepository, ContratoRepository>();
        services.AddScoped<IPoolRepository, PoolRepository>();
        services.AddScoped<IDocumentoRepository, DocumentoRepository>();
        services.AddScoped<ICirbeRepository, CirbeRepository>();
        services.AddScoped<IFicheroRepository, FicheroRepository>();
        services.AddScoped<IRatioRepository, RatioRepository>();
        services.AddScoped<ICuotaRepository, CuotaRepository>();
        services.AddScoped<IInterpretacionRepository, InterpretacionRepository>();
        services.AddScoped<IAnaliticaRepository, AnaliticaRepository>();
        services.AddScoped<IContabilidadRepository, ContabilidadRepository>();
        services.AddScoped<IContabilidadConfiguracionRepository, ContabilidadConfiguracionRepository>();

        return services;
    }
}
