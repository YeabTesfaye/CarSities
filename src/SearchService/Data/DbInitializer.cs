using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        var mongoClientSettings = MongoClientSettings.FromConnectionString(
            app.Configuration.GetConnectionString("MongoDbConnection")
        );
        var mongoClient = new MongoClient(mongoClientSettings);
        await DB.InitAsync("SerchDb", mongoClientSettings);

        // to implement search capability using make,model and color 
        await DB.Index<Item>()
        .Key(x => x.Make, KeyType.Text)
        .Key(x => x.Model, KeyType.Text)
        .Key(x => x.Color, KeyType.Text)
        .CreateAsync();

        var count = await DB.CountAsync<Item>();
        using var scope = app.Services.CreateScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();

        var items =  await httpClient.GetItemsForSearchDb();

        if (items.Count > 0) await DB.SaveAsync(items);
    }
}