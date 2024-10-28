using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AuctionService.RequestHelpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/auctions")]
public class AuctionsController(AuctionDbContext context, IMapper mapper,
    IPublishEndpoint publishEndpoint) : ControllerBase
{
    private readonly AuctionDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions(string date)
    {
        var query = _context.Auctions.OrderBy(x => x.Item.Make).AsQueryable();
        if (!string.IsNullOrEmpty(date))
        {
            if (DateTime.TryParse(date, out DateTime parsedDate))
            {
                query = query.Where(x => x.UpdatedAt >= parsedDate.ToUniversalTime());
            }
            else BadRequest("Invalid date format");
        }


        var results = await query
            .ProjectTo<AuctionDto>(_mapper.ConfigurationProvider) // Project to DTOs
            .ToListAsync();

        return Ok(results);
    }
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById([FromRoute] Guid id)
    {
        var auction = await _context.Auctions.Where(a => a.Id == id)
        .Include(x => x.Item)
        .FirstOrDefaultAsync();
        if (auction is null)
        {
            return NotFound();
        }
        return _mapper.Map<AuctionDto>(auction);
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        var auction = _mapper.Map<Auction>(auctionDto);

        auction.Seller = User.Identity.Name;

        _context.Auctions.Add(auction);

        var newAuction = _mapper.Map<AuctionDto>(auction);

        await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction));

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest("Could not save changes to the DB");

        return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, newAuction);
    }


    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateAuction([FromRoute] Guid id, UpdateAuctionDto updateAuctionDto)
    {
        var auction = await _context.Auctions.Include(x => x.Item)
        .FirstOrDefaultAsync(x => x.Id == id);

        if (auction is null) return NotFound();

        if (auction.Seller != User.Identity.Name) return Forbid();

        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;

        await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest("Problem saving changes!!");

        return Ok();
    }
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAuction([FromRoute] Guid id)
    {
        var auction = await _context.Auctions.Include(x => x.Item)
        .FirstOrDefaultAsync(x => x.Id == id);
        if (auction is null) return NotFound();

        if (auction.Seller != User.Identity.Name) return Forbid();

        await _publishEndpoint.Publish<AuctionDeleted>(new { Id = auction.Id.ToString() });

        _context.Auctions.Remove(auction);

        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Could not update DB");
        return NoContent();
    }

}