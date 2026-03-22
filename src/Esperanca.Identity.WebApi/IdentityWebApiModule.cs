using Esperanca.Identity.Application;
using Esperanca.Identity.Application._Shared;
using Esperanca.Identity.Infrastructure;
using Esperanca.Identity.WebApi._Shared;
using Microsoft.OpenApi.Models;

namespace Esperanca.Identity.WebApi;

public static class IdentityWebApiModule
{
    public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Modules
        IdentityApplicationModule.ConfigureServices(services);
        IdentityInfrastructureModule.ConfigureServices(services, configuration);

        // Usuario autenticado
        services.AddHttpContextAccessor();
        services.AddScoped<IUsuarioAutenticado, UsuarioAutenticado>();

        // Controllers
        services.AddControllers();

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Esperanca Identity API",
                Version = "v1",
                Description = "API de Identidade e Acesso - Plataforma Conexao Solidaria"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        // Health Checks
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("IdentityDb")!,
                name: "postgresql",
                tags: ["db", "ready"]);

        return services;
    }
}
