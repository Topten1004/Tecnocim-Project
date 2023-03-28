using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.CommandHandlers
{
    public class DeleteEmpresaCommandHandler : IRequestHandler<DeleteEmpresaCommand, GenericResult<DeleteEmpresaResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteEmpresaCommandHandler> _logger;

        public DeleteEmpresaCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<DeleteEmpresaCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResult<DeleteEmpresaResponse>> Handle(DeleteEmpresaCommand request, CancellationToken cancellationToken)
        {
            var result = new GenericResult<DeleteEmpresaResponse>() ;
            try
            {
                var existingEmpresa = await _unitOfWork.EmpresaRepository.GetFirstOrDefault(x => x, x => x.EmpresaId == request.empresaId && !x.Deleted.HasValue,
                      null, x => x.Include(y => y.Usuarios), false);
                if (existingEmpresa is null)
                {
                    return result.Failed(404, "No se ha encontrado la empresa a eliminar");
                }

                existingEmpresa.Deleted = DateTime.UtcNow;

                await Task.Run(() =>
                {
                    _unitOfWork.EmpresaRepository.Update(existingEmpresa);
                    _unitOfWork.Commit();
                }, cancellationToken);

                var deleteResponse = new DeleteEmpresaResponse { IsSuccess = true };

                return result.Ok(deleteResponse);
            }
            catch (Exception exception)
            {
                _logger.LogError("Error al eliminar la empresa", exception);
                return result.Failed(500, $"Error al eliminar la empresa: {Environment.NewLine}{exception.Message}");
            }
        }
    }
}
