using Domain.Models;

namespace Domain.Abstractions.Interfaces;

public interface ICartRepository
{
    public Task<IEnumerable<Cart>> GetCartAsync();
    public Task CreateCartAsync(Cart cart);
}