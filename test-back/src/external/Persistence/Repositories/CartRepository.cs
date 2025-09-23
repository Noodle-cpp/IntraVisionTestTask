using Domain.Abstractions.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CartRepository : ICartRepository
{
    private readonly SodaDbContext _context;

    public CartRepository(SodaDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Cart>> GetCartAsync()
    {
        var carts = await _context.Carts.AsNoTracking()
            .Include(c => c.Brand)
            .Include(c => c.Soda).ToListAsync().ConfigureAwait(false);
        return carts.Select(c => new Cart()
        {
            Id = c.Id,
            SodaId = c.SodaId,
            Price = c.Price,
            Count = c.Count,
            BrandId = c.BrandId,
            CreatedAt = c.CreatedAt,
            SodaName = c.SodaName,
            BrandName = c.BrandName,
        });
    }
}