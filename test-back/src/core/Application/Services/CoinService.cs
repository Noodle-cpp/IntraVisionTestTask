using Application.Abstractions.Interfaces;
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
}