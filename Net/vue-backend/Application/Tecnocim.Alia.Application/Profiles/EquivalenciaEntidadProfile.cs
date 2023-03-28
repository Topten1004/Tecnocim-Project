using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EquivalenciaEntidadProfile : Profile
{
    public EquivalenciaEntidadProfile()
    {
        CreateMap<EquivalenciasEntidad, EquivalenciaEntidadDto>();
    }
}
