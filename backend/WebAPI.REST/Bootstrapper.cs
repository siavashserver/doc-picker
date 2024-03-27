using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Accounts;
using Services.Doctors;
using Services.Reservations;
using WebAPI.REST.Secrets;
using WebAPI.REST.Settings;

namespace WebAPI.REST;

public static class Bootstrapper
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.AddExternalConfigurations();
        builder.AddMainServices();
        builder.AddCommonServices();
        builder.AddAuthenticationServices();
        builder.AddSwaggerServices();
    }

    private static void AddExternalConfigurations(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("appsecrets.json", false, true);

        builder.Services.Configure<ApplicationSecrets>(
            builder.Configuration.GetSection("ApplicationSecrets"));

        builder.Services.Configure<ApplicationSettings>(
            builder.Configuration.GetSection("ApplicationSettings"));
    }

    private static void AddMainServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddGrpcClient<Accounts.AccountsClient>(nameof(Accounts.AccountsClient),
            options => options.Address = new Uri("https://localhost:5400"));
        builder.Services.AddGrpcClient<Doctors.DoctorsClient>(nameof(Doctors.DoctorsClient),
            options => options.Address = new Uri("https://localhost:5410"));
        builder.Services.AddGrpcClient<Specialities.SpecialitiesClient>(nameof(Specialities.SpecialitiesClient),
            options => options.Address = new Uri("https://localhost:5410"));
        builder.Services.AddGrpcClient<Reservations.ReservationsClient>(nameof(Reservations.ReservationsClient),
            options => options.Address = new Uri("https://localhost:5420"));
    }

    private static void AddCommonServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors();
        builder.Services.AddControllers().AddJsonOptions(jsonOptions => jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null);
        builder.Services.AddResponseCaching();
    }

    private static void AddAuthenticationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
                    {
                        var sessionTokenSecret = builder.Configuration["ApplicationSecrets:SessionTokenSecret"];
                        SecurityKey issuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sessionTokenSecret));
                        return new List<SecurityKey> { issuerSigningKey };
                    }
                };
            });
    }

    private static void AddSwaggerServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(swaggerGenOptions =>
        {
            swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

            // JWT Authentication
            var securitySchema = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            swaggerGenOptions.AddSecurityDefinition(securitySchema.Reference.Id, securitySchema);
            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securitySchema, Array.Empty<string>() }
            });

            // Include API documentations
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            swaggerGenOptions.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    public static void ConfigureApplicationPipeline(this WebApplication application)
    {
        application.ConfigureExceptionHandler();
        application.ConfigureSwagger();
        application.ConfigureHttps();
        application.ConfigureRouting();
        application.ConfigureAuthentication();
        application.ConfigureResponseCaching();
        application.ConfigureStaticFileServing();
        application.ConfigureEndpoints();
    }

    private static void ConfigureExceptionHandler(this WebApplication application)
    {
        application.UseExceptionHandler("/api/errors");
    }

    private static void ConfigureSwagger(this WebApplication application)
    {
        if (!application.Environment.IsDevelopment()) return;
        application.UseSwagger();
        application.UseSwaggerUI();
    }

    private static void ConfigureHttps(this WebApplication application)
    {
        application.UseHttpsRedirection();
    }

    private static void ConfigureRouting(this WebApplication application)
    {
        application.UseRouting();
        application.UseCors(policy => policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
        );
    }

    private static void ConfigureAuthentication(this WebApplication application)
    {
        application.UseAuthentication();
        application.UseAuthorization();
    }

    private static void ConfigureResponseCaching(this WebApplication application)
    {
        application.UseResponseCaching();
    }

    private static void ConfigureStaticFileServing(this WebApplication application)
    {
        application.UseDefaultFiles();
        application.UseStaticFiles();
    }

    private static void ConfigureEndpoints(this WebApplication application)
    {
        application.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            // Setup SignalR endpoints
            //endpoints.MapHub<PresenceHub>("hubs/presence");
            //endpoints.MapHub<MessageHub>("hubs/message");

            // Forward unhandled urls to Angular/React app router
            // "Index" route inside "Fallback" controller
            //endpoints.MapFallbackToController("Index", "Fallback");
        });
    }
}