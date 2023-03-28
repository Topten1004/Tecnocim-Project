using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Extensions;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetPoolBancarioEstadosByEmpresaIdQueryHandler : IRequestHandler<GetPoolBancarioEstadosByEmpresaIdQuery, GenericResult<PoolBancarioEstadosResponse>>
{
    private readonly ILogger<GetPoolBancarioEstadosByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetPoolBancarioEstadosByEmpresaIdQueryHandler(
        ILogger<GetPoolBancarioEstadosByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<PoolBancarioEstadosResponse>> Handle(GetPoolBancarioEstadosByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<PoolBancarioEstadosResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var pools = await unitOfWork.PoolRepository.GetEstadosByEmpresaId(request.EmpresaId);

            if (pools is { } && pools.Any())
            {
                return result.Ok(new PoolBancarioEstadosResponse 
                { 
                    PoolBancarioList = pools.Select(x => new PoolBancarioEstadosDto
                    {
                        PoolId= x.PoolId,
                        Cuenta = x.Cuenta,
                        Concepto= x.Concepto,
                        Dispuesto = x.Dispuesto.HasValue ? x.Dispuesto.Value.ToTwoDecimalAndSymbolFormat('c') : 0m.ToTwoDecimalAndSymbolFormat('c'),
                        ContratoId= x.ContratoId,
                        Estado = x.ContratoId.HasValue
                    }) 
                });
            }

            return result.NotFound();

        }
        catch (Exception exception)
        {
            var message = $"Error al obtener el pool bancario estados para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
