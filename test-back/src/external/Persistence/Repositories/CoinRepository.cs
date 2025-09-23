using Domain.Abstractions.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CoinRepository : ICoinRepository
{
    private readonly SodaDbContext _context;

    public CoinRepository(SodaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Coin>> GetAllCoinsAsync()
    {
        var coins = await _context.Coins.AsNoTracking().ToListAsync().ConfigureAwait(false);
        return coins.Select((c => new Coin()
        {
            Id = c.Id,
            Banknote = c.Banknote,
        }));
    }
}