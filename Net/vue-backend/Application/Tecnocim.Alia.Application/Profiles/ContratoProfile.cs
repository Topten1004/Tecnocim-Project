using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Extensions;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.DataInfrastructure;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class ContratoProfile : Profile
{
    public ContratoProfile(SmartdebtContext context)
    {
        Context = context;

        CreateMap<Contrato, PoolBancarioDto>()
            .ForMember(dto => dto.Entidad, e => e.MapFrom(x => x.EquivalenciasEntidad.Nombre))
            .ForMember(dto => dto.Producto, e => e.MapFrom(x => x.EquivalenciasProducto.Tipo))
            .ForMember(dto => dto.Divisa, e => e.MapFrom(x => x.EquivalenciasMoneda.Tipo))
            .ForMember(dto => dto.ImporteInicial, e => e.MapFrom(x => decimal.Round(x.Limite, 2, MidpointRounding.AwayFromZero)))
            .ForMember(dto => dto.Dispuesto, e => e.MapFrom(x => decimal.Round(x.Pools.Sum(t => t.Dispuesto) ?? 0, 2, MidpointRounding.AwayFromZero)))
            .ForMember(dto => dto.Disponible, e => e.MapFrom(x => decimal.Round(x.Limite - x.Pools.Sum(t => t.Dispuesto) ?? 0, 2, MidpointRounding.AwayFromZero)))
            .ForMember(dto => dto.Inicio, e => e.MapFrom(x => x.Inicio.ToString("yyyy-MM-dd")))
            .ForMember(dto => dto.Fin, e => e.MapFrom(x => x.Vencimiento.ToString("yyyy-MM-dd")))
            .ForMember(dto => dto.FormaPago, e => e.MapFrom(x => x.EquivalenciasPeriodificacionId))
            .ForMember(dto => dto.Cuota, e => e.Ignore())
            .ForMember(dto => dto.Numero, e => e.MapFrom(x => x.PlazosAmortizacion))
            .ForMember(dto => dto.ServicioDeuda, e => e.Ignore())
            .ForMember(dto => dto.Cirbe, e => e.MapFrom(x => x.Cirbes.Any()))
            .ForMember(dto => dto.Valoracion, e => e.MapFrom(x => decimal.Round(x.Valoracion ?? 0, 2, MidpointRounding.AwayFromZero)))
            .ForMember(dto => dto.Notas, e => e.MapFrom(x => x.Notas))
            .ForMember(dto => dto.Precio, e => e.MapFrom(x => decimal.Round(x.Precio, 2, MidpointRounding.AwayFromZero)))
            .ForMember(dto => dto.PolizaDigitalizada, e => e.MapFrom(x => x.Digitalizada))
            .ForMember(dto => dto.Minimis, e => e.MapFrom(x => x.Minimis))
            .AfterMap((c, dto) =>
            {
                var fecha = Context.Pools.FirstOrDefault(x => x.ContratoId == c.ContratoId)?.Documento?.Fecha;
                if (fecha is not null)
                {
                    var cuota = c.Cuotas?.FirstOrDefault(x => x.Fecha.AddMonths(1) == fecha)?.Importe ?? 0;
                    var servicioDeuda = c.Cuotas?.Where(x => x.Fecha > fecha)?.Sum(x => x.Importe) ?? 0;
                    dto.Cuota = decimal.Round(cuota, 2, MidpointRounding.AwayFromZero);
                    dto.ServicioDeuda = decimal.Round(servicioDeuda, 2, MidpointRounding.AwayFromZero);
                }
            });

        CreateMap<Contrato, ContratoDto>()
           .ForMember(dto => dto.EquivalenciasEntidad, e => e.MapFrom(x => x.EquivalenciasEntidad))
           .ForMember(dto => dto.EquivalenciasProducto, e => e.MapFrom(x => x.EquivalenciasProducto))
           .ForMember(dto => dto.EquivalenciasMoneda, e => e.MapFrom(x => x.EquivalenciasMoneda))
           .ForMember(dto => dto.EquivalenciasPeriodificacion, e => e.MapFrom(x => x.EquivalenciasPeriodificacion))
           .ForMember(dto => dto.Carencia, e => e.MapFrom(x => x.Carencia))
           .ForMember(dto => dto.ContratoId, e => e.MapFrom(x => x.ContratoId))
           .ForMember(dto => dto.Cuenta2, e => e.Ignore())
           .ForMember(dto => dto.Created, e => e.MapFrom(x => x.Created.ToString("yyyy-MM-dd")))
           .ForMember(dto => dto.Digitalizada, e => e.MapFrom(x => x.Digitalizada))
           .ForMember(dto => dto.Inicio, e => e.MapFrom(x => x.Inicio.ToString("yyyy-MM-dd")))
           .ForMember(dto => dto.Vencimiento, e => e.MapFrom(x => x.Vencimiento.ToString("yyyy-MM-dd")))
           .ForMember(dto => dto.Limite, e => e.MapFrom(x => x.Limite.ToTwoDecimalAndSymbolFormat('c')))
           .ForMember(dto => dto.Minimis, e => e.MapFrom(x => x.Minimis))
           .ForMember(dto => dto.Notas, e => e.MapFrom(x => x.Notas))
           .ForMember(dto => dto.PlazosAmortizacion, e => e.MapFrom(x => x.PlazosAmortizacion))
           .ForMember(dto => dto.Precio, e => e.MapFrom(x => x.Precio.ToTwoDecimalAndSymbolFormat('c')))
           .ForMember(dto => dto.Valoracion, e => e.MapFrom(x => x.Valoracion))
           .AfterMap((cto, dto) =>
           {
               var cuenta2 = cto.Pools.FirstOrDefault(x => x.Cuenta.StartsWith("52"))?.PoolId;
               dto.Cuenta2 = cuenta2.HasValue ? cuenta2.Value.ToString() : string.Empty;
           });

        CreateMap<CreateContratoRequest, Contrato>()
            .ForMember(entity => entity.Inicio, req => req.MapFrom(x => DateOnly.FromDateTime(x.FechaInicio)))
            .ForMember(entity => entity.Vencimiento, req => req.MapFrom(x => DateOnly.FromDateTime(x.FechaFin)))
            .ForMember(entity => entity.Carencia, req => req.MapFrom(x => x.Carencia))
            .ForMember(entity => entity.Precio, req => req.MapFrom(x => x.Precio))
            .ForMember(entity => entity.Limite, req => req.MapFrom(x => x.ImporteInicial))
            .ForMember(entity => entity.Valoracion, req => req.MapFrom(x => x.Valoracion))
            .ForMember(entity => entity.Notas, req => req.MapFrom(x => x.NotasConsultor))
            .ForMember(entity => entity.Digitalizada, req => req.MapFrom(x => x.PolizaDigitalizada))
            .ForMember(entity => entity.Minimis, req => req.MapFrom(x => x.Minimis))
            .ForMember(entity => entity.EquivalenciasEntidadId, req => req.MapFrom(x => x.Entidad))
            .ForMember(entity => entity.EquivalenciasProductoId, req => req.MapFrom(x => x.TipoProducto))
            .ForMember(entity => entity.EquivalenciasMonedaId, req => req.MapFrom(x => x.Moneda))
            .ForMember(entity => entity.EquivalenciasPeriodificacionId, req => req.MapFrom(x => x.FormaDePago))
            .ForMember(entity => entity.Cirbes, req => req.Ignore())
            .ForMember(entity => entity.Pools, req => req.Ignore())
            .ForMember(e => e.Created, e => e.MapFrom(x => DateTime.UtcNow))
            .ForMember(e => e.Updated, e => e.MapFrom(x => DateTime.UtcNow));

        CreateMap<UpdateContratoRequest, Contrato>()
           .ForMember(entity => entity.Inicio, req => req.MapFrom(x => DateOnly.FromDateTime(x.FechaInicio)))
           .ForMember(entity => entity.Vencimiento, req => req.MapFrom(x => DateOnly.FromDateTime(x.FechaFin)))
           .ForMember(entity => entity.Carencia, req => req.MapFrom(x => x.Carencia))
           .ForMember(entity => entity.Precio, req => req.MapFrom(x => x.Precio))
           .ForMember(entity => entity.Limite, req => req.MapFrom(x => x.ImporteInicial))
           .ForMember(entity => entity.Valoracion, req => req.MapFrom(x => x.Valoracion))
           .ForMember(entity => entity.Notas, req => req.MapFrom(x => x.NotasConsultor))
           .ForMember(entity => entity.Digitalizada, req => req.MapFrom(x => x.PolizaDigitalizada))
           .ForMember(entity => entity.Minimis, req => req.MapFrom(x => x.Minimis))
           .ForMember(entity => entity.EquivalenciasEntidadId, req => req.MapFrom(x => x.Entidad))
           .ForMember(entity => entity.EquivalenciasProductoId, req => req.MapFrom(x => x.TipoProducto))
           .ForMember(entity => entity.EquivalenciasMonedaId, req => req.MapFrom(x => x.Moneda))
           .ForMember(entity => entity.EquivalenciasPeriodificacionId, req => req.MapFrom(x => x.FormaDePago))
           .ForMember(entity => entity.Cirbes, req => req.Ignore())
           .ForMember(entity => entity.Pools, req => req.Ignore())
           .ForMember(e => e.Created, e => e.MapFrom(x => DateTime.UtcNow))
           .ForMember(e => e.Updated, e => e.MapFrom(x => DateTime.UtcNow));
    }

    public SmartdebtContext Context { get; }
}
