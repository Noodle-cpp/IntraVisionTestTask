using Application.ViewModels;

namespace Application.Abstractions.Interfaces;

public interface ICartService
{
    public Task<CartResponseViewModel> GetCartsAsync();
}