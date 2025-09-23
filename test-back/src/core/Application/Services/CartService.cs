using Application.Abstractions.Interfaces;
using Application.Exceptions;
using Application.ViewModels;
using Domain.Abstractions.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly ISodaRepository _sodaRepository;

    public CartService(ICartRepository cartRepository, ISodaRepository sodaRepository)
    {
        _cartRepository = cartRepository;
        _sodaRepository = sodaRepository;
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

    public async Task CreateCartAsync(Guid sodaId)
    {
        var soda = await _sodaRepository.GetSodaByIdAsync(sodaId).ConfigureAwait(false) ??
                   throw new SodaNotFoundException();
        var cart = new Cart()
        {
            Id = Guid.NewGuid(),
            SodaId = sodaId,
            Price = soda.Price,
            Count = 1,
            BrandId = soda.BrandId,
            CreatedAt = DateTime.UtcNow,
            SodaName = soda.Name,
            BrandName = soda.Brand.Name,
        };
        
        await _cartRepository.CreateCartAsync(cart).ConfigureAwait(false);
    }
}