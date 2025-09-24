using System.Transactions;
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
    private readonly ICoinRepository _coinRepository;

    public CartService(ICartRepository cartRepository, ISodaRepository sodaRepository, ICoinRepository coinRepository)
    {
        _cartRepository = cartRepository;
        _sodaRepository = sodaRepository;
        _coinRepository = coinRepository;
    }

    public async Task<CartResponseViewModel> GetCartsAsync()
    {
        var carts = await _cartRepository.GetCartAsync().ConfigureAwait(false);
        return new CartResponseViewModel()
        {
            Carts = carts,
            Count = carts.Count(),
            TotalPrice = carts.Sum(c => (c.Price * c.Count))
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

    public async Task UpdateCartAsync(Guid cartId, Cart updatedCart)
    {
        var cart = await _cartRepository.GetCartByIdAsync(cartId).ConfigureAwait(false) ?? throw new CartNotFoundException(nameof(cartId));
        
        cart.SodaId = updatedCart.SodaId;
        cart.Price = updatedCart.Price;
        cart.Count = updatedCart.Count;
        cart.BrandId = updatedCart.BrandId;
        cart.CreatedAt = updatedCart.CreatedAt;
        cart.SodaName = updatedCart.SodaName;
        
        await _cartRepository.UpdateCartAsync(cart).ConfigureAwait(false);
    }

    public async Task DeleteCartByIdAsync(Guid cartId)
    {
        var cart = await _cartRepository.GetCartByIdAsync(cartId).ConfigureAwait(false) ?? throw new CartNotFoundException(nameof(cartId));
        await _cartRepository.DeleteCartAsync(cart).ConfigureAwait(false);
    }
    
    public async Task<int> BuyCartAsync(IEnumerable<Coin> coins)
    {
        var carts = await _cartRepository.GetCartAsync().ConfigureAwait(false);
        var totalCartPrice = carts.Sum(c => (c.Price * c.Count));
        var totalCoins = 0;
        
        foreach (var coin in coins)
        {
            var dbCoin = await _coinRepository.GetCoinByIdAsync(coin.Id).ConfigureAwait(false);
            if(dbCoin is not null) totalCoins += dbCoin.Count * dbCoin.Banknote;
        }

        if (totalCartPrice > totalCoins) throw new InsufficientFundsException();

        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        foreach (var cart in carts)
        {
            var soda = await _sodaRepository.GetSodaByIdAsync(cart.SodaId).ConfigureAwait(false);
            soda.Count -= cart.Count;
            await _sodaRepository.UpdateSodaAsync(soda).ConfigureAwait(false);
            await _cartRepository.DeleteCartAsync(cart).ConfigureAwait(false);
        }
        
        transaction.Complete();
        
        return totalCoins - totalCartPrice;
    }
}