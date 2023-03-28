using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;

namespace vue_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public partial class NegocioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NegocioController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Listado del pool bancario de la empresa y sección pasadas por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <param name="seccion">Sección, uno de los siguientes valores: largoplazo, creditos, ventas, compras.</param>
        /// <returns>Listado del pool bancario.</returns>
        /// <response code="200">Devuelve el listado del pool bancario.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado el pool bancario con esos parámetros de empresa y sección.</response>
        [HttpGet("poolbancario/{empresaId}/{seccion}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<PoolBancarioResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetPoolBancarioByEmpresaIdYSeccion(int empresaId, Seccion seccion)
        {
            var poolBancario = await _mediator.Send(new GetPoolBancarioByEmpresaIdAndSeccionQuery(empresaId, seccion.ToString()));

            if (!poolBancario.IsSuccessful)
            {
                return BadRequest(poolBancario);
            }

            if (poolBancario.IsSuccessful && poolBancario.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            return Ok(poolBancario);
        }

        /// <summary>
        /// Listado de pool bancario pendientes.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Listado del pool bancario pendiente.</returns>
        /// <response code="200">Devuelve el listado del pool bancario pendiente.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado el pool bancario con ese identificador de empresa.</response>
        [HttpGet("poolbancario/pendientes/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<PoolBancarioResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetPoolBancarioPendientesByEmpresaId(int empresaId)
        {
            var poolPendientes = await _mediator.Send(new GetPoolBancarioPendienteByEmpresaIdQuery(empresaId));

            if (!poolPendientes.IsSuccessful)
            {
                return BadRequest(poolPendientes);
            }

            if (poolPendientes.IsSuccessful && poolPendientes.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            return Ok(poolPendientes);
        }

        /// <summary>
        /// Listado de pool bancario pendientes tipo 52.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Listado del pool bancario pendiente tipo 52.</returns>
        /// <response code="200">Devuelve el listado del pool bancario pendiente tipo 52.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado el pool bancario con ese identificador de empresa.</response>
        [HttpGet("poolbancario/pendientestipo52/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<PoolBancarioResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetPoolBancarioPendientesTipo52ByEmpresaId(int empresaId)
        {
            var poolPendientes = await _mediator.Send(new GetPoolBancarioPendienteTipo52ByEmpresaIdQuery(empresaId));

            if (!poolPendientes.IsSuccessful)
            {
                return BadRequest(poolPendientes);
            }

            if (poolPendientes.IsSuccessful && poolPendientes.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            return Ok(poolPendientes);
        }

        /// <summary>
        /// Listado de pool bancario tipo 52.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Listado del pool bancario tipo 52.</returns>
        /// <response code="200">Devuelve el listado del pool bancario tipo 52.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado el pool bancario con ese identificador de empresa.</response>
        [HttpGet("poolbancario/tipo52/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<PoolBancarioResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetPoolBancarioTipo52ByEmpresaId(int empresaId)
        {
            var poolPendientes = await _mediator.Send(new GetPoolBancarioTipo52ByEmpresaIdQuery(empresaId));

            if (!poolPendientes.IsSuccessful)
            {
                return BadRequest(poolPendientes);
            }

            if (poolPendientes.IsSuccessful && poolPendientes.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            return Ok(poolPendientes);
        }

        /// <summary>
        /// Listado de pool bancario de estados.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Listado del pool bancario de estados.</returns>
        /// <response code="200">Devuelve el listado del pool bancario de estados.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado el pool bancario con ese identificador de empresa.</response>
        [HttpGet("poolbancario/estados/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<PoolBancarioEstadosResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetPoolBancarioEstadosByEmpresaId(int empresaId)
        {
            var poolPendientes = await _mediator.Send(new GetPoolBancarioEstadosByEmpresaIdQuery(empresaId));

            if (!poolPendientes.IsSuccessful)
            {
                return BadRequest(poolPendientes);
            }

            if (poolPendientes.IsSuccessful && poolPendientes.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            return Ok(poolPendientes);
        }

        /// <summary>
        /// Obtiene el contrato con el identificador pasado por parámetro.
        /// </summary>
        /// <param name="contratoId">Identificador del contrato.</param>
        /// <returns>Datos del contrato.</returns>
        /// <response code="200">Devuelve los datos del contrato.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado un contrato con el identificador pasado por parámetro.</response>
        [HttpGet("contrato/{contratoId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<ContratoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetContrato(int contratoId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var response = await _mediator.Send(new GetContratoByIdQuery(contratoId, usuario));

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
        /// Dar de alta un contrato.
        /// </summary>
        /// <returns>El identificador del contrato creado.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado.</response>
        [HttpPost("contrato")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<CreateContratoResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> InsertContrato([FromBody] CreateContratoRequest request)
        {
            var contratoId = await _mediator.Send(new CreateContratoCommand(request));

            if (!contratoId.IsSuccessful && contratoId.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound(contratoId);
            }

            if (!contratoId.IsSuccessful)
            {
                return BadRequest(contratoId);
            }

            return Ok(contratoId);
        }

        /// <summary>
        /// Modifica un contrato.
        /// </summary>
        /// <returns>El identificador del contrato creado.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado el contrado con el identificador pasado.</response>
        [HttpPut("contrato")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<UpdateContratoResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> UpdateContrato([FromBody] UpdateContratoRequest request)
        {
            var contrato = await _mediator.Send(new UpdateContratoCommand(request));

            if (!contrato.IsSuccessful && contrato.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound(contrato);
            }

            if (!contrato.IsSuccessful)
            {
                return BadRequest(contrato);
            }

            return Ok(contrato);
        }

        /// <summary>
        /// Listado de documentos de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <param name="origen">Origen, uno de los siguientes valores: BSS, Modelo200, Cirbe.</param>
        /// <param name="status">El estado del documento: 0 ó 1.</param>
        /// <returns>Listado del pool bancario.</returns>
        /// <response code="200">Devuelve el listado de documentos.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado documentos con esos parámetros.</response>
        [HttpGet("documento/listado/{empresaId}/{origen}/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<DocumentoResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetListadoDocumentos(int empresaId, string origen = null, bool? status = null)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var documentos = await _mediator.Send(new GetDocumentosByEmpresaIdAndOrigenAndStatusQuery(empresaId, origen, status));

            if (!documentos.IsSuccessful)
            {
                return BadRequest(documentos);
            }

            if (documentos.IsSuccessful && documentos.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            return Ok(documentos);
        }

        /// <summary>
        /// Listado de documentos vigentes de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Listado de documentos vigentes.</returns>
        /// <response code="200">Devuelve el listado de documentos vigentes.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("documento/vigentes/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<DocumentoErroresResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> GetListadoDocumentosVigentes(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var documentos = await _mediator.Send(new GetDocumentosVigentesByEmpresaIdQuery(empresaId));

            if (!documentos.IsSuccessful)
            {
                return BadRequest(documentos);
            }

            return Ok(documentos);
        }

        /// <summary>
        /// Listado de todos documentos de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Listado de documentos.</returns>
        /// <response code="200">Devuelve el listado de documentos.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("documento/todos/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<DocumentoErroresResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> GetListadoDocumentos(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var documentos = await _mediator.Send(new GetDocumentosByEmpresaIdQuery(empresaId));

            if (!documentos.IsSuccessful)
            {
                return BadRequest(documentos);
            }

            return Ok(documentos);
        }

        /// <summary>
        /// Documento cuyo identificador es pasado por parámetro.
        /// </summary>
        /// <param name="documentoId">Identificador del documento.</param>
        /// <returns>Documento cuyo identificador es pasado por parámetro.</returns>
        /// <response code="200">Devuelve el documento.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        /// <response code="404">No se ha encontrado el documento con ese identificador.</response>
        [HttpGet("documento/{documentoId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<DocumentoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        public async Task<IActionResult> GetDocumento(long documentoId)
        {
            var usuario = Request.HttpContext.Items["User"];

            var documento = await _mediator.Send(new GetDocumentoByIdQuery(documentoId, usuario));

            if (!documento.IsSuccessful)
            {
                return BadRequest(documento);
            }

            if (documento.IsSuccessful && documento.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            return Ok(documento);
        }

        /// <summary>
        /// Elimina un documento.
        /// </summary>
        /// <returns>Si se ha eliminado correctamente.</returns>
        /// <response code="200">Operación correcta.</response>
        /// <response code="404">No se ha encontrado el documento con ese identificador.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpDelete("documento/{documentoId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<DeleteDocumentoResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> Delete(int documentoId)
        {
            var usuario = Request.HttpContext.Items["User"];

            var response = await _mediator.Send(new DeleteDocumentoCommand(documentoId, usuario));

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
        /// Listado del total por entidades de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Listado del total por entidades de la empresa pasada por parámetro.</returns>
        /// <response code="200">Devuelve el listado del total por entidades.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("cirbe/totalentidades/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<TotalEntidadesResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> GetCirbeTotalPorEntidades(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var totales = await _mediator.Send(new GetTotalPorEntidadesByEmpresaIdQuery(empresaId));

            if (totales.IsSuccessful && totales.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!totales.IsSuccessful)
            {
                return BadRequest(totales);
            }

            return Ok(totales);
        }

        /// <summary>
        /// Listado de inversión por entidades de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Listado de inversión por entidades de la empresa pasada por parámetro.</returns>
        /// <response code="200">Devuelve el listado de inversión por entidades.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("cirbe/inversionentidades/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<InversionEntidadesResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> GetInversionPorEntidades(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var resultado = await _mediator.Send(new GetInversionPorEntidadesByEmpresaIdQuery(empresaId));

            if (resultado.IsSuccessful && resultado.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!resultado.IsSuccessful)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Listado del circulante por entidades de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Listado del circulante por entidades de la empresa pasada por parámetro.</returns>
        /// <response code="200">Devuelve el listado del circulante por entidades.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("cirbe/circulanteporentidades/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<TotalEntidadesResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> GetCirbeCirculantePorEntidades(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var totales = await _mediator.Send(new GetCirculantePorEntidadesByEmpresaIdQuery(empresaId));

            if (totales.IsSuccessful && totales.ErrorCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }

            if (!totales.IsSuccessful)
            {
                return BadRequest(totales);
            }

            return Ok(totales);
        }

        /// <summary>
        /// Listado del análisis servicio deuda de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Listado del análisis servicio deuda de la empresa pasada por parámetro.</returns>
        /// <response code="200">Devuelve el listado del análisis del servicio deuda.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("cirbe/analisisserviciodeuda/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<AnalisisServicioDeudaResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> GetCirbeAnalisisServicioDeuda(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var response = await _mediator.Send(new GetAnalisisServicioDeudaByEmpresaIdQuery(empresaId));

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
        /// Contabilidad analítica de pérdidas y ganancias de la empresa pasada por parámetro.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa.</param>
        /// <returns>Contabilidad analítica de pérdidas y ganancias de la empresa pasada por parámetro.</returns>
        /// <response code="200">Devuelve las pérdidas y ganancias.</response>
        /// <response code="400">Se ha producido un error en la petición.</response>
        /// <response code="401">El usuario debe estar autenticado y autorizado.</response>
        [HttpGet("contabilidadanalitica/perdidasganancias/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<ContabilidadAnaliticaPerdidasGananciasResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Unit))]
        public async Task<IActionResult> GetContabilidadAnaliticaPerdidasGanancias(int empresaId)
        {
            var usuario = Request.HttpContext.Items["User"];
            var isAuthorized = await ValidateUsuarioEmpresa(usuario, empresaId);

            if (!isAuthorized)
            {
                return Unauthorized("El usuario no tiene asignada la empresa enviada");
            }

            var response = await _mediator.Send(new GetContabilidadAnaliticaPerdidasGananciasByEmpresaIdQuery(empresaId));

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
