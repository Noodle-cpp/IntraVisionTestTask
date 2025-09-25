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
        var coins = await _context.Coins.AsNoTracking().OrderBy(c => c.Banknote).AsNoTracking().ToListAsync().ConfigureAwait(false);
        return coins.Select((c => new Coin()
        {
            Id = c.Id,
            Banknote = c.Banknote,
            Count = c.Count
        }));
    }

    public async Task<Coin?> GetCoinByIdAsync(Guid coinId)
    {
        var coin = await _context.Coins.AsNoTracking().FirstOrDefaultAsync(c => c.Id == coinId).ConfigureAwait(false);
        return coin is null ? null : new Coin()
        {
            Id = coin.Id,
            Banknote = coin.Banknote,
            Count = coin.Count
        };
    }

    public async Task UpdateCoinAsync(Coin coin)
    {
        var updatedCoin = new Entities.Coin()
        {
            Id = coin.Id,
            Banknote = coin.Banknote,
            Count = coin.Count
        };
        
        _context.Coins.Update(updatedCoin);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}