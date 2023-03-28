using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EquivalenciaSolcolProfile : Profile
{
    public EquivalenciaSolcolProfile()
    {
        CreateMap<EquivalenciasSolcol, EquivalenciaSolcolDto>();
    }
}
