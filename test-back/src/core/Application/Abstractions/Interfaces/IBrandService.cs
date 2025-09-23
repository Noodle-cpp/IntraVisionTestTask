using Domain.Models;

namespace Application.Abstractions.Interfaces;

public interface IBrandService
{
    public Task<IEnumerable<Brand>> GetBrandsAsync();
}