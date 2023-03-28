using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;

namespace vue_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmpresaConfiguracionesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmpresaConfiguracionesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Listado de las configuraciones de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Listado de configuraciones.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado la configuración para la empresa con ese identificador.</response>
        [HttpGet("empresasconfiguraciones/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<IEnumerable<EmpresaConfiguracionesDto>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        public async Task<IActionResult> GetByEmpresaId(int empresaId)
        {
            var empresasConfiguraciones = await _mediator.Send(new GetEmpresaConfiguracionesByEmpresaIdQuery(empresaId));

            if (!empresasConfiguraciones.IsSuccessful)
            {
                return BadRequest(empresasConfiguraciones);
            }

            if (empresasConfiguraciones.IsSuccessful && empresasConfiguraciones.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok(empresasConfiguraciones);
        }
    }
}
