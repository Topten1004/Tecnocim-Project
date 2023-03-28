using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EquivalenciaMonedaProfile : Profile
{
    public EquivalenciaMonedaProfile()
    {
        CreateMap<EquivalenciasMoneda, EquivalenciaMonedaDto>();
    }
}
