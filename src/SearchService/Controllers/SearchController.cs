using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController  : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems()
    {
        var query = DB.Find<Item>();
        query.Sort(x => x.Ascending(a => a.Make));
        var result = await query.ExecuteAsync();
        return result;
    }
}