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

public class GetInterpretacionByConceptoQueryHandler : IRequestHandler<GetInterpretacionByConceptoQuery, GenericResult<InterpretacionDto>>
{
    private readonly ILogger<GetInterpretacionByConceptoQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetInterpretacionByConceptoQueryHandler(
        ILogger<GetInterpretacionByConceptoQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<InterpretacionDto>> Handle(GetInterpretacionByConceptoQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<InterpretacionDto>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            if (string.IsNullOrEmpty(request.Concepto))
            {
                return result.Failed(400, "Debe especificar el concepto");
            }

            var ratioMaestro = await unitOfWork.InterpretacionRepository.GetFirstAsync(x => x.Concepto == request.Concepto);

            if(ratioMaestro is null)
            {
                return result.NotFound();
            }

            var ratioMaestrosDto = _mapper.Map<Interpretacion, InterpretacionDto>(ratioMaestro);
            return result.Ok(ratioMaestrosDto);
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener el maestro del ratio con concepto {request.Concepto}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
