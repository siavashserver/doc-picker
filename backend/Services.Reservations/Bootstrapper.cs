using MassTransit;
using Microsoft.EntityFrameworkCore;
using Services.Reservations.Core.DataAccess;
using Services.Reservations.Core.EventHandlers;
using Services.Reservations.Core.Secrets;
using Services.Reservations.Core.Settings;
using Services.Reservations.WebAPI;
using Services.Shared;

namespace Services.Reservations;

public class Bootstrapper : GrpcServiceBootstrapper
{
    protected override void ConfigureSettings(WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Configuration.AddJsonFile("appsecrets.json", true, true);

        webApplicationBuilder.Services.Configure<ApplicationSecrets>(
            webApplicationBuilder.Configuration.GetSection("ApplicationSecrets"));

        webApplicationBuilder.Services.Configure<ApplicationSettings>(
            webApplicationBuilder.Configuration.GetSection("ApplicationSettings"));
    }

    protected override void ConfigureDatabase(WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddDbContext<DataContext>(options =>
        {
            options
                .UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("PostgreSQL"));
        });
    }

    protected override void ConfigureServices(WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddMassTransit(busRegistrationConfigurator =>
        {
            busRegistrationConfigurator.AddConsumer<AccountDeletedEventHandler>();
            busRegistrationConfigurator.AddConsumer<DoctorDeletedEventHandler>();

            busRegistrationConfigurator.UsingRabbitMq((busRegistrationContext, rabbitMqBusFactoryConfigurator) =>
            {
                rabbitMqBusFactoryConfigurator.ConfigureEndpoints(busRegistrationContext);

                var connectionString = webApplicationBuilder.Configuration.GetConnectionString("RabbitMQ");
                rabbitMqBusFactoryConfigurator.Host(new Uri(connectionString), "/", rabbitMqHostConfigurator => { });
            });
        });
    }

    protected override async Task ConfigureDatabaseMigrations(WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        await dataContext.Database.MigrateAsync();
    }

    protected override void ConfigurePipeline(WebApplication webApplication)
    {
        webApplication.MapGrpcService<ReservationsController>();
    }
}