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

public class GetEmpresasQueryHandler : IRequestHandler<GetEmpresasQuery, GenericResult<IEnumerable<EmpresaDto>>>
{
    private readonly ILogger<GetEmpresasQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetEmpresasQueryHandler(
        ILogger<GetEmpresasQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<IEnumerable<EmpresaDto>>> Handle(GetEmpresasQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<IEnumerable<EmpresaDto>>() { Result = Enumerable.Empty<EmpresaDto>() };

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var empresas = await unitOfWork.EmpresaRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue, x => x.OrderBy(y => y.Nombre));

            if (empresas is not null && empresas.Any())
            {
                var empresasDtos = _mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaDto>>(empresas);
                return result.Ok(empresasDtos);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al obtener todas las empresas", exception);
            return result.Failed(500, $"Error al obtener todas las empresas. {exception.Message}");
        }

        return result;
    }
}
