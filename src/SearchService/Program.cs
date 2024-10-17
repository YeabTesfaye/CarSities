using SearchService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


var app = builder.Build();

try
{
    await DbInitializer.InitDb(app);
}
catch (Exception e)
{ Console.WriteLine(e); }

app.MapControllers();

app.Run();


