using Microsoft.EntityFrameworkCore;
using Services.Reservations.Core.DataAccess;
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
    }

    protected override void ConfigurePipeline(WebApplication webApplication)
    {
        webApplication.MapGrpcService<ReservationsController>();
    }
}