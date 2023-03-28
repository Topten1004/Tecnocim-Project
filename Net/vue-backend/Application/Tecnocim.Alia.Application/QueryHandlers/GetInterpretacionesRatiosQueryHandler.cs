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

public class GetInterpretacionesRatiosQueryHandler : IRequestHandler<GetInterpretacionesRatiosQuery, GenericResult<InterpretacionListDto>>
{
    private readonly ILogger<GetInterpretacionesRatiosQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetInterpretacionesRatiosQueryHandler(
        ILogger<GetInterpretacionesRatiosQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<InterpretacionListDto>> Handle(GetInterpretacionesRatiosQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<InterpretacionListDto>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var interpretaciones = await unitOfWork.InterpretacionRepository.GetAsync();

            var interpretacionesDto = _mapper.Map<IEnumerable<Interpretacion>, IEnumerable<InterpretacionDto>>(interpretaciones);
            return result.Ok(new InterpretacionListDto { Ratios = interpretacionesDto });
        }
        catch (Exception exception)
        {
            var message = "Error al obtener el maestro de ratios";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
