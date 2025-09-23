using Domain.Models;

namespace Domain.Abstractions.Interfaces;

public interface ICoinRepository
{
    public Task<IEnumerable<Coin>> GetAllCoinsAsync();
}