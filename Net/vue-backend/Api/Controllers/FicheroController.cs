using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;

namespace vue_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FicheroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FicheroController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Publica un fichero.
        /// </summary>
        /// <returns>Los datos del fichero publicado.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpPost("publicar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<UploadFicheroResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> Publish([FromForm] UploadFicheroRequest request)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, request.EmpresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var ficheroResponse = await _mediator.Send(new UploadFicheroCommand(request, usuario!));

            if (!ficheroResponse.IsSuccessful)
            {
                return BadRequest(ficheroResponse);
            }

            return Ok(ficheroResponse);
        }

        /// <summary>
        /// Obtiene el fichero con el identificador pasado por parámetro.
        /// </summary>
        /// <param name="id">Identificador del fichero.</param>
        /// <returns>Los datos del fichero.</returns>
        /// <response code="200">Devuelve el fichero.</response>
        /// <response code="404">No existe el fichero con ese identificador.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("fichero/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<UploadFicheroResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = Request.HttpContext.Items["User"];

            var fichero = await _mediator.Send(new GetFicheroByIdQuery(id, usuario));

            if (fichero.IsSuccessful && fichero.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!fichero.IsSuccessful)
            {
                return BadRequest(fichero);
            }

            return Ok(fichero);
        }

        /// <summary>
        /// Ejecuta un fichero.
        /// </summary>
        /// <returns>El fichero ejecutado.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpPut("ejecutar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<UploadFicheroResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> Update([FromBody] UpdateFicheroRequest request)
        {
            var usuario = Request.HttpContext.Items["User"];

            var response = await _mediator.Send(new UpdateFicheroCommand(request, usuario));

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

        private async Task<bool> ValidateUsuarioEmpresa(object? usuario, int empresaId)
        {
            var isAuthorized = false;
            if (usuario != null)
            {
                isAuthorized = await _mediator.Send(new GetUsuarioEmpresaAsignadaQuery(usuario, empresaId));
            }

            return isAuthorized;
        }
    }
}
