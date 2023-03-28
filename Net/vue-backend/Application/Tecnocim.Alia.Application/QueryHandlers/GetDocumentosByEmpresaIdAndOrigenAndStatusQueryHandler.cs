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

public class GetDocumentosByEmpresaIdAndOrigenAndStatusQueryHandler : IRequestHandler<GetDocumentosByEmpresaIdAndOrigenAndStatusQuery,
    GenericResult<DocumentoErroresResponse>>
{
    private readonly ILogger<GetDocumentosByEmpresaIdAndOrigenAndStatusQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetDocumentosByEmpresaIdAndOrigenAndStatusQueryHandler(
        ILogger<GetDocumentosByEmpresaIdAndOrigenAndStatusQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<DocumentoErroresResponse>> Handle(GetDocumentosByEmpresaIdAndOrigenAndStatusQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<DocumentoErroresResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var documentos = await unitOfWork.DocumentoRepository.GetByEmpresaIdAndOrigenAndStatus(request.EmpresaId, request.Origen, request.Status);

            if (documentos is { } && documentos.Any())
            {
                var documentosDtos = _mapper.Map<IEnumerable<Documento>, IEnumerable<DocumentoErroresDto>>(documentos);

                var intermedia_unitOfWork = scope.ServiceProvider.GetService<Intermedia.Domain.Repositories.IUnitOfWork>();
                
                foreach (var elem in documentosDtos)
                {
                    var errores = await intermedia_unitOfWork.CoreExtraccionesErroreRepository.GetIncludeAsync(z => z, z => z.ExtraccionId == elem.ExtractorId);
                    elem.Errores = errores?.Select(z => new ErroresResponse(z.Mensaje, z.Traza, z.Bloqueo));
                }

                return result.Ok(new DocumentoErroresResponse { DocumentoErroresDtoList = documentosDtos });
            }

            return result.NotFound();

        }
        catch (Exception exception)
        {
            var message = $"Error al obtener los documentos para la empresa con id: {request.EmpresaId}, origen: {request.Origen}, status: {request.Status}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
