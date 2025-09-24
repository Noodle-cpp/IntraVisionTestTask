using Domain.Models;

namespace Application.Abstractions.Interfaces;

public interface ICoinService
{
    public Task<IEnumerable<Coin>> GetCoinsAsync();
}