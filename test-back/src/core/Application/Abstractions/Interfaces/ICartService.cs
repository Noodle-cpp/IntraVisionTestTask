using Application.ViewModels;
using Domain.Models;

namespace Application.Abstractions.Interfaces;

public interface ICartService
{
    public Task<CartResponseViewModel> GetCartsAsync();
    public Task CreateCartAsync(Guid sodaId);
    public Task UpdateCartAsync(Guid cartId, Cart updatedCart);
    public Task DeleteCartByIdAsync(Guid cartId);
    public Task<int> BuyCartAsync(IEnumerable<Coin> coins);
}