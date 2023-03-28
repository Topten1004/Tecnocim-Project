using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.CommandHandlers
{
    public class CreateContratoCommandHandler : IRequestHandler<CreateContratoCommand, GenericResult<CreateContratoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateContratoCommandHandler> _logger;

        public CreateContratoCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateContratoCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResult<CreateContratoResponse>> Handle(CreateContratoCommand request, CancellationToken cancellationToken)
        {
            var result = new GenericResult<CreateContratoResponse>();
            try
            {
                // validación: el pool con Id= Cuenta, tiene que tener el ContratoId a null
                var pool = await _unitOfWork.PoolRepository.GetFirstAsync(x => x.PoolId == request.contrato.Cuenta);
                if (pool != null && pool.ContratoId.HasValue)
                {
                    var message = $"El pool sobre el que se intenta asignar un contrato ya tiene asignado el contrato {pool.ContratoId}";
                    return result.Failed(404, message);
                }

                if (request.contrato.Cuenta2.HasValue)
                {
                    var pool2 = await _unitOfWork.PoolRepository.GetFirstAsync(x => x.PoolId == request.contrato.Cuenta2);
                    if (pool2 != null && pool2.ContratoId.HasValue)
                    {
                        var message = $"El pool sobre el que se intenta asignar un contrato ya tiene asignado el contrato {pool2.ContratoId}";
                        return result.Failed(404, message);
                    }
                }

                Contrato? contrato = null;
                await Task.Run(() =>
                {
                    contrato = _mapper.Map<CreateContratoRequest, Contrato>(request.contrato);

                    _unitOfWork.ContratoRepository.Insert(contrato);
                    _unitOfWork.Commit();
                }, cancellationToken);

                await Task.Run(async () =>
                {
                    pool = await _unitOfWork.PoolRepository.GetFirstAsync(x => x.PoolId == request.contrato.Cuenta);
                    if (pool != null)
                    {
                        pool.ContratoId = contrato?.ContratoId;
                        pool.Updated = DateTime.UtcNow;
                        _unitOfWork.PoolRepository.Update(pool);
                    }

                    if (request.contrato.Cuenta2.HasValue)
                    {
                        var pool2 = await _unitOfWork.PoolRepository.GetFirstAsync(x => x.PoolId == request.contrato.Cuenta2);
                        if (pool2 != null)
                        {
                            pool2.ContratoId = contrato?.ContratoId;
                            pool2.Updated = DateTime.UtcNow;
                            _unitOfWork.PoolRepository.Update(pool2);
                        }
                    }

                    var cirbe = await _unitOfWork.CirbeRepository.GetFirstAsync(x => x.CirbeId == request.contrato.Cirbe);
                    if (cirbe != null)
                    {
                        cirbe.ContratoId = contrato?.ContratoId;
                        cirbe.Updated = DateTime.UtcNow;
                        _unitOfWork.CirbeRepository.Update(cirbe);
                    }
                    _unitOfWork.Commit();
                }, cancellationToken);

                var cuotas = await CreateCuotasAsync(contrato, pool);

                if (cuotas.Cuotas is not null && cuotas.Cuotas.Any())
                {
                    await Task.Run(() =>
                    {
                        _unitOfWork.CuotaRepository.InsertRange(cuotas.Cuotas.ToList());

                        contrato.PlazosAmortizacion = cuotas.PlazoAmortizacion;
                        _unitOfWork.ContratoRepository.Update(contrato);
                        _unitOfWork.Commit();
                    }, cancellationToken);
                }

                return result.Ok(new CreateContratoResponse { ContratoId = contrato != null ? contrato.ContratoId : 0 });
            }
            catch (Exception exception)
            {
                _logger.LogError("Error al insertar el contrato", exception);
                return result.Failed(500, $"Error al insertar el contrato: {Environment.NewLine}{exception.Message}");
            }
        }

        private async Task<(IEnumerable<Cuota> Cuotas, int PlazoAmortizacion)> CreateCuotasAsync(Contrato contrato, Pool pool)
        {
            if (contrato is null)
            {
                throw new ArgumentNullException("El contrato es nulo");
            }

            var poolToConsider = contrato.Pools.FirstOrDefault(x => x.PoolId == pool.PoolId);

            if (poolToConsider is null)
            {
                throw new ApplicationException("El pool no existe");
            }

            // cuenta de largo plazo
            if (!poolToConsider.Cuenta.StartsWith("170") && !poolToConsider.Cuenta.StartsWith("171"))
            {
                return (Enumerable.Empty<Cuota>(), 0);
            }

            var numeroMeses = (contrato.Vencimiento.Year - contrato.Inicio.Year) * 12 + (contrato.Vencimiento.Month - contrato.Inicio.Month);

            var vencimiento = new DateTime(contrato.Vencimiento.Year, contrato.Vencimiento.Month, contrato.Vencimiento.Day);
            var inicio = new DateTime(contrato.Inicio.Year, contrato.Inicio.Month, contrato.Inicio.Day);

            var plazoAmortizacion = (int)Math.Floor((double)(numeroMeses - contrato.Carencia) / Convert.ToDouble(contrato.EquivalenciasPeriodificacionId));

            plazoAmortizacion = plazoAmortizacion == 0 ? 1 : plazoAmortizacion;

            var temporal = Math.Pow(Convert.ToDouble((contrato.Precio / 100) + 1), Convert.ToDouble(contrato.EquivalenciasPeriodificacionId) / 12) - 1;

            var cuotas = new List<Cuota>();

            var fechaInicial = contrato.Inicio;

            for (var i = 1; i < Math.Round(numeroMeses / Convert.ToDouble(contrato.EquivalenciasPeriodificacionId), 0, MidpointRounding.AwayFromZero) + 1; i++)
            {
                var cuota = new Cuota
                {
                    ContratoId = contrato.ContratoId,
                    Fecha = CalculateFechaCuota(contrato.EquivalenciasPeriodificacionId, fechaInicial)
                };

                if (i > 0 && i < contrato.Carencia + 1)  // TODO: la carencia son meses
                {
                    cuota.Carencia = true;   // =100*15/100*(3/12)
                    cuota.Importe = contrato.Limite * contrato.Precio / 100 * Convert.ToDecimal(Convert.ToDouble(contrato.EquivalenciasPeriodificacionId) / 12);
                }
                else
                {
                    cuota.Carencia = false;
                    cuota.Importe = (contrato.Limite * Convert.ToDecimal(temporal)) / 1 - Convert.ToDecimal(Math.Pow(1 + temporal, -1 * plazoAmortizacion));
                }

                cuotas.Add(cuota);
                fechaInicial = DateOnly.FromDateTime(cuota.Fecha);
            }

            return (cuotas, plazoAmortizacion);
        }

        private DateTime CalculateFechaCuota(short periodificacion, DateOnly inicio)
        {
            var fecha = periodificacion switch
            {
                1 => inicio.AddMonths(1),
                3 => inicio.AddMonths(3),
                4 => inicio.AddMonths(4),
                6 => inicio.AddMonths(6),
                12 => inicio.AddMonths(12),
                _ => inicio.AddMonths(1),
            };

            return new DateTime(fecha.Year, fecha.Month, 1);
        }
    }
}
