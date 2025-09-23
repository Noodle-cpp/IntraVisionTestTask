using Domain.Models;

namespace Domain.Abstractions.Interfaces;

public interface ISodaRepository
{
    public Task<IEnumerable<Soda>> GetSodasAsync(int minPrice, int maxPrice, int page, int perPage, Guid? brandId = null);
    public Task<int> GetMinPriceAsync(Guid? brandId = null);
    public Task<int> GetMaxPriceAsync(Guid? brandId = null);
    public Task<int> GetTotalSodasCountAsync(int minPrice, int maxPrice, Guid? brandId = null);
    public Task<int> GetTotalCountAsync(int minPrice, int maxPrice, Guid? brandId = null);
    public Task<Soda?> GetSodaByIdAsync(Guid sodaId);
}