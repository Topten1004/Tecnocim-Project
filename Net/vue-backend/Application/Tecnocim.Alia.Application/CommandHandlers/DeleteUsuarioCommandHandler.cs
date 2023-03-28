using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.CommandHandlers
{
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, GenericResult<DeleteUsuarioResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteUsuarioCommandHandler> _logger;

        public DeleteUsuarioCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<DeleteUsuarioCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResult<DeleteUsuarioResponse>> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new GenericResult<DeleteUsuarioResponse>() ;
            try
            {
                var existingUser = await _unitOfWork.UsuarioRepository.GetFirstOrDefault(x => x, x => x.UsuarioId == request.usuarioId && !x.Deleted.HasValue);
                if (existingUser is null)
                {
                    return result.Failed(404, "No se ha encontrado el usuario a eliminar");
                }

                existingUser.Deleted = DateTime.UtcNow;

                await Task.Run(() =>
                {
                    _unitOfWork.UsuarioRepository.Update(existingUser);
                    _unitOfWork.Commit();
                }, cancellationToken);

                var deleteResponse = new DeleteUsuarioResponse { IsSuccess = true };

                return result.Ok(deleteResponse);
            }
            catch (Exception exception)
            {
                _logger.LogError("Error al eliminar el usuario", exception);
                return result.Failed(500, $"Error al eliminar el usuario: {Environment.NewLine}{exception.Message}");
            }
        }
    }
}
