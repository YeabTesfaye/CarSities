using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
    {

        var query = DB.PagedSearch<Item, Item>();
        if (!string.IsNullOrWhiteSpace(searchParams.SearchTerm))
        {
            var searchRegex = new BsonRegularExpression(searchParams.SearchTerm, "i");

            query.Match(x =>
              x.Make.Contains(searchParams.SearchTerm, StringComparison.CurrentCultureIgnoreCase) ||
              x.Model.Contains(searchParams.SearchTerm, StringComparison.CurrentCultureIgnoreCase) ||
              x.Color.Contains(searchParams.SearchTerm, StringComparison.CurrentCultureIgnoreCase)
          );
        }

        // Sorting based on provided parameters 
        query = searchParams.OrderBy switch
        {
            "make" => query.Sort(x => x.Ascending(a => a.Make)),
            "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
            _ => query.Sort(x => x.Ascending(a => a.AuctionEnd))
        };

        if (!string.IsNullOrEmpty(searchParams.FilterBy))
        {
            query = searchParams.FilterBy switch
            {
                "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
                "endingSoon" => query.Match(x => x.AuctionEnd < DateTime.UtcNow.AddHours(6) && x.AuctionEnd > DateTime.UtcNow),
                _ => query.Match(expression: x => x.AuctionEnd > DateTime.UtcNow)
            };
        }



        if (!string.IsNullOrEmpty(searchParams.Seller))
        {
            query.Match(x => x.Seller == searchParams.Seller);
        }
        if (!string.IsNullOrEmpty(searchParams.Winner))
        {
            query.Match(x => x.Winner == searchParams.Winner);
        }
        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);
        var result = await query.ExecuteAsync();
        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount

        });
    }
}