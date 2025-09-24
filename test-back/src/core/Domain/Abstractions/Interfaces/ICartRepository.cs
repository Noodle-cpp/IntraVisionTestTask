using Domain.Models;

namespace Domain.Abstractions.Interfaces;

public interface ICartRepository
{
    public Task<IEnumerable<Cart>> GetCartAsync();
    public Task CreateCartAsync(Cart cart);
    public Task UpdateCartAsync(Cart cart);
    public Task<Cart?> GetCartByIdAsync(Guid cartId);
    public Task DeleteCartAsync(Cart cart);
}