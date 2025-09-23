namespace Api.ViewModels.Responses;

public class CartListResponse
{
    public IEnumerable<CartResponse> Carts { get; set; }
    public int Count { get; set; }
}