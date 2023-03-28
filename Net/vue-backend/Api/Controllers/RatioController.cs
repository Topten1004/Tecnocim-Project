using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;

namespace vue_backend.Controllers
{
    public partial class NegocioController : ControllerBase
    {
        /// <summary>
        /// Ratios de la empresa y el concepto pasados por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <param name="concepto">Concepto especificado.</param>
        /// <param name="anualidad">Año especificado o en su defecto el actual.</param>
        /// <param name="extrapolar">Valor true o false o en su defecto true.</param>
        /// <returns>Ratios Empresa Concepto.</returns>
        /// <response code="200">Devuelve los ratios.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado ratios con esos parámetros.</response>
        [HttpGet("ratio/empresaconcepto/{empresaId}/{concepto}/{anualidad?}/{extrapolar?}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<RatioEmpresaConceptoResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetRatiosEmpresaConcepto(int empresaId, string concepto, int? anualidad = null, bool? extrapolar = null)
        {
            var usuario = Request.HttpContext.Items["User"];
            var response = await _mediator.Send(new GetRatiosByEmpresaIdAndConceptoAndAnualidadAndExtrapolarQuery(empresaId, concepto, anualidad, extrapolar, usuario));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Ebitda Versus servicio / deuda de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <param name="anualidad">Año especificado o en su defecto el actual.</param>
        /// <returns>Ratios Ebitda servicio-deuda.</returns>
        /// <response code="200">Devuelve los ratios.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado Ebitda versus servicio deuda con esos parámetros.</response>
        [HttpGet("ebitda/vsserviciodeuda/{empresaId}/{anualidad?}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<VsServicioDeudaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetEbitdaVsServicioDeuda(int empresaId, int? anualidad)
        {
            var usuario = Request.HttpContext.Items["User"];
            var response = await _mediator.Send(new GetEbitdaVsServicioDeudaByEmpresaIdQuery(empresaId, anualidad, usuario));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Ratios de renatabilidad de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Ratios de rentabilidad.</returns>
        /// <response code="200">Devuelve los ratios.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado ratios con el identificador de empresa pasado por parámetro.</response>
        [HttpGet("ratio/ratiosrentabilidad/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<RatiosRentabilidadDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetRatiosRentabilidad(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var response = await _mediator.Send(new GetRatiosRentabilidadByEmpresaIdQuery(empresaId, usuario));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Ratios de endeudamiento de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Ratios de endeudamiento.</returns>
        /// <response code="200">Devuelve los ratios.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado ratios con el identificador de empresa pasado por parámetro.</response>
        [HttpGet("ratio/ratiosendeudamiento/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<RatiosEndeudamientoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetRatiosEndeudamiento(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var response = await _mediator.Send(new GetRatiosEndeudamientoByEmpresaIdQuery(empresaId, usuario));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Ratios de cuenta resultados de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Ratios de cuenta resultados.</returns>
        /// <response code="200">Devuelve los ratios.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado ratios con el identificador de empresa pasado por parámetro.</response>
        [HttpGet("ratio/ratioscuentaresultados/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<RatiosCuentaResultadoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetRatiosCuentaResultados(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var response = await _mediator.Send(new GetRatiosCuentaResultadoByEmpresaIdQuery(empresaId, usuario));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Ratios de liquidez de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Ratios de liquidez.</returns>
        /// <response code="200">Devuelve los ratios.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado ratios con el identificador de empresa pasado por parámetro.</response>
        [HttpGet("ratio/ratiosliquidez/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<RatioLiquidezResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetRatiosLiquidez(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var response = await _mediator.Send(new GetRatiosLiquidezByEmpresaIdQuery(empresaId, usuario));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Balance de situación de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Balance de situación de la empresa.</returns>
        /// <response code="200">Devuelve el balance de situación.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado valores para ese identificador de empresa.</response>
        [HttpGet("ratio/balancesituacion/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<BalanceSituacionResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetEvaBalanceSituacion(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var response = await _mediator.Send(new GetEvaBalanceSituacionByEmpresaIdQuery(empresaId));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Balance de situación activo de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Balance de situación activo de la empresa.</returns>
        /// <response code="200">Devuelve el balance de situación activo.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado valores para ese identificador de empresa.</response>
        [HttpGet("ratio/balancesituacion/activo/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<BalanceSituacionResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetEvaBalanceSituacionActivo(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var response = await _mediator.Send(new GetEvaBalanceSituacionActivoByEmpresaIdQuery(empresaId));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Balance de situación pasivo de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Balance de situación pasivo de la empresa.</returns>
        /// <response code="200">Devuelve el balance de situación pasivo.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado valores para ese identificador de empresa.</response>
        [HttpGet("ratio/balancesituacion/pasivo/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<BalanceSituacionResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetEvaBalanceSituacionPasivo(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var response = await _mediator.Send(new GetEvaBalanceSituacionPasivoByEmpresaIdQuery(empresaId));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Balance de situación pyg de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Balance de situación pyg de la empresa.</returns>
        /// <response code="200">Devuelve el balance de situación pyg.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado valores para ese identificador de empresa.</response>
        [HttpGet("ratio/balancesituacion/pyg/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<BalanceSituacionResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetEvaBalanceSituacionPyg(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var response = await _mediator.Send(new GetEvaBalanceSituacionPygByEmpresaIdQuery(empresaId));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Ratios de rotación periodos medios de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Ratios de rotación periodos medios.</returns>
        /// <response code="200">Devuelve los ratios de rotación periodos medios.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado ratios con el identificador de empresa pasado por parámetro.</response>
        [HttpGet("ratio/rotacionperiodosmedios/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<RotacionPeriodosMediosResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetRotacionPeriodosMedios(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }
            var response = await _mediator.Send(new GetRotacionPeriodosMediosByEmpresaIdQuery(empresaId));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Interpretación del Ratio por concepto.
        /// </summary>
        /// <returns>Interpretación del Ratio con concepto pasado como parámetro.</returns>
        /// <response code="200">Devuelve la interpretación del ratio con concepto pasdo como parámetro.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado la interpretación del ratio con el concepto pasado por parámetro.</response>
        [HttpGet("ratio/ratio/{concepto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<InterpretacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetRatioByConcepto(string concepto)
        {
            var response = await _mediator.Send(new GetInterpretacionByConceptoQuery(concepto));

            if (response.IsSuccessful && response.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Interpretaciones de Ratios.
        /// </summary>
        /// <returns>Interpretaciones de Ratios.</returns>
        /// <response code="200">Devuelve las interpretaciones de ratios.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("ratio/ratios")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<InterpretacionListDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> GetRatios()
        {
            var response = await _mediator.Send(new GetInterpretacionesRatiosQuery());

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
