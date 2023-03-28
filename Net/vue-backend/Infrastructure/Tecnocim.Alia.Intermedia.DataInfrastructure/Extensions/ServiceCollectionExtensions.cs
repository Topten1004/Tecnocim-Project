using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tecnocim.Alia.Intermedia.DataInfrastructure;
using Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntermediaDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("IntermediaConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception($"A {nameof(connectionString)} is required");
        }

        services.AddDbContext<SmartdebtIntermediaContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
            options.EnableSensitiveDataLogging();
        }, ServiceLifetime.Transient);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICoreEmpresaRepository, CoreEmpresaRepository>();
        services.AddScoped<ICoreExtraccionesErroreRepository, CoreExtraccionesErroreRepository>();

        return services;
    }
}
