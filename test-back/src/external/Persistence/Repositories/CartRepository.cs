using System.Security.Cryptography.X509Certificates;
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
            Brand = new Brand()
            {
                Id = c.Brand.Id,
                Name = c.Brand.Name
            },
            Soda = new Soda()
            {
                Id = c.Soda.Id,
                Name = c.Soda.Name,
                ImgPath = c.Soda.ImgPath,
                Price = c.Soda.Price,
                Count = c.Soda.Count,
                BrandId = c.Soda.BrandId,
            }
        });
    }

    public async Task CreateCartAsync(Cart cart)
    {
        var newCart = new Entities.Cart()
        {
            Id = cart.Id,
            SodaId = cart.SodaId,
            Price = cart.Price,
            Count = cart.Count,
            BrandId = cart.BrandId,
            CreatedAt = cart.CreatedAt,
            SodaName = cart.SodaName,
            BrandName = cart.BrandName,
        };
        
        await _context.Carts.AddAsync(newCart).ConfigureAwait(false);
        await _context.SaveChangesAsync();
    }
}