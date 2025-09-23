using Application.ViewModels;
using Domain.Models;

namespace Application.Abstractions.Interfaces;

public interface ISodaService
{
    public Task<IEnumerable<Soda>> GetSodasAsync(int minPrice, int maxPrice, int page, int perPage, Guid? brandId = null);
    public Task<int> GetTotalSodasCountAsync(int minPrice, int maxPrice, Guid? brandId = null);
    public Task<int> GetMinPrice(Guid? brandId = null);
    public Task<int> GetMaxPrice(Guid? brandId = null);
    public Task<int> GetTotalCountAsync(int minPrice, int maxPrice, Guid? brandId = null);
    public Task<MemoryStream> GetSodaImgByIdAsync(Guid sodaId);
}    
