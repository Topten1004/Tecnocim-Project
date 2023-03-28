using AutoMapper;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class FicheroProfile : Profile
{
    public FicheroProfile()
    {
        CreateMap<Fichero, UploadFicheroResponse>()
            .ForMember(dto => dto.FechaContenido, e => e.MapFrom(x => new DateTime(x.FechaContenido.Year, x.FechaContenido.Month, x.FechaContenido.Day)));
    }
}
