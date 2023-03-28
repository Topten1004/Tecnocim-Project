using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EquivalenciaPersonalProfile : Profile
{
    public EquivalenciaPersonalProfile()
    {
        CreateMap<EquivalenciasPersonal, EquivalenciaDto>();
    }
}
