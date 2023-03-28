using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EquivalenciaTipoProfile : Profile
{
    public EquivalenciaTipoProfile()
    {
        CreateMap<EquivalenciasTipo, EquivalenciaDto>();
    }
}