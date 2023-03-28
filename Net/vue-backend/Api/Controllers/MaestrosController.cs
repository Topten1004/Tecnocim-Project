using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tecnocim.Alia.Application.Queries;

namespace vue_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public partial class MaestrosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaestrosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Devuelve la lista de todos los roles.
        /// </summary>
        /// <returns>Lista de todos los roles.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());

            if (!roles.IsSuccessful)
            {
                return BadRequest(roles);
            }

            return Ok(roles);
        }
    }
}
