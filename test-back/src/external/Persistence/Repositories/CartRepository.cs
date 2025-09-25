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
            .Include(c => c.Brand).AsNoTracking()
            .Include(c => c.Soda).AsNoTracking().ToListAsync().ConfigureAwait(false);
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

    public async Task UpdateCartAsync(Cart cart)
    {
        var updatedCart = new Entities.Cart()
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
        _context.Carts.Update(updatedCart);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<Cart?> GetCartByIdAsync(Guid cartId)
    {
        var cart = await _context.Carts.AsNoTracking()
            .Include(c => c.Brand).AsNoTracking()
            .Include(c => c.Soda).AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == cartId).ConfigureAwait(false);
        
        return cart is null ? null : new Cart()
        {
            Id = cart.Id,
            SodaId = cart.SodaId,
            Price = cart.Price,
            Count = cart.Count,
            BrandId = cart.BrandId,
            CreatedAt = cart.CreatedAt,
            SodaName = cart.SodaName,
            BrandName = cart.BrandName,
            Brand = new Brand()
            {
                Id = cart.Brand.Id,
                Name = cart.Brand.Name
            },
            Soda = new Soda()
            {
                Id = cart.Soda.Id,
                Name = cart.Soda.Name,
                ImgPath = cart.Soda.ImgPath,
                Price = cart.Soda.Price,
                Count = cart.Soda.Count,
                BrandId = cart.Soda.BrandId,
            }
        };
    }

    public async Task DeleteCartAsync(Cart cart)
    {
        var cartToDelete = new Entities.Cart()
        {
            Id = cart.Id,
            SodaId = cart.SodaId,
            Price = cart.Price,
            Count = cart.Count,
            BrandId = cart.BrandId,
            CreatedAt = cart.CreatedAt,
            SodaName = cart.SodaName,
            BrandName = cart.BrandName
        };

        _context.Carts.Remove(cartToDelete);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}