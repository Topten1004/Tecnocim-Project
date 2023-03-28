using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class DocumentoProfile : Profile
{
    public DocumentoProfile()
    {
        CreateMap<Documento, DocumentoDto>()
            .ForMember(dto => dto.Fecha, x => x.MapFrom(d => d.Fecha.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dto => dto.Created, x => x.MapFrom(d => d.Created.ToString("yyyy-MM-dd HH:mm:ss")));

        CreateMap<Documento, DocumentoErroresDto>()
            .ForMember(dto => dto.Fecha, x => x.MapFrom(d => d.Fecha.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dto => dto.Created, x => x.MapFrom(d => d.Created.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dto => dto.Errores, x => x.Ignore());
    }
}
