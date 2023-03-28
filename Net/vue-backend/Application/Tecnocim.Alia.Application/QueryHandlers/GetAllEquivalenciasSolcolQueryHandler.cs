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

public class GetAllEquivalenciasSolcolQueryHandler : IRequestHandler<GetAllEquivalenciasSolcolQuery, GenericResult<IEnumerable<EquivalenciaSolcolDto>>>
{
    private readonly ILogger<GetAllEquivalenciasSolcolQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetAllEquivalenciasSolcolQueryHandler(
        ILogger<GetAllEquivalenciasSolcolQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<IEnumerable<EquivalenciaSolcolDto>>> Handle(GetAllEquivalenciasSolcolQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<IEnumerable<EquivalenciaSolcolDto>>() { Result = Enumerable.Empty<EquivalenciaSolcolDto>() };

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var equivalencias = await unitOfWork.EquivalenciasSolcolRepository.GetAsync();

            if (equivalencias is not null && equivalencias.Any())
            {
                var equivalenciasDtos = _mapper.Map<IEnumerable<EquivalenciasSolcol>, IEnumerable<EquivalenciaSolcolDto>>(equivalencias);
                return result.Ok(equivalenciasDtos);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al obtener las equivalencias solcol", exception);
            return result.Failed(500, "Error al obtener las equivalencias solcol.");
        }

        return result;
    }
}
