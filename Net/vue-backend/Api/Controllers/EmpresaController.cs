using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;

namespace vue_backend.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmpresaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmpresaController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Listado de las empresas del usuario pasado por parámetro.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <returns>Listado de empresas.</returns>
        /// <response code="200">Devuelve el listado de empresas.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("empresas/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByUsuarioId(int usuarioId)
        {
            var empresas = await _mediator.Send(new GetEmpresasByUsuarioIdQuery(usuarioId));

            return Ok(empresas);
        }

        /// <summary>
        /// Obtiene la empresa con el identificador pasado por parámetro.
        /// </summary>
        /// <param name="id">Identificador de la empresa.</param>
        /// <returns>La empresa.</returns>
        /// <response code="200">Devuelve la empresa.</response>
        /// <response code="404">No existe la empresa con ese identificador.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("empresa/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var empresa = await _mediator.Send(new GetEmpresaByIdQuery(id));

            if(empresa is null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        /// <summary>
        /// Obtiene el estado de la web la empresa con el identificador pasado por parámetro.
        /// </summary>
        /// <param name="id">Identificador de la empresa.</param>
        /// <returns>Estado de la empresa.</returns>
        /// <response code="200">Devuelve el estado de la empresa.</response>
        /// <response code="404">No existe la empresa con ese identificador.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [Authorize]
        [HttpGet("statusweb/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> GetStatusWebById(int id)
        {
            var usuario = Request.HttpContext.Items["User"];
            var response = await _mediator.Send(new GetStatusWebEmpresaByIdQuery(id, usuario));

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response.Result);
        }

        /// <summary>
        /// Devuelve la lista de todas las empresas.
        /// </summary>
        /// <returns>Lista de todas las empresas.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("empresas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var empresas = await _mediator.Send(new GetEmpresasQuery());

            if (!empresas.IsSuccessful)
            {
                return BadRequest(empresas);
            }

            return Ok(empresas);
        }

        /// <summary>
        /// Crea una empresa.
        /// </summary>
        /// <returns>El identificador de la empresa creada.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpPost("empresa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<CreateEmpresaResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> Insert([FromBody] CreateEmpresaRequest request)
        {
            var empresaId = await _mediator.Send(new CreateEmpresaCommand(request));

            if (!empresaId.IsSuccessful)
            {
                return BadRequest(empresaId);
            }

            return Ok(empresaId);
        }

        /// <summary>
        /// Actualiza una empresa.
        /// </summary>
        /// <returns>La empresa actualizada.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpPut("empresa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<UpdateEmpresaResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> Update([FromBody] UpdateEmpresaRequest request)
        {
            var empresa = await _mediator.Send(new UpdateEmpresaCommand(request));

            if (!empresa.IsSuccessful)
            {
                return BadRequest(empresa);
            }

            return Ok(empresa);
        }

        /// <summary>
        /// Elimina una empresa.
        /// </summary>
        /// <returns>Si se ha eliminado correctamente.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="404">No se ha encontrado la empresa con ese identificador.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpDelete("empresa/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<DeleteEmpresaResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> Delete(int empresaId)
        {
            var response = await _mediator.Send(new DeleteEmpresaCommand(empresaId));

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
