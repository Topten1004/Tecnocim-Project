using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace vue_backend.Filters;

public class ReApplyOptionalRouteParameterOperationFilter : IOperationFilter
{
    const string captureName = "routeParameter";

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var httpMethodAttributes = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute>();

        var httpMethodWithOptional = httpMethodAttributes?.FirstOrDefault(m => m.Template?.Contains("?") ?? false);
        if (httpMethodWithOptional == null)
            return;

        string regex = $"{{(?<{captureName}>\\w+)\\?}}";

        var matches = System.Text.RegularExpressions.Regex.Matches(httpMethodWithOptional.Template, regex);

        foreach (System.Text.RegularExpressions.Match match in matches)
        {
            var name = match.Groups[captureName].Value;

            var parameter = operation.Parameters.FirstOrDefault(p => p.In == ParameterLocation.Path && p.Name == name);
            if (parameter != null)
            {
                parameter.AllowEmptyValue = true;
                parameter.Description = "Seleccione \"Enviar valores vacíos\" o Swagger pasará una coma para valores vacíos";
                parameter.Required = false;
                //parameter.Schema.Default = new OpenApiString(string.Empty);
                parameter.Schema.Nullable = true;
            }
        }
    }
}
