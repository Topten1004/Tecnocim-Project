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

public class GetEquivalenciaPlazoByIdQueryHandler : IRequestHandler<GetEquivalenciaPlazoByIdQuery, GenericResult<EquivalenciaDto>>
{
    private readonly ILogger<GetEquivalenciaPlazoByIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetEquivalenciaPlazoByIdQueryHandler(
        ILogger<GetEquivalenciaPlazoByIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<EquivalenciaDto>> Handle(GetEquivalenciaPlazoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<EquivalenciaDto>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var equivalencia = await unitOfWork.EquivalenciasPlazoRepository.GetAsync(x => x.Id == request.Id);

            if (equivalencia is not null && equivalencia.Any())
            {
                return result.Ok(_mapper.Map<EquivalenciasPlazo, EquivalenciaDto>(equivalencia.First()));
            }

            return result.NotFound();
        }
        catch(Exception exception)
        {
            _logger.LogError($"Error al obtener la equivalencia plazo con el identificador {request.Id}.", exception);
            return result.Failed(500, $"Error al obtener la equivalencia plazo con el identificador {request.Id}. { exception.Message}");
        }
    }
}
