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

public class GetAllEquivalenciasProductoQueryHandler : IRequestHandler<GetAllEquivalenciasProductoQuery, GenericResult<IEnumerable<EquivalenciaProductoDto>>>
{
    private readonly ILogger<GetAllEquivalenciasProductoQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetAllEquivalenciasProductoQueryHandler(
        ILogger<GetAllEquivalenciasProductoQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<IEnumerable<EquivalenciaProductoDto>>> Handle(GetAllEquivalenciasProductoQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<IEnumerable<EquivalenciaProductoDto>>() { Result = Enumerable.Empty<EquivalenciaProductoDto>() };

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var equivalencias = await unitOfWork.EquivalenciasProductoRepository.GetAsync();

            if (equivalencias is not null && equivalencias.Any())
            {
                var equivalenciasDtos = _mapper.Map<IEnumerable<EquivalenciasProducto>, IEnumerable<EquivalenciaProductoDto>>(equivalencias);
                return result.Ok(equivalenciasDtos);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al obtener las equivalencias producto", exception);
            return result.Failed(500, "Error al obtener las equivalencias producto.");
        }

        return result;
    }
}
