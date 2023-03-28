using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class PoolProfile : Profile
{
    public PoolProfile()
    {
        CreateMap<Pool, PoolDto>()
            .ForMember(dto => dto.Dispuesto, x => x.MapFrom(p => decimal.Round(p.Dispuesto ?? 0, 2, MidpointRounding.AwayFromZero)));
    }
}
