using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tecnocim.Alia.Application.Authentication;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;
    private readonly Models.Authentication _authenticationSettings;

    public JwtMiddleware(
        RequestDelegate next,
        IOptions<Models.Authentication> authenticationSettings,
        IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
        _authenticationSettings = authenticationSettings.Value ?? throw new ArgumentNullException(nameof(authenticationSettings));
    }

    public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var usuarioId = jwtUtils.ValidateJwtToken(token);
        if (usuarioId != null)
        {
            // attach user to context on successful jwt validation


            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            context.Items["User"] = await unitOfWork.UsuarioRepository.GetFirstAsync(x => x.UsuarioId == usuarioId.Value);
        }

        await _next(context);
    }
}
