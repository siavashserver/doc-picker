using WebAPI.WebAPI.DTOs;

namespace WebAPI.WebAPI;

public static class ConfigureWebAPIServices
{
    public static IServiceCollection AddWebAPIServices(this IServiceCollection services, IConfiguration config)
    {
        // Setup AutoMapper Profiles
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

        return services;
    }
}