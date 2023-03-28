using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tecnocim.Alia.Application.Authentication;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    private readonly ILogger<UsuarioService> _logger;
    private readonly Models.Authentication _authenticationSettings;

    public UsuarioService(
        IUnitOfWork unitOfWork,
        IJwtUtils jwtUtils,
        IMapper mapper,
        ILogger<UsuarioService> logger,
        IOptions<Models.Authentication> authenticationSettings)
    {
        _unitOfWork = unitOfWork;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _logger = logger;
        _authenticationSettings = authenticationSettings.Value ?? throw new ArgumentNullException(nameof(authenticationSettings));
    }

    public async Task<AuthenticateResponse> Authenticate(LoginRequest request, string ipAddress)
    {
        UsuarioDto usuarioDto = null;
        var jwtToken = string.Empty;
        RefreshTokenDto refreshToken = null;
        try
        {
            var usuarios = await _unitOfWork.UsuarioRepository.GetAsync(x => x.Email == request.Email, null, null, null, x => x.Rol);
            if (usuarios == null)
            {
                throw new ApplicationException("Nombre de usuario o contraseña incorrecta");
            }

            var usuario = usuarios.FirstOrDefault();

            if (usuario is null || !usuarios.Any() || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
            {
                throw new ApplicationException("Nombre de usuario o contraseña incorrecta");
            }

            usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);

            jwtToken = _jwtUtils.GenerateJwtToken(usuarioDto);
            refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);

            usuarioDto.RefreshTokens.Add(refreshToken);

            // remove old refresh tokens from user
            RemoveOldRefreshTokens(usuarioDto);

            var usuarioModificado = _mapper.Map<UsuarioDto, Usuario>(usuarioDto);
            _unitOfWork.UsuarioRepository.Update(usuarioModificado);
            _unitOfWork.Commit();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Authenticate");
            return new AuthenticateResponse();
        }

        return new AuthenticateResponse(usuarioDto, usuarioDto.NombreRol, jwtToken, refreshToken.Token);
    }

    public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
    {
        var usuarioDto = await GetUserByRefreshToken(token);
        var refreshToken = usuarioDto.RefreshTokens.Single(x => x.Token == token);

        if (refreshToken.IsRevoked)
        {
            // revoke all descendant tokens in case this token has been compromised
            RevokeDescendantRefreshTokens(refreshToken, usuarioDto, ipAddress, $"Intentando usar un token ya revocado: {token}");

            var usuarioModificado = _mapper.Map<UsuarioDto, Usuario>(usuarioDto);
            _unitOfWork.UsuarioRepository.Update(usuarioModificado);
            _unitOfWork.Commit();
        }

        if (!refreshToken.IsActive)
        {
            throw new ApplicationException("Token no válido");
        }

        // replace old refresh token with a new one (rotate token)
        var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);
        usuarioDto.RefreshTokens.Add(newRefreshToken);

        // remove old refresh tokens from user
        RemoveOldRefreshTokens(usuarioDto);

        var usuarioToken = _mapper.Map<UsuarioDto, Usuario>(usuarioDto);
        _unitOfWork.UsuarioRepository.Update(usuarioToken);
        _unitOfWork.Commit();

        // generate new jwt
        var jwtToken = _jwtUtils.GenerateJwtToken(usuarioDto);

        return new AuthenticateResponse(usuarioDto, usuarioDto.NombreRol, jwtToken, newRefreshToken.Token);
    }

    public async Task<GenericResult<bool>> LogoutAsync(int userId)
    {
        var result = new GenericResult<bool>();

        try
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetFirstOrDefault(x => x, x => x.UsuarioId == userId, null, x => x.Include(y => y.RefreshTokens), false);

            if (usuario == null)
            {
                return result.Failed(404, $"No se ha encontrado el usuario con el user id: {userId}");
            }

            var refreshToken = usuario.RefreshTokens?.OrderByDescending(x => x.Created)?.FirstOrDefault();

            if (refreshToken == null)
            {
                return result.Ok(true);
            }

            usuario.RefreshTokens.Remove(refreshToken);

            _unitOfWork.UsuarioRepository.Update(usuario);
            _unitOfWork.Commit();

            return result.Ok(true);
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al hacer logout", exception);
        }

        return result.Failed(500, $"No se ha podido hacer el logout del usuario con id {userId}");
    }

    public async Task RevokeToken(string token, string ipAddress)
    {
        var usuarioDto = await GetUserByRefreshToken(token);
        var refreshToken = usuarioDto.RefreshTokens.Single(x => x.Token == token);

        if (!refreshToken.IsActive)
        {
            throw new ApplicationException("Token no válido");
        }

        // revoke token and save
        RevokeRefreshToken(refreshToken, ipAddress, "Revocado sin sustitución");
        var usuarioToken = _mapper.Map<UsuarioDto, Usuario>(usuarioDto);
        _unitOfWork.UsuarioRepository.Update(usuarioToken);
        _unitOfWork.Commit();
    }

    private async Task<UsuarioDto> GetUserByRefreshToken(string token)
    {
        var usuario = await _unitOfWork.UsuarioRepository.GetFirstOrDefault(u => u, u => u.RefreshTokens.Any(t => t.Token == token), null,
            x => x.Include(y => y.RefreshTokens).Include(y => y.Rol));

        if (usuario is null)
        {
            throw new ApplicationException("Token no válido");
        }

        return _mapper.Map<Usuario, UsuarioDto>(usuario);
    }

    private RefreshTokenDto RotateRefreshToken(RefreshTokenDto refreshToken, string ipAddress)
    {
        var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
        RevokeRefreshToken(refreshToken, ipAddress, "Reemplazado por un nuevo token", newRefreshToken.Token);
        return newRefreshToken;
    }

    private void RemoveOldRefreshTokens(UsuarioDto usuario)
    {
        // remove old inactive refresh tokens from user based on TTL in app settings
        usuario.RefreshTokens.ToList().RemoveAll(x =>
            !x.IsActive &&
            x.Created.AddDays(_authenticationSettings.RefreshTokenTTL) <= DateTime.UtcNow);
    }

    private void RevokeDescendantRefreshTokens(RefreshTokenDto refreshToken, UsuarioDto usuario, string ipAddress, string motivo)
    {
        // recursively traverse the refresh token chain and ensure all descendants are revoked
        if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
        {
            var childToken = usuario.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
            if (childToken != null && childToken.IsActive)
            {
                RevokeRefreshToken(childToken, ipAddress, motivo);
            }
            else
            {
                RevokeDescendantRefreshTokens(childToken, usuario, ipAddress, motivo);
            }
        }
    }

    private void RevokeRefreshToken(RefreshTokenDto token, string ipAddress, string motivo = null, string replacedByToken = null)
    {
        token.Revoked = DateTime.UtcNow;
        token.RevokedByIp = ipAddress;
        token.ReasonRevoked = motivo;
        token.ReplacedByToken = replacedByToken;
    }
}
