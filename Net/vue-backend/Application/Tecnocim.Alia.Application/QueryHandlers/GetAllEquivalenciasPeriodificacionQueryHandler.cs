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

public class GetAllEquivalenciasPeriodificacionQueryHandler : IRequestHandler<GetAllEquivalenciasPeriodificacionQuery, GenericResult<IEnumerable<EquivalenciaPeriodificacionDto>>>
{
    private readonly ILogger<GetAllEquivalenciasPeriodificacionQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetAllEquivalenciasPeriodificacionQueryHandler(
        ILogger<GetAllEquivalenciasPeriodificacionQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<IEnumerable<EquivalenciaPeriodificacionDto>>> Handle(GetAllEquivalenciasPeriodificacionQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<IEnumerable<EquivalenciaPeriodificacionDto>>() { Result = Enumerable.Empty<EquivalenciaPeriodificacionDto>() };

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var equivalencias = await unitOfWork.EquivalenciasPeriodificacionRepository.GetAsync();

            if (equivalencias is not null && equivalencias.Any())
            {
                var equivalenciasDtos = _mapper.Map<IEnumerable<EquivalenciasPeriodificacion>, IEnumerable<EquivalenciaPeriodificacionDto>>(equivalencias);
                return result.Ok(equivalenciasDtos);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al obtener las equivalencias periodificación", exception);
            return result.Failed(500, "Error al obtener las equivalencias periodificación.");
        }

        return result;
    }
}
