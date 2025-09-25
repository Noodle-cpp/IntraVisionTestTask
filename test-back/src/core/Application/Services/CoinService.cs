using System.Transactions;
using Application.Abstractions.Interfaces;
using Application.Exceptions;
using Domain.Abstractions.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CoinService : ICoinService
{
    private readonly ICoinRepository _coinRepository;

    public CoinService(ICoinRepository coinRepository)
    {
        _coinRepository = coinRepository;
    }

    public async Task<IEnumerable<Coin>> GetCoinsAsync()
    {
        return await _coinRepository.GetAllCoinsAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<Coin>> ChangeOfCoinAsync(int amount)
    {
        var coins = await _coinRepository.GetAllCoinsAsync().ConfigureAwait(false);
        var change = new List<Coin>();
        
        var sortedCoins = coins.OrderByDescending(x => x.Banknote).ToList();
        foreach (var coin in sortedCoins)
        {
            var neededCount = amount / coin.Banknote;
            var actualAmount = amount - (neededCount * coin.Banknote);
            if (actualAmount < 0) continue;
            amount = actualAmount;
            
            change.Add(new Coin()
            {
                Id = coin.Id,
                Banknote = coin.Banknote,
                Count = neededCount,
            });
        }
        
        if (amount > 0) throw new ChangeCoinException();
        
        return change;
    }

    public async Task CountNewBalanceAsync(IEnumerable<Coin> change, IEnumerable<Coin> coins)
    {
        var dbCoins = await _coinRepository.GetAllCoinsAsync().ConfigureAwait(false);
        foreach (var dbCoin in dbCoins)
        {
            dbCoin.Count += coins.FirstOrDefault(x => x.Id == dbCoin.Id)?.Count ?? 0;
            dbCoin.Count -= change.FirstOrDefault(x => x.Id == dbCoin.Id)?.Count ?? 0;
            if (dbCoin.Count < 0) throw new InsufficientFundsException();
            
            await _coinRepository.UpdateCoinAsync(dbCoin).ConfigureAwait(false);
        }
    }
}