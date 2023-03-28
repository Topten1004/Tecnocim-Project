using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.CommandHandlers
{
    public class UpdateEmpresaCommandHandler : IRequestHandler<UpdateEmpresaCommand, GenericResult<UpdateEmpresaResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEmpresaCommandHandler> _logger;

        public UpdateEmpresaCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdateEmpresaCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResult<UpdateEmpresaResponse>> Handle(UpdateEmpresaCommand request, CancellationToken cancellationToken)
        {
            var result = new GenericResult<UpdateEmpresaResponse>();
            try
            {
                var existingEmpresa = await _unitOfWork.EmpresaRepository.GetFirstOrDefault(x => x, x => x.EmpresaId == request.empresa.EmpresaId && !x.Deleted.HasValue,
                    null, x => x.Include(y => y.Usuarios), false);
                if (existingEmpresa is null)
                {
                    return result.Failed(404, "No se ha encontrado la empresa a actualizar");
                }

                if(!existingEmpresa.Usuarios.Select(x => x.UsuarioId).Contains(request.empresa.UsuarioId))
                {
                    var usuario = await _unitOfWork.UsuarioRepository.GetFirstAsync(x => x.UsuarioId == request.empresa.UsuarioId);
                    existingEmpresa.Usuarios.Add(usuario);
                }

                existingEmpresa.Updated = DateTime.UtcNow;
                existingEmpresa.Email = request.empresa.Email;
                existingEmpresa.CIF = request.empresa.CIF;
                existingEmpresa.Nombre = request.empresa.Nombre;
                existingEmpresa.Telefono = request.empresa.Telefono;    
                existingEmpresa.Contacto = request.empresa.Contacto;

                await Task.Run(() =>
                {
                    _unitOfWork.EmpresaRepository.Update(existingEmpresa);
                    _unitOfWork.Commit();
                }, cancellationToken);

                var empresaResponse = _mapper.Map<Empresa, UpdateEmpresaResponse>(existingEmpresa);

                return result.Ok(empresaResponse);
            }
            catch (Exception exception)
            {
                _logger.LogError("Error al actualizar la empresa", exception);
                return result.Failed(500, $"Error al actualizar la empresa: {Environment.NewLine}{exception.Message}");
            }
        }
    }
}
