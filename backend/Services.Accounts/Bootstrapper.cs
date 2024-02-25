using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Accounts.Core.DataAccess;
using Services.Accounts.Core.DataAccess.Entities;
using Services.Accounts.Core.Interfaces;
using Services.Accounts.Core.Secrets;
using Services.Accounts.Core.Services;
using Services.Accounts.Core.Settings;
using Services.Accounts.WebAPI;
using Services.Shared;

namespace Services.Accounts;

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
        webApplicationBuilder.Services.AddTransient<IPasswordHasher<Account>, PasswordHasher<Account>>();
        webApplicationBuilder.Services.AddTransient<IPasswordService, PasswordService>();
        webApplicationBuilder.Services.AddTransient<ISessionTokenService, SessionTokenService>();

        webApplicationBuilder.Services.AddMassTransit(busRegistrationConfigurator =>
            busRegistrationConfigurator.UsingRabbitMq(
                (busRegistrationContext, rabbitMqBusFactoryConfigurator) =>
                {
                    var connectionString = webApplicationBuilder.Configuration.GetConnectionString("RabbitMQ");
                    rabbitMqBusFactoryConfigurator.Host(new Uri(connectionString), "/",
                        rabbitMqHostConfigurator => { });
                }));
    }

    protected override void ConfigurePipeline(WebApplication webApplication)
    {
        webApplication.MapGrpcService<AccountsController>();
    }
}