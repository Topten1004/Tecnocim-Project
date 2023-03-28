using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Application.Services;
using Tecnocim.Alia.Domain;

namespace vue_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IMediator _mediator;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(
            ILogger<UsuarioController> logger,
            IMediator mediator,
            IUsuarioService usuarioService)
        {
            _logger = logger;
            _mediator = mediator;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Autenticación del usuario.
        /// </summary>
        /// <param name="request">Email y contraseña.</param>
        /// <returns>Datos del usuario y token de authenticación.</returns>
        /// <response code="200">Devuelve los datos del usuario y el token.</response>
        /// <response code="400">Se ha producido un error.</response>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Authenticate(LoginRequest request)
        {
            var response = _usuarioService.Authenticate(request, GetIpAddress()).GetAwaiter().GetResult();

            if (response.Id == 0)
            {
                return BadRequest();
            }

            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        /// <summary>
        /// Salir de la sesión.
        /// </summary>
        /// <returns>True si la operación ha sido correcta.</returns>
        /// <response code="200">Operación de salir de sesión correcta.</response>
        /// <response code="404">Se ha producido un error. Usuario no encontrado.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

            if (userId == null || !int.TryParse(userId, out var userIdValue))
            {
                return Ok();
            }

            var logoutResult = await _usuarioService.LogoutAsync(userIdValue);

            if (!logoutResult.IsSuccessful)
            {
                return BadRequest(logoutResult);
            }

            return Ok(logoutResult);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _usuarioService.RefreshToken(refreshToken, GetIpAddress()).GetAwaiter().GetResult();
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(RevokeTokenRequest model)
        {
            // accept refresh token in request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "El token is requerido" });
            }

            _usuarioService.RevokeToken(token, GetIpAddress());
            return Ok(new { message = "Token revocado" });
        }

        /// <summary>
        /// Devuelve la lista de todos los usuarios con sus empresas.
        /// </summary>
        /// <returns>Lista de todos los usuarios con sus empresas.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [Authorize()]
        [HttpGet("usuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _mediator.Send(new GetUsuariosQuery());

            if (!usuarios.IsSuccessful)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }

        /// <summary>
        /// Devuelve el usuario con sus empresas que tiene como identificador el pasado por parámetro.
        /// </summary>
        /// <returns>Usuario con sus empresas.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado el usuario con ese identificador.</response>
        [Authorize()]
        [HttpGet("usuario/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<UsuarioListadoDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> Get(int usuarioId)
        {
            var usuario = await _mediator.Send(new GetUsuarioByIdQuery(usuarioId));

            if (!usuario.IsSuccessful)
            {
                return BadRequest(usuario);
            }

            if (usuario.IsSuccessful && usuario.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Devuelve todos los usuarios de la empresa que tiene como identificador el pasado por parámetro.
        /// </summary>
        /// <returns>Usuarios de la empresa.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("usuarios/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<IEnumerable<UsuarioListadoDto>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> GetAllByEmpresaId(int empresaId)
        {
            var usuarios = await _mediator.Send(new GetUsuariosByEmpresaIdQuery(empresaId));

            return Ok(usuarios);
        }

        /// <summary>
        /// Crea un usuario.
        /// </summary>
        /// <returns>El identificador del usuario creado.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [Authorize(Roles = "Admin")]
        [HttpPost("usuario")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<CreateUsuarioResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> Insert([FromBody] CreateUsuarioRequest request)
        {
            var usuarioId = await _mediator.Send(new CreateUsuarioCommand(request));

            if (!usuarioId.IsSuccessful)
            {
                return BadRequest(usuarioId);
            }

            return Ok(usuarioId);
        }

        /// <summary>
        /// Actualiza un usuario.
        /// </summary>
        /// <returns>El usuario actualizado.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [Authorize(Roles = "Admin")]
        [HttpPut("usuario")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<UpdateUsuarioResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> Update([FromBody] UpdateUsuarioRequest request)
        {
            var usuario = await _mediator.Send(new UpdateUsuarioCommand(request));

            if (!usuario.IsSuccessful)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Elimina un usuario.
        /// </summary>
        /// <returns>Si se ha eliminado correctamente.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="404">No se ha encontrado el usuario con ese identificador.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("usuario/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<DeleteUsuarioResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> Delete(int usuarioId)
        {
            var response = await _mediator.Send(new DeleteUsuarioCommand(usuarioId));

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        //[HttpGet("{id}/refresh-tokens")]
        //public IActionResult GetRefreshTokens(int id)
        //{
        //    var user = _usuarioService.GetById(id);
        //    return Ok(user.RefreshTokens);
        //}

        private void SetTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string? GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }

            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
    }
}
