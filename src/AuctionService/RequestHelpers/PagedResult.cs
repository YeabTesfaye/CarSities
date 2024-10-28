namespace AuctionService.RequestHelpers;

public class PagedResult<T>
{
   public List<T> Results { get; set; }
   public int PageCount { get; set; }
   public int TotalItems { get; set; } 
}