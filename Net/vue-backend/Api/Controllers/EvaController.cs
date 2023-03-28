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
        /// Valor añadido de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Valor añadido de la empresa.</returns>
        /// <response code="200">Devuelve el EVA.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado EVA para ese identificador de empresa.</response>
        [HttpGet("eva/valoranadido/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<EvaValorAnadidoResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetEvaValorAnadido(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var response = await _mediator.Send(new GetEvaValorAnadidoByEmpresaIdQuery(empresaId));

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
        /// Previsión de demandas de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Previsión de demandas de la empresa.</returns>
        /// <response code="200">Devuelve la previsión de demandas.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado valores para ese identificador de empresa.</response>
        [HttpGet("nof/previsiondemandas/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<PrevisionDemandasResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetPrevisionDemandas(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var response = await _mediator.Send(new GetPrevisionDemandasByEmpresaIdQuery(empresaId));

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
        /// NOF medias de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>NOF medias de la empresa.</returns>
        /// <response code="200">Devuelve NOF medias.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado valores para ese identificador de empresa.</response>
        [HttpGet("nof/medias/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<PrevisionDemandasResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetNofMedias(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var response = await _mediator.Send(new GetNofMediasByEmpresaIdQuery(empresaId));

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
        /// Diferencia de Crecimiento de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <param name="incremento">El incremento.</param>
        /// <returns>Diferencia de Crecimiento de la empresa.</returns>
        /// <response code="200">Devuelve la Diferencia de Crecimiento.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado valores para ese identificador de empresa.</response>
        [HttpGet("nof/diferenciacrecimiento/{empresaId}/{incremento}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<NofDirerenciaCrecimientoResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetDiferenciaCrecimiento(int empresaId, decimal incremento)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var response = await _mediator.Send(new GetNofDirerenciaCrecimientoByEmpresaIdQuery(empresaId, incremento));

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
        /// NOF PM Maduración de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>NOF PM Maduración de la empresa.</returns>
        /// <response code="200">Devuelve NOF PM Maduración.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se han encontrado valores para ese identificador de empresa.</response>
        [HttpGet("nof/pmmaduracion/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<PrevisionDemandasResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetNofPMMaduracion(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var response = await _mediator.Send(new GetNofPMMaduracionByEmpresaIdQuery(empresaId));

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
    }
}
