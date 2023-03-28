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

public class GetEquivalenciaNatintervByIdQueryHandler : IRequestHandler<GetEquivalenciaNatintervByIdQuery, GenericResult<EquivalenciaDto>>
{
    private readonly ILogger<GetEquivalenciaNatintervByIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetEquivalenciaNatintervByIdQueryHandler(
        ILogger<GetEquivalenciaNatintervByIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<EquivalenciaDto>> Handle(GetEquivalenciaNatintervByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<EquivalenciaDto>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var equivalencia = await unitOfWork.EquivalenciasNatintervRepository.GetAsync(x => x.Id == request.Id);

            if (equivalencia is not null && equivalencia.Any())
            {
                return result.Ok(_mapper.Map<EquivalenciasNatinterv, EquivalenciaDto>(equivalencia.First()));
            }

            return result.NotFound();
        }
        catch(Exception exception)
        {
            _logger.LogError($"Error al obtener la equivalencia natinterv con el identificador {request.Id}.", exception);
            return result.Failed(500, $"Error al obtener la equivalencia natinterv con el identificador {request.Id}. { exception.Message}");
        }
    }
}
