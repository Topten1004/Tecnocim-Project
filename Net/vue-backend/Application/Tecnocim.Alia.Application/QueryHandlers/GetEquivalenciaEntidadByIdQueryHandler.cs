using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetEquivalenciaEntidadByIdQueryHandler : IRequestHandler<GetEquivalenciaEntidadByIdQuery, GenericResult<EquivalenciaEntidadDto>>
{
    private readonly ILogger<GetEquivalenciaEntidadByIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetEquivalenciaEntidadByIdQueryHandler(
        ILogger<GetEquivalenciaEntidadByIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<EquivalenciaEntidadDto>> Handle(GetEquivalenciaEntidadByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<EquivalenciaEntidadDto>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var equivalencia = await unitOfWork.EquivalenciasEntidadRepository.GetAsync(x => x.Id == request.Id);

            if (equivalencia is not null && equivalencia.Any())
            {
                return result.Ok(_mapper.Map<EquivalenciasEntidad, EquivalenciaEntidadDto>(equivalencia.First()));
            }

            return result.NotFound();
        }
        catch(Exception exception)
        {
            _logger.LogError($"Error al obtener la equivalencia entidad con el identificador {request.Id}.", exception);
            return result.Failed(500, $"Error al obtener la equivalencia entidad con el identificador {request.Id}. { exception.Message}");
        }
    }
}
