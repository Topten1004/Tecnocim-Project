using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class InterpretacionProfile : Profile
{
    public InterpretacionProfile()
    {
        CreateMap<Interpretacion, InterpretacionDto>()
            .ForMember(dto => dto.TendenciaColor, x => x.Ignore())
            .ForMember(dto => dto.TendenciaIcono, x => x.Ignore())
            .ForMember(dto => dto.TendenciaAnteriorColor, x => x.Ignore())
            .ForMember(dto => dto.TendenciaAnteriorColor, x => x.Ignore());
    }
}