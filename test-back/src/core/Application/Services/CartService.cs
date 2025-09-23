using Application.Abstractions.Interfaces;
using Application.ViewModels;
using Domain.Abstractions.Interfaces;

namespace Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<CartResponseViewModel> GetCartsAsync()
    {
        var carts = await _cartRepository.GetCartAsync().ConfigureAwait(false);
        return new CartResponseViewModel()
        {
            Carts = carts,
            Count = carts.Count()
        };
    }
}