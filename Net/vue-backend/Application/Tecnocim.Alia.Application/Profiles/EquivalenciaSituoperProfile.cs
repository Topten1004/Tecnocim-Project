using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EquivalenciaSituoperProfile : Profile
{
    public EquivalenciaSituoperProfile()
    {
        CreateMap<EquivalenciasSituoper, EquivalenciaDto>();
    }
}
