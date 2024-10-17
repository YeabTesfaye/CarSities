using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        var mongoClientSettings = MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection"));
        var mongoClient = new MongoClient(mongoClientSettings);
        await DB.InitAsync("SearchDb", MongoClientSettings
.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));


        // to implement search capability 
        await DB.Index<Item>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();
        var count = await DB.CountAsync<Item>();
        if (count is 0)
        {
            Console.WriteLine("No data -- will attempt to seed");
            var itemsData = await File.ReadAllTextAsync("Data/auctions.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var items = JsonSerializer.Deserialize<List<Item>>(itemsData, options);
            await DB.SaveAsync(items);

        }
    }
}