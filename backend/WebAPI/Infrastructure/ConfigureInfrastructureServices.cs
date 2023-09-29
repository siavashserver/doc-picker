using EntityFramework.Exceptions.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebAPI.Core.Common.Interfaces;
using WebAPI.Infrastructure.DataAccess;
using WebAPI.Infrastructure.Security;

namespace WebAPI.Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        // Setup DataBase
        services.AddDbContext<DataContext>(options =>
        {
            options
                .UseExceptionProcessor()
                .UseSqlite(config.GetConnectionString("SQLiteConnection"));
            //.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });

        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddTransient<ISessionTokenService, SessionTokenService>();
        services.AddHttpContextAccessor();
        //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
        services.AddScoped<ICurrentAccountService, CurrentAccountService>();

        return services;
    }
}