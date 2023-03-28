using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Tecnocim.Alia.Application.Authentication;
using Tecnocim.Alia.Application.Profiles;
using Tecnocim.Alia.Application.Services;
using Tecnocim.Alia.DataInfrastructure;

namespace Tecnocim.Alia.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IExtractorService, ExtractorService>();
        services.AddScoped<IJwtUtils, JwtUtils>();

        services.AddTransient(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ContratoProfile(provider.GetService<SmartdebtContext>()));
            cfg.AddProfile(new EmpresaConfiguracionesProfile());
            cfg.AddProfile(new EmpresaProfile());
            cfg.AddProfile(new EquivalenciaEntidadProfile());
            cfg.AddProfile(new EquivalenciaMonedaProfile());
            cfg.AddProfile(new EquivalenciaNatintervProfile());
            cfg.AddProfile(new EquivalenciaPeriodificacionProfile());
            cfg.AddProfile(new EquivalenciaPersonalProfile());
            cfg.AddProfile(new EquivalenciaPlazoProfile());
            cfg.AddProfile(new EquivalenciaProductoProfile());
            cfg.AddProfile(new EquivalenciaRealProfile());
            cfg.AddProfile(new EquivalenciaSituoperProfile());
            cfg.AddProfile(new EquivalenciaSolcolProfile());
            cfg.AddProfile(new EquivalenciaTipoProfile());
            cfg.AddProfile(new RefreshTokenProfile());
            cfg.AddProfile(new RolProfile());
            cfg.AddProfile(new UsuarioProfile());
            cfg.AddProfile(new PoolProfile());
            cfg.AddProfile(new DocumentoProfile());
            cfg.AddProfile(new FicheroProfile());
            cfg.AddProfile(new InterpretacionProfile());
        }).CreateMapper());

        return services;
    }
}
