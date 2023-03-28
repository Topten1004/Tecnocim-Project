using System.Text.RegularExpressions;
using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Extensions;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetEvaBalanceSituacionPygByEmpresaIdQueryHandler : IRequestHandler<GetEvaBalanceSituacionPygByEmpresaIdQuery,
    GenericResult<BalanceSituacionResponse>>
{
    private readonly ILogger<GetEvaBalanceSituacionPygByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetEvaBalanceSituacionPygByEmpresaIdQueryHandler(
        ILogger<GetEvaBalanceSituacionPygByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<BalanceSituacionResponse>> Handle(GetEvaBalanceSituacionPygByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<BalanceSituacionResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var conceptosContabilidad = new List<string> {
               "importe neto de la cifra de negocios",
                "variacion de existencias de productos terminados y en curso de fabricacion",
                "trabajos realizados por la empresa para su activo",
                "aprovisionamientos",
                "otros ingresos de explotacion",
                "gastos de personal",
                "otros gastos de explotacion",
                "amortizacion del inmovilizado",
                "imputacion de subvenciones de inmovilizado no financiero y otras",
                "excesos de provisiones",
                "deterioro y resultado por enajenaciones del inmovilizado",
                "diferencia negativa de combinaciones de negocio",
                "otros resultados","resultado de explotacion",
                "ingresos financieros",
                "gastos financieros",
                "variacion de valor razonable en instrumentos financieros",
                "diferencias de cambio",
                "deterioro y resultado por enajenaciones de instrumentos financieros",
                "otros ingresos y gastos de caracter financiero",
                "resultado financiero",
                "resultado antes de impuestos",
                "impuestos sobre beneficios",
                "resultado del ejercicio procedente de operaciones continuadas",
                "costes fijos",
                "margen de contribucion",
                "ebitda"
            };
            var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue
            && x.EmpresaId == request.EmpresaId, null, x => x.Include(e => e.Contabilidades));

            var anualidad = DateTime.UtcNow.Year;

            if (documentos is { } && documentos.Any())
            {
                var documentoAnhoActual = documentos.Where(x => x.Fecha.Year == anualidad)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();

                // comprobación de la anualidad
                while (documentoAnhoActual is null && anualidad >= 2019)
                {
                    anualidad--;
                    documentoAnhoActual = documentos.Where(x => x.Fecha.Year == anualidad)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                }

                var documentoAnyoAnterior = documentos.Where(x => x.Fecha.Year == anualidad - 1)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                var documentoHaceDosAnyos = documentos.Where(x => x.Fecha.Year == anualidad - 2)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                var list = new List<TotalBalanceSituacionStringDto>();

                foreach (var concepto in conceptosContabilidad)
                {
                    var valorActual = documentoAnhoActual?.Contabilidades?.FirstOrDefault(a => a.Concepto == concepto)?.Magnitud ?? 0;
                    var valorAnyoAnterior = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(a => a.Concepto == concepto)?.Magnitud ?? 0;
                    var valorHaceDosAnyos = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(a => a.Concepto == concepto)?.Magnitud ?? 0;
                    var tendencia = valorAnyoAnterior != 0 ? (valorActual - valorAnyoAnterior) / valorAnyoAnterior : 0;
                    var tendenciaAnterior = valorHaceDosAnyos != 0 ? (valorAnyoAnterior - valorHaceDosAnyos) / valorHaceDosAnyos : 0;
                    var configuracionContabilidad = await unitOfWork.ContabilidadConfiguracionRepository.GetFirstAsync(x => x.Concepto == concepto);

                    list.Add(new TotalBalanceSituacionStringDto
                    {
                        Concepto = concepto,
                        ValorActual = valorActual.ToTwoDecimalAndSymbolFormat('c'),
                        Tendencia = tendencia.ToTwoDecimalAndSymbolFormat('p'),
                        ValorAnterior = valorAnyoAnterior.ToTwoDecimalAndSymbolFormat('c'),
                        TendenciaAnterior = tendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                        ValorAnterior2 = valorHaceDosAnyos.ToTwoDecimalAndSymbolFormat('c'),
                        Divisa = string.Empty,
                        Configuracion = configuracionContabilidad is null ? null :
                        new ContabilidadConfiguracionDto
                        {
                            Grupo = configuracionContabilidad.Grupo,
                            Prioridad = configuracionContabilidad.Prioridad,
                            Etiqueta = configuracionContabilidad.Etiqueta
                        }
                    });
                }

                var actual = documentoAnhoActual is null ? Models.Constants.SinDatos : documentoAnhoActual?.Fecha.ToString("dd-MM-yyyy");
                var anterior = documentoAnyoAnterior is null ? Models.Constants.SinDatos : documentoAnyoAnterior?.Fecha.ToString("dd-MM-yyyy");
                var haceDosAnyos = documentoHaceDosAnyos is null ? Models.Constants.SinDatos : documentoHaceDosAnyos?.Fecha.ToString("dd-MM-yyyy");

                var columnas = $"{actual},Tendencia,{anterior},Tendencia,{haceDosAnyos}";

                var ingresosGastos = new BalanceSituacionResponse { Nombre = "Balance de situación - PyG", List = list, Columnas = columnas };

                return result.Ok(ingresosGastos);
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener el balance de situación pyg para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
