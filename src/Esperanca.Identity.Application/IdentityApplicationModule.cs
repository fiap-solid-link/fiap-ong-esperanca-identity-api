using Esperanca.Identity.Application._Shared.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Esperanca.Identity.Application;

public static class IdentityApplicationModule
{
    public static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        var assembly = typeof(IdentityApplicationModule).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
