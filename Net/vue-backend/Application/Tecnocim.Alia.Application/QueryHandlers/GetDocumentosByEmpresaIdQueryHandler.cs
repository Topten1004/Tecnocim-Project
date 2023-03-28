using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using System.Linq;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetDocumentosByEmpresaIdQueryHandler : IRequestHandler<GetDocumentosByEmpresaIdQuery, GenericResult<DocumentoErroresResponse>>
{
    private readonly ILogger<GetDocumentosByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetDocumentosByEmpresaIdQueryHandler(
        ILogger<GetDocumentosByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<DocumentoErroresResponse>> Handle(GetDocumentosByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<DocumentoErroresResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<Alia.Domain.Repositories.IUnitOfWork>();
            var intermediaUnitOfWork = scope.ServiceProvider.GetService<Intermedia.Domain.Repositories.IUnitOfWork>();

            var documentos = await unitOfWork.DocumentoRepository
                .GetIncludeAsync(x => x, x => !x.Deleted.HasValue && x.EmpresaId == request.EmpresaId,
                x => x.OrderByDescending(t => t.Fecha));

            var documentosDtoList = new List<DocumentoErroresDto>();

            if (documentos is not null && documentos.Any())
            {
                documentosDtoList.AddRange(documentos.Select(doc => _mapper.Map<Documento, DocumentoErroresDto>(doc)));

                foreach (var documento in documentosDtoList)
                {
                    var errores = await intermediaUnitOfWork.CoreExtraccionesErroreRepository.GetIncludeAsync(z => z, z => z.ExtraccionId == documento.ExtractorId);
                    documento.Errores = errores?.Select(z => new ErroresResponse(z.Mensaje, z.Traza, z.Bloqueo));
                }
            }

            return result.Ok(new DocumentoErroresResponse { DocumentoErroresDtoList = documentosDtoList });
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener los documentos para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
