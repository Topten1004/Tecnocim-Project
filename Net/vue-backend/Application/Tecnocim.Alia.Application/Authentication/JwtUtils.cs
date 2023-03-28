using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.Authentication;

public class JwtUtils : IJwtUtils
{
    private readonly Tecnocim.Alia.Application.Models.Authentication _authenticationSettings;
    private readonly IServiceProvider _serviceProvider;

    public JwtUtils(
        IServiceProvider serviceProvider,
        IOptions<Tecnocim.Alia.Application.Models.Authentication> authenticationSettings)
    {
        _serviceProvider = serviceProvider;
        _authenticationSettings = authenticationSettings.Value ?? throw new ArgumentNullException(nameof(authenticationSettings));
    }

    public string GenerateJwtToken(UsuarioDto usuario)
    {
        // token válido durante 15 minutos
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authenticationSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] 
            { 
                new Claim("id", usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role, usuario.NombreRol)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_authenticationSettings.AccessExpiration > 0 ? _authenticationSettings.AccessExpiration : 15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateJwtToken(string token)
    {
        if (token is null)
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authenticationSettings.Secret);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var usuarioId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // return user id from JWT token if validation successful
            return usuarioId;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }

    public RefreshTokenDto GenerateRefreshToken(string ipAddress)
    {
        var refreshToken = new RefreshTokenDto
        {
            Token = getUniqueToken().GetAwaiter().GetResult(),
            // token is valid for 7 days
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };

        return refreshToken;

        async Task<string> getUniqueToken()
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            // token is a cryptographically strong random sequence of values
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            // ensure token is unique by checking against db
            var tokens = await unitOfWork.UsuarioRepository.GetAsync(x => x.RefreshTokens.Any(t => t.Token == token));

            var tokenIsUnique = tokens == null || !tokens.Any();

            if (!tokenIsUnique)
            {
                return getUniqueToken().GetAwaiter().GetResult();
            }

            return token;
        }
    }
}
