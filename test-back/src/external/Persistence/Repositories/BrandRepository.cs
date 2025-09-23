using Domain.Abstractions.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly SodaDbContext _context;

    public BrandRepository(SodaDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
    {
        var brands = await _context.Brands.AsNoTracking().ToListAsync().ConfigureAwait(false);
        return brands.Select(b => new  Brand
        {
            Id = b.Id,
            Name = b.Name
        });
    }
}