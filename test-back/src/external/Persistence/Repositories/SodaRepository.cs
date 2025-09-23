using Domain.Abstractions.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SodaRepository : ISodaRepository
{
    private readonly SodaDbContext _context;

    public SodaRepository(SodaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Soda>> GetSodasAsync(int minPrice, int maxPrice, int page, int perPage, Guid? brandId = null)
    {
        var sodas = await _context.Sodas.AsNoTracking().Include(s => s.Brand).AsNoTracking()
            .Where(s => (s.BrandId == brandId || brandId == null) && (minPrice <= s.Price && s.Price <= maxPrice))
            .Skip((page - 1) * perPage)
            .Take(perPage)
            .ToListAsync().ConfigureAwait(false);
        
        return sodas.Select(s => new Soda()
        {
            Id = s.Id,
            Name = s.Name,
            ImgPath = s.ImgPath,
            Price = s.Price,
            Count = s.Count,
            BrandId = s.BrandId,
            Brand = new Brand()
            {
                Id = s.Brand.Id,
                Name = s.Brand.Name
            },
        });
    }

    public async Task<int> GetTotalCountAsync(int minPrice, int maxPrice, Guid? brandId = null)
    {
        var count = await _context.Sodas.AsNoTracking().AsNoTracking()
            .Where(s => (s.BrandId == brandId || brandId == null) && (minPrice <= s.Price && s.Price <= maxPrice))
            .CountAsync().ConfigureAwait(false);
        
        return count;
    }

    public async Task<Soda?> GetSodaByIdAsync(Guid sodaId)
    {
        var soda = await _context.Sodas.Include(s => s.Brand)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == sodaId).ConfigureAwait(false);
        
        return soda is null ? null : new Soda()
        {
            Id = soda.Id,
            Name = soda.Name,
            ImgPath = soda.ImgPath,
            Price = soda.Price,
            Count = soda.Count,
            BrandId = soda.BrandId,
            Brand = new Brand()
            {
                Id = soda.Brand.Id,
                Name = soda.Brand.Name
            },
        };
    }

    public async Task<int> GetMinPriceAsync(Guid? brandId = null)
    {
        var minPrice = await _context.Sodas.AsNoTracking()
                                                .Where(s => brandId == null || brandId == s.BrandId).MinAsync(s => (int?)s.Price).ConfigureAwait(false);
        return minPrice ?? 0;
    }

    public async Task<int> GetMaxPriceAsync(Guid? brandId = null)
    {
        var maxPrice = await _context.Sodas.AsNoTracking()
            .Where(s => brandId == null || brandId == s.BrandId).MaxAsync(s => (int?)s.Price).ConfigureAwait(false);
        return maxPrice ?? 0;
    }

    public async Task<int> GetTotalSodasCountAsync(int minPrice, int maxPrice, Guid? brandId = null)
    {
        return await _context.Sodas.AsNoTracking().Include(s => s.Brand).AsNoTracking()
            .Where(s => ((brandId != null && s.BrandId == brandId) || brandId == null) && (minPrice <= s.Price && s.Price <= maxPrice))
            .CountAsync().ConfigureAwait(false);
    }
}