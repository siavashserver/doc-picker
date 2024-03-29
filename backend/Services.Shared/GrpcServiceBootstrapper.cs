using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Shared.Core.Interfaces;
using Services.Shared.WebAPI;

namespace Services.Shared;

public abstract class GrpcServiceBootstrapper
{
    public async Task Bootstrap(string[] args)
    {
        var webApplicationBuilder = WebApplication.CreateBuilder(args);
        ConfigureWebApplicationBuilder(webApplicationBuilder);

        var webApplication = webApplicationBuilder.Build();
        await ConfigureDatabaseMigrations(webApplication);
        ConfigureWebApplication(webApplication);

        await webApplication.RunAsync();
    }

    private void ConfigureWebApplicationBuilder(WebApplicationBuilder webApplicationBuilder)
    {
        ConfigureSettings(webApplicationBuilder);

        webApplicationBuilder.Configuration.AddEnvironmentVariables();

        ConfigureDatabase(webApplicationBuilder);
        ConfigureServices(webApplicationBuilder);

        var requestHandlers = Assembly
            .GetEntryAssembly()
            .GetTypes()
            .Where(t => t.IsClass &&
                        t.GetInterfaces().Any(i => i.Name == typeof(IRequestHandler<,>).Name));
        foreach (var requestHandler in requestHandlers) webApplicationBuilder.Services.AddTransient(requestHandler);

        webApplicationBuilder.Services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());

        webApplicationBuilder.Services.AddGrpc(options =>
        {
            options.Interceptors.Add<ExceptionInterceptor>();
            options.Interceptors.Add<RequestValidationInterceptor>();
        });

        if (webApplicationBuilder.Environment.IsDevelopment()) webApplicationBuilder.Services.AddGrpcReflection();
    }

    private void ConfigureWebApplication(WebApplication webApplication)
    {
        ConfigurePipeline(webApplication);

        if (webApplication.Environment.IsDevelopment()) webApplication.MapGrpcReflectionService();
    }

    protected abstract void ConfigureSettings(WebApplicationBuilder webApplicationBuilder);
    protected abstract void ConfigureDatabase(WebApplicationBuilder webApplicationBuilder);
    protected abstract void ConfigureServices(WebApplicationBuilder webApplicationBuilder);
    protected abstract Task ConfigureDatabaseMigrations(WebApplication webApplication);
    protected abstract void ConfigurePipeline(WebApplication webApplication);
}