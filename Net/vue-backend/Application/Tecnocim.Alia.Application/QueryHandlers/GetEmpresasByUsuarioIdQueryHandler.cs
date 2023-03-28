using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetEmpresasByUsuarioIdQueryHandler : IRequestHandler<GetEmpresasByUsuarioIdQuery, IEnumerable<EmpresaUsuarioDto>>
{
    private readonly ILogger<GetEmpresasByUsuarioIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetEmpresasByUsuarioIdQueryHandler(
        ILogger<GetEmpresasByUsuarioIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmpresaUsuarioDto>> Handle(GetEmpresasByUsuarioIdQuery request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

        var empresas = await unitOfWork.UsuarioRepository.GetEmpresasByUsuarioId(request.UsuarioId);

        var result = new List<EmpresaUsuarioDto>();

        if (empresas is not null && empresas.Any())
        {
            foreach (var empresa in empresas)
            {
                var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => x.EmpresaId == empresa.EmpresaId && !x.Deleted.HasValue, null,
                   x => x.Include(y => y.Empresa), false);

                var resultCode = 0;
                if (documentos is not null && documentos.Any())
                {
                    var pools = await unitOfWork.PoolRepository.GetByEmpresaId(empresa.EmpresaId);
                    if (pools is not null)
                    {
                        resultCode = !pools.Any() ? 1 : 2;
                    }
                }

                var empresaUsuarioDto = _mapper.Map<Empresa, EmpresaUsuarioDto>(empresa);
                empresaUsuarioDto.StatusWeb = resultCode;

                result.Add(empresaUsuarioDto);
            }

            return result;
        }

        return Enumerable.Empty<EmpresaUsuarioDto>();
    }
}
