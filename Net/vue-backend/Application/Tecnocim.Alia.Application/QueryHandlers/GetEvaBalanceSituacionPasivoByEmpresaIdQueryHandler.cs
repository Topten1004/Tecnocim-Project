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

public class GetEvaBalanceSituacionPasivoByEmpresaIdQueryHandler : IRequestHandler<GetEvaBalanceSituacionPasivoByEmpresaIdQuery,
    GenericResult<BalanceSituacionResponse>>
{
    private readonly ILogger<GetEvaBalanceSituacionPasivoByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetEvaBalanceSituacionPasivoByEmpresaIdQueryHandler(
        ILogger<GetEvaBalanceSituacionPasivoByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<BalanceSituacionResponse>> Handle(GetEvaBalanceSituacionPasivoByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<BalanceSituacionResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var conceptosContabilidad = new List<string> {
                "patrimonio neto",
                "fondos propios",
                "capital",
                "prima de emision",
                "reservas",
                "acciones y participaciones en patrimonio propias",
                "resultados de ejercicios anteriores",
                "otras aportaciones de socios","resultado del ejercicio",
                "dividendo a cuenta",
                "otros instrumentos de patrimonio neto",
                "ajustes por cambios de valor",
                "subvenciones, donaciones y legados recibidos",
                "pasivo no corriente",
                "provisiones a largo plazo",
                "deudas a largo plazo",
                "deudas con entidades de credito a largo plazo",
                "acreedores por arrendamiento financiero a largo plazo",
                "otras deudas a largo plazo",
                "deudas con empresas del grupo y asociadas a largo plazo",
                "pasivos por impuesto diferido",
                "periodificaciones a largo plazo",
                "deuda con caracteristicas especiales a largo plazo",
                "acreedores comerciales no corrientes",
                "pasivo corriente",
                "pasivos vinculados con activos no corrientes mantenidos para la venta",
                "provisiones a corto plazo",
                "deudas a corto plazo",
                "deudas con entidades de credito a corto plazo",
                "acreedores por arrendamiento financiero a corto plazo",
                "otras deudas a corto plazo",
                "deudas con empresas del grupo y asociadas a corto plazo",
                "acreedores comerciales y otras cuentas a pagar",
                "periodificaciones a corto plazo_pasivo",
                "deuda con caracteristicas especiales a corto plazo",
                "total patrimonio neto y pasivo"
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

                var ingresosGastos = new BalanceSituacionResponse { Nombre = "Balance de situación - Pasivo", List = list, Columnas = columnas };

                return result.Ok(ingresosGastos);
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener el balance de situación pasivo para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
