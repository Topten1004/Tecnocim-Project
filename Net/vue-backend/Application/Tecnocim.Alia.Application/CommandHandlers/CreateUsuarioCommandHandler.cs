using System.Globalization;
using AutoMapper;
using LinqKit;
using MediatR;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.CommandHandlers
{
    public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, GenericResult<CreateUsuarioResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUsuarioCommandHandler> _logger;

        public CreateUsuarioCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateUsuarioCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResult<CreateUsuarioResponse>> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new GenericResult<CreateUsuarioResponse>();
            try
            {
                var roles = await _unitOfWork.RolRepository.GetAsync();
                var rolAdminId = roles.FirstOrDefault(x => x.Nombre.ToLower(CultureInfo.InvariantCulture) == "admin")?.RolId;
                IEnumerable<Empresa> empresas = new List<Empresa>(); ;

                if (request.usuario.RolId == rolAdminId)
                {
                    empresas = await _unitOfWork.EmpresaRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue, null, null, false);
                }

                request.usuario.Password = BCrypt.Net.BCrypt.HashPassword(request.usuario.Password);

                Usuario? usuario = null;
                await Task.Run(() =>
                {
                    usuario = _mapper.Map<CreateUsuarioRequest, Usuario>(request.usuario);

                    if (empresas.Any())
                    {
                        empresas.ForEach(usuario.Empresas.Add);
                    }

                    _unitOfWork.UsuarioRepository.Insert(usuario);
                    _unitOfWork.Commit();
                }, cancellationToken);

                return result.Ok(new CreateUsuarioResponse { UsuarioId = usuario != null ? usuario.UsuarioId : 0});
            }
            catch (Exception exception)
            {
                _logger.LogError("Error al insertar el usuario", exception);
                return result.Failed(500, $"Error al insertar el usuario: {Environment.NewLine}{exception.Message}");
            }
        }
    }
}
