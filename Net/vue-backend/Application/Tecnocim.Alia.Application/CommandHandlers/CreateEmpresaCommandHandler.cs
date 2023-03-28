using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.CommandHandlers
{
    public class CreateEmpresaCommandHandler : IRequestHandler<CreateEmpresaCommand, GenericResult<CreateEmpresaResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEmpresaCommandHandler> _logger;

        public CreateEmpresaCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateEmpresaCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResult<CreateEmpresaResponse>> Handle(CreateEmpresaCommand request, CancellationToken cancellationToken)
        {
            var result = new GenericResult<CreateEmpresaResponse>();
            try
            {
                var existingUser = await _unitOfWork.UsuarioRepository.GetFirstOrDefault(x => x, x => x.UsuarioId == request.empresa.UsuarioId && !x.Deleted.HasValue,
                     null, null, false);
                if (existingUser is null)
                {
                    return result.Failed(404, "No se ha encontrado el usuario al que pertenece la empresa.");
                }

                Empresa? empresa = null;
                await Task.Run(() =>
                {
                    empresa = _mapper.Map<CreateEmpresaRequest, Empresa>(request.empresa);
                    empresa.Usuarios.Add(existingUser);

                    _unitOfWork.EmpresaRepository.Insert(empresa);
                    _unitOfWork.Commit();
                }, cancellationToken);

                return result.Ok(new CreateEmpresaResponse { EmpresaId = empresa != null ? empresa.EmpresaId : 0});
            }
            catch (Exception exception)
            {
                _logger.LogError("Error al insertar la empresa", exception);
                return result.Failed(500, $"Error al insertar la empresa: {Environment.NewLine}{exception.Message}");
            }
        }
    }
}
