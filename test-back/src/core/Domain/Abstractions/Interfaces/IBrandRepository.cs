using Domain.Models;

namespace Domain.Abstractions.Interfaces;

public interface IBrandRepository
{
    public Task<IEnumerable<Brand>> GetAllBrandsAsync();
}