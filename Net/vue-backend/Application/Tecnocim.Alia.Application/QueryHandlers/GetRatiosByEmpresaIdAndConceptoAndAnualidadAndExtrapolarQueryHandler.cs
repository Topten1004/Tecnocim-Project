using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Extensions;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Extensions;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetRatiosByEmpresaIdAndConceptoAndAnualidadAndExtrapolarQueryHandler : IRequestHandler<GetRatiosByEmpresaIdAndConceptoAndAnualidadAndExtrapolarQuery,
    GenericResult<RatioEmpresaConceptoResponse>>
{
    private readonly ILogger<GetRatiosByEmpresaIdAndConceptoAndAnualidadAndExtrapolarQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetRatiosByEmpresaIdAndConceptoAndAnualidadAndExtrapolarQueryHandler(
        ILogger<GetRatiosByEmpresaIdAndConceptoAndAnualidadAndExtrapolarQueryHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<GenericResult<RatioEmpresaConceptoResponse>> Handle(GetRatiosByEmpresaIdAndConceptoAndAnualidadAndExtrapolarQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<RatioEmpresaConceptoResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => x.EmpresaId == request.EmpresaId && !x.Deleted.HasValue, null,
          x => x.Include(y => y.Empresa).Include(y => y.Ratios), false);

            if (documentos is not null && documentos.Any())
            {
                if (request.Usuario is not Usuario usuario)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que pertenece el documento");
                }

                var usuarioExistente = await unitOfWork.UsuarioRepository.GetFirstAsync(x => !x.Deleted.HasValue && x.UsuarioId == usuario.UsuarioId
                && x.Empresas.Any(t => t.EmpresaId == documentos.First().EmpresaId), null, x => x.Empresas);

                if (usuarioExistente is null)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que pertenece el documento");
                }

                var anualidad = request.Anualidad ?? DateTime.UtcNow.Year;

                if (anualidad.ToString().Length != 4)
                {
                    return result.Failed(400, "La anualidad no tiene el formato de año (4 dígitos)");
                }

                var totalRatiosDto = documentos.GetTotalRatiosByConcepto(anualidad, request.Concepto.ToLowerInvariant(), request.Extrapolar ?? true);

                var interpretacion = await unitOfWork.InterpretacionRepository.GetFirstAsync(x => x.Concepto == request.Concepto);

                var ratios = new RatioEmpresaConceptoResponse
                {
                    Ratio = new TotalRatiosStringDto
                    {
                        TotalActual = totalRatiosDto.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                        Tendencia = totalRatiosDto.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior = totalRatiosDto.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                        TendenciaAnterior = totalRatiosDto.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior2 = totalRatiosDto.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                        Divisa = string.Empty,
                        Interpretacion = interpretacion?.ToRatioInterpretacionDto(totalRatiosDto.Tendencia, totalRatiosDto.TendenciaAnterior)!
                    }
                };

                return result.Ok(ratios);
            }

            return result.Ok(new RatioEmpresaConceptoResponse());
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener los ratios para la empresa con id: {request.EmpresaId}, concepto: {request.Concepto}, anualidad: {request.Anualidad}, extrapolar: {request.Extrapolar}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
