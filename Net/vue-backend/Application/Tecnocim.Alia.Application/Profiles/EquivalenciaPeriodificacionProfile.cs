using AutoMapper;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Profiles;

public class EquivalenciaPeriodificacionProfile : Profile
{
    public EquivalenciaPeriodificacionProfile()
    {
        CreateMap<EquivalenciasPeriodificacion, EquivalenciaPeriodificacionDto>();
    }
}