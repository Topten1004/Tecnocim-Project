using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EquivalenciaRealProfile : Profile
{
    public EquivalenciaRealProfile()
    {
        CreateMap<EquivalenciasReal, EquivalenciaDto>();
    }
}
