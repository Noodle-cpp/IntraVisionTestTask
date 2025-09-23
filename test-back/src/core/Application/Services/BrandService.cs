using Application.Abstractions.Interfaces;
using Domain.Abstractions.Interfaces;
using Domain.Models;

namespace Application.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IEnumerable<Brand>> GetBrandsAsync()
    {
        return await _brandRepository.GetAllBrandsAsync().ConfigureAwait(false);
    }
}