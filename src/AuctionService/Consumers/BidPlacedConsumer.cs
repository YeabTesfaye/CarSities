using AuctionService.Data;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers;

public class BidPlacedConsumer(AuctionDbContext dbContext) : IConsumer<BidPlaced>
{
    private readonly AuctionDbContext _dbContext = dbContext;

    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine("--> Consuming Bid placed");
        var auction = await _dbContext.Auctions.FindAsync(context.Message.AuctinId);

        if (auction.CurrentHighBid == null
         || context.Message.BidStatus.Contains("Äccepted")
         && context.Message.Amount > auction.CurrentHighBid)
        {
           auction.CurrentHighBid = context.Message.Amount;
           await _dbContext.SaveChangesAsync();
        }
    }
}