namespace Api.ViewModels.Requests;

public class UpdateCartRequest
{
    public Guid SodaId { get; set; }
    public int Price { get; set; }
    public int Count { get; set; }
    public Guid BrandId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string SodaName { get; set; }
    public string BrandName { get; set; }
}