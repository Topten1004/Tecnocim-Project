using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Tecnocim.Alia.Application.Extensions;
using Tecnocim.Alia.Application.Middlewares;
using Tecnocim.Alia.Application.Models;
using Tecnocim.Alia.Application.Profiles;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Validators;
using Tecnocim.Alia.DataInfrastructure.Extensions;
using vue_backend.Filters;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

logger.Info("Started");

var builder = WebApplication.CreateBuilder(args);

LoadSettings(builder);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIntermediaDatabase(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly()); //, typeof(UsuarioProfile).Assembly);
builder.Services.AddMediatR(typeof(GetEmpresasByUsuarioIdQuery).Assembly);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddCors();
builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.Converters.Add(new Tecnocim.Alia.Application.Converters.DateOnlyConverter());
    });

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<EmpresaValidator>();

var authenticationConfig = new Authentication();
builder.Configuration.GetSection("Authentication").Bind(authenticationConfig);
var key = Encoding.ASCII.GetBytes(authenticationConfig.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
       .AddJwtBearer(x =>
       {
           x.RequireHttpsMetadata = false;
           x.SaveToken = true;
           x.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(key),
               ValidateIssuer = false,
               ValidateAudience = false
           };

       });

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ALIA API", Version = "v1", Description = "ALIA API" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {user token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "ALIA API V1");
        c.RoutePrefix = "help";
        c.DocumentTitle = "ALIA API";
    });
//}

app.UseHttpsRedirection();

app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();

void LoadSettings(WebApplicationBuilder builder)
{
    builder.Services.Configure<Authentication>(builder.Configuration.GetSection("Authentication"));
    builder.Services.Configure<Fichero>(builder.Configuration.GetSection("Fichero"));
}