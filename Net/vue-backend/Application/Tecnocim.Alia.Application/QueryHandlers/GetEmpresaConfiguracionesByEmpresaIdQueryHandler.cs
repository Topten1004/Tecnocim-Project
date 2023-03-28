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

public class GetEmpresaConfiguracionesByEmpresaIdQueryHandler : IRequestHandler<GetEmpresaConfiguracionesByEmpresaIdQuery, GenericResult<IEnumerable<EmpresaConfiguracionesDto>>>
{
    private readonly ILogger<GetEmpresaConfiguracionesByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetEmpresaConfiguracionesByEmpresaIdQueryHandler(
        ILogger<GetEmpresaConfiguracionesByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<IEnumerable<EmpresaConfiguracionesDto>>> Handle(GetEmpresaConfiguracionesByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<IEnumerable<EmpresaConfiguracionesDto>>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var empresaConfiguraciones = await unitOfWork.EmpresaConfiguracionesRepository.GetAsync(x => x.Empresa.EmpresaId == request.EmpresaId && !x.Deleted.HasValue);

            if (empresaConfiguraciones is not null && empresaConfiguraciones.Any())
            {
                return result.Ok(_mapper.Map<IEnumerable<EmpresaConfiguraciones>, IEnumerable<EmpresaConfiguracionesDto>>(empresaConfiguraciones));
            }

            return result.NotFound();
        }
        catch(Exception exception)
        {
            _logger.LogError("Error al obtener configuraciones de las empresas.", exception);
            return result.Failed(500, $"Error al obtener configuraciones de las empresas. {exception.Message}");
        }
    }
}
