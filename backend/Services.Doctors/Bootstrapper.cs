using Services.Doctors.Core.Secrets;
using Services.Doctors.Core.Settings;
using Services.Doctors.WebAPI;
using Services.Shared;

namespace Services.Doctors;

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
    }

    protected override void ConfigureServices(WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddHttpClient("elasticsearch",
            options =>
            {
                var connectionString =
                    webApplicationBuilder.Configuration.GetConnectionString("ElasticsearchConnection");
                options.BaseAddress = new Uri(connectionString);
            }
        );
    }

    protected override void ConfigurePipeline(WebApplication webApplication)
    {
        webApplication.MapGrpcService<DoctorsController>();
        webApplication.MapGrpcService<SpecialitiesController>();
    }
}