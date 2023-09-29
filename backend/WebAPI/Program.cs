using WebAPI;

/*********************************************************/
/* Add services to the container *************************/
/*********************************************************/
var builder = WebApplication.CreateBuilder(args);
builder.AddApplicationServices();

/*********************************************************/
/* Configure Application *********************************/
/*********************************************************/
var app = builder.Build();
app.ConfigureApplicationPipeline();

/*********************************************************/
/* Migrate Database **************************************/
/*********************************************************/
/*
{
    // Get access to service provider
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        // Get access to DBContext (DataContext) service
        var dataContext = services.GetRequiredService<DataContext>();

        // Automatically run pending database migrations
        // dotnet ef database update
        await dataContext.Database.MigrateAsync();

        // Seed database
        var seed = new Seed(dataContext);
        await seed.PopulateDatabase();
    }
    catch (Exception ex)
    {
        // Get access to Logger service
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured during migration");
    }
}
*/

/*********************************************************/
/* Run Application ***************************************/
/*********************************************************/

app.Run();