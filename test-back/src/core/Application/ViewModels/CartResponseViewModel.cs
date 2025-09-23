using Domain.Models;

namespace Application.ViewModels;

public class CartResponseViewModel
{
    public IEnumerable<Cart> Carts { get; set; }
    public int Count { get; set; }
}