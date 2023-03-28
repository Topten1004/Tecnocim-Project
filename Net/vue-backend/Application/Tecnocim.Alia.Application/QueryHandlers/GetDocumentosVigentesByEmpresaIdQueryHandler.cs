using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetDocumentosVigentesByEmpresaIdQueryHandler : IRequestHandler<GetDocumentosVigentesByEmpresaIdQuery, GenericResult<DocumentoErroresResponse>>
{
    private readonly ILogger<GetDocumentosVigentesByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetDocumentosVigentesByEmpresaIdQueryHandler(
        ILogger<GetDocumentosVigentesByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<DocumentoErroresResponse>> Handle(GetDocumentosVigentesByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<DocumentoErroresResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<Alia.Domain.Repositories.IUnitOfWork>();
            var intermediaUnitOfWork = scope.ServiceProvider.GetService<Intermedia.Domain.Repositories.IUnitOfWork>();

            var anhoAnterior = (DateTime.UtcNow.Year - 1);
            var documentoAnterior = await unitOfWork.DocumentoRepository
                .GetFirstAsync(x => !x.Deleted.HasValue && x.EmpresaId == request.EmpresaId && x.Origen == Origen.Modelo200.ToString() && x.Fecha.Year == anhoAnterior,
                x => x.OrderByDescending(t => t.Fecha));

            var anhoDosAnterior = (DateTime.UtcNow.Year - 2);
            var documentoDosAnterior = await unitOfWork.DocumentoRepository
                .GetFirstAsync(x => !x.Deleted.HasValue && x.EmpresaId == request.EmpresaId && x.Origen == Origen.Modelo200.ToString() && x.Fecha.Year == anhoDosAnterior,
                x => x.OrderByDescending(t => t.Fecha));

            var documentoBss = await unitOfWork.DocumentoRepository
                .GetFirstAsync(x => !x.Deleted.HasValue && x.EmpresaId == request.EmpresaId && x.Origen == Origen.BSS.ToString(), x => x.OrderByDescending(t => t.Fecha));

            var documentoCirbe = await unitOfWork.DocumentoRepository
                .GetFirstAsync(x => !x.Deleted.HasValue && x.EmpresaId == request.EmpresaId && x.Origen == Origen.Cirbe.ToString(), x => x.OrderByDescending(t => t.Fecha));

            var documentosDtoList = new List<DocumentoErroresDto>();

            if (documentoAnterior != null)
            {
                documentosDtoList.Add(_mapper.Map<Documento, DocumentoErroresDto>(documentoAnterior));
            }

            if (documentoDosAnterior != null)
            {
                documentosDtoList.Add(_mapper.Map<Documento, DocumentoErroresDto>(documentoDosAnterior));
            }

            if (documentoBss != null)
            {
                documentosDtoList.Add(_mapper.Map<Documento, DocumentoErroresDto>(documentoBss));
            }

            if (documentoCirbe != null)
            {
                documentosDtoList.Add(_mapper.Map<Documento, DocumentoErroresDto>(documentoCirbe));
            }

            foreach (var documento in documentosDtoList)
            {
                var errores = await intermediaUnitOfWork.CoreExtraccionesErroreRepository.GetIncludeAsync(z => z, z => z.ExtraccionId == documento.ExtractorId);
                documento.Errores = errores?.Select(z => new ErroresResponse(z.Mensaje, z.Traza, z.Bloqueo));
            }

            return result.Ok(new DocumentoErroresResponse { DocumentoErroresDtoList = documentosDtoList });
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener los documentos vigentes para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
