namespace Api.ViewModels.Requests;

public class SodaFilterRequest
{
    public Guid? BrandId { get; set; }
    public int MinPrice { get; set; }
    public int MaxPrice { get; set; }
}