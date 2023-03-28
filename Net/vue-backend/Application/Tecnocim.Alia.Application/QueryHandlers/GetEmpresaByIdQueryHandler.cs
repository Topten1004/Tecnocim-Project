using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetEmpresaByIdQueryHandler : IRequestHandler<GetEmpresaByIdQuery, EmpresaDto>
{
    private readonly ILogger<GetEmpresaByIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetEmpresaByIdQueryHandler(
        ILogger<GetEmpresaByIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<EmpresaDto> Handle(GetEmpresaByIdQuery request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

        var empresa = await unitOfWork.EmpresaRepository.GetFirstAsync(x => x.EmpresaId == request.Id && !x.Deleted.HasValue);

        if (empresa is not null)
        {
            return _mapper.Map<Empresa, EmpresaDto>(empresa);
        }

        return new EmpresaDto();
    }
}
