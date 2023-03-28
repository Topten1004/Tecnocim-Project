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

public class GetAllEquivalenciasTipoQueryHandler : IRequestHandler<GetAllEquivalenciasTipoQuery, GenericResult<IEnumerable<EquivalenciaDto>>>
{
    private readonly ILogger<GetAllEquivalenciasTipoQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetAllEquivalenciasTipoQueryHandler(
        ILogger<GetAllEquivalenciasTipoQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<IEnumerable<EquivalenciaDto>>> Handle(GetAllEquivalenciasTipoQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<IEnumerable<EquivalenciaDto>>() { Result = Enumerable.Empty<EquivalenciaDto>() };

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var equivalencias = await unitOfWork.EquivalenciasTipoRepository.GetAsync();

            if (equivalencias is not null && equivalencias.Any())
            {
                var equivalenciasDtos = _mapper.Map<IEnumerable<EquivalenciasTipo>, IEnumerable<EquivalenciaDto>>(equivalencias);
                return result.Ok(equivalenciasDtos);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al obtener las equivalencias tipo", exception);
            return result.Failed(500, "Error al obtener las equivalencias tipo.");
        }

        return result;
    }
}
