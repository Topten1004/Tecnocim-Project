using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EquivalenciaPlazoProfile : Profile
{
    public EquivalenciaPlazoProfile()
    {
        CreateMap<EquivalenciasPlazo, EquivalenciaDto>();
    }
}
