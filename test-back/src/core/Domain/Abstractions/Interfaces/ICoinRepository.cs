using Domain.Models;

namespace Domain.Abstractions.Interfaces;

public interface ICoinRepository
{
    public Task<IEnumerable<Coin>> GetAllCoinsAsync();
    public Task<Coin?> GetCoinByIdAsync(Guid coinId);
    public Task UpdateCoinAsync(Coin coin);
}