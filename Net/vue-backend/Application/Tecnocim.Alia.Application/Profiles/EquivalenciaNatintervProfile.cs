using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EquivalenciaNatintervProfile : Profile
{
    public EquivalenciaNatintervProfile()
    {
        CreateMap<EquivalenciasNatinterv, EquivalenciaDto>();
    }
}
