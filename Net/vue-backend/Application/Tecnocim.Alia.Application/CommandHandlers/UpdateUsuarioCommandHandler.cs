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
    public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, GenericResult<UpdateUsuarioResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUsuarioCommandHandler> _logger;

        public UpdateUsuarioCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdateUsuarioCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResult<UpdateUsuarioResponse>> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new GenericResult<UpdateUsuarioResponse>();
            try
            {
                var existingUser = await _unitOfWork.UsuarioRepository.GetFirstOrDefault(x => x, x => x.UsuarioId == request.usuario.UsuarioId && !x.Deleted.HasValue,
                    null, x => x.Include(y => y.Rol), false);
                if (existingUser is null)
                {
                    return result.Failed(404, "No se ha encontrado el usuario a actualizar");
                }

                var newPassword = BCrypt.Net.BCrypt.HashPassword(request.usuario.Password);
                if (newPassword != existingUser.Password)
                {
                    existingUser.Password = newPassword;
                }

                if(existingUser.RolId != request.usuario.RolId)
                {
                    var newRol = await _unitOfWork.RolRepository.GetFirstAsync(x => x.RolId == request.usuario.RolId);
                    existingUser.Rol = newRol;
                }

                existingUser.Updated = DateTime.UtcNow;
                existingUser.Email = request.usuario.Email;
                existingUser.Apellidos = request.usuario.Apellidos;
                existingUser.Nombre = request.usuario.Nombre;
                existingUser.PuestoTrabajo = request.usuario.PuestoTrabajo;

                await Task.Run(() =>
                {
                    _unitOfWork.UsuarioRepository.Update(existingUser);
                    _unitOfWork.Commit();
                }, cancellationToken);

                var usuarioResponse = _mapper.Map<Usuario, UpdateUsuarioResponse>(existingUser);

                return result.Ok(usuarioResponse);
            }
            catch (Exception exception)
            {
                _logger.LogError("Error al actualizar el usuario", exception);
                return result.Failed(500, $"Error al actualizar el usuario: {Environment.NewLine}{exception.Message}");
            }
        }
    }
}
