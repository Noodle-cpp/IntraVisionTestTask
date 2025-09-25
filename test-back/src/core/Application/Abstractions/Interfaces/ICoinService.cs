using Domain.Models;

namespace Application.Abstractions.Interfaces;

public interface ICoinService
{
    public Task<IEnumerable<Coin>> GetCoinsAsync();
    public Task<IEnumerable<Coin>> ChangeOfCoinAsync(int amount);
    public Task CountNewBalanceAsync(IEnumerable<Coin> change, IEnumerable<Coin> coins);
}