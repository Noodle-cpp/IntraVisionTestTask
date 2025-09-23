using Application.ViewModels;
using Domain.Models;

namespace Application.Abstractions.Interfaces;

public interface ICartService
{
    public Task<CartResponseViewModel> GetCartsAsync();
    public Task CreateCartAsync(Guid sodaId);
}