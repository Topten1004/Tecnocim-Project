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

public class GetAllEquivalenciasMonedaQueryHandler : IRequestHandler<GetAllEquivalenciasMonedaQuery, GenericResult<IEnumerable<EquivalenciaMonedaDto>>>
{
    private readonly ILogger<GetAllEquivalenciasMonedaQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetAllEquivalenciasMonedaQueryHandler(
        ILogger<GetAllEquivalenciasMonedaQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<IEnumerable<EquivalenciaMonedaDto>>> Handle(GetAllEquivalenciasMonedaQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<IEnumerable<EquivalenciaMonedaDto>>() { Result = Enumerable.Empty<EquivalenciaMonedaDto>() };

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var equivalencias = await unitOfWork.EquivalenciasMonedaRepository.GetAsync();

            if (equivalencias is not null && equivalencias.Any())
            {
                var equivalenciasDtos = _mapper.Map<IEnumerable<EquivalenciasMoneda>, IEnumerable<EquivalenciaMonedaDto>>(equivalencias);
                return result.Ok(equivalenciasDtos);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al obtener las equivalencias moneda", exception);
            return result.Failed(500, "Error al obtener las equivalencias moneda.");
        }

        return result;
    }
}
