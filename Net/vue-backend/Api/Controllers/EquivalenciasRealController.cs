using Microsoft.AspNetCore.Mvc;
using Tecnocim.Alia.Application.Queries;

namespace vue_backend.Controllers
{
    public partial class MaestrosController : ControllerBase
    {
        /// <summary>
        /// Devuelve la lista de todas las equivalencias real.
        /// </summary>
        /// <returns>Lista de todas las equivalencias real.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        //[Authorize]
        [HttpGet("equivalencias/real")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllEquivalenciasReal()
        {
            var equivalencias = await _mediator.Send(new GetAllEquivalenciasRealQuery());

            if (!equivalencias.IsSuccessful)
            {
                return BadRequest(equivalencias);
            }

            return Ok(equivalencias);
        }

        /// <summary>
        /// Devuelve la equivalencia real con el identificador pasado por parámetro.
        /// </summary>
        /// <returns>La equivalencia real con el identificador pasado por parámetro.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado la equivalencia real con ese identificador.</response>
        //[Authorize]
        [HttpGet("equivalencias/real/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetEquivalenciaRealById(int id)
        {
            var equivalencia = await _mediator.Send(new GetEquivalenciaRealByIdQuery(id));

            if (!equivalencia.IsSuccessful)
            {
                return BadRequest(equivalencia);
            }

            if (equivalencia.IsSuccessful && equivalencia.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            return Ok(equivalencia);
        }
    }
}
