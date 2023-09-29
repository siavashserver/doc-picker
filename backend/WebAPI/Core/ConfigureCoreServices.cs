using FluentValidation;
using MediatR;
using WebAPI.Core.Common.Behaviors;

namespace WebAPI.Core;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration config)
    {
        // Setup MediatR
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(typeof(ConfigureCoreServices).Assembly));

        // Setup Validation Behavior
        services.AddValidatorsFromAssembly(typeof(ConfigureCoreServices).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}