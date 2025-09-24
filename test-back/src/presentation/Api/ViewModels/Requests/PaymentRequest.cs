namespace Api.ViewModels.Requests;

public class PaymentRequest
{
    public IEnumerable<CoinRequest> Coins { get; set; }
}