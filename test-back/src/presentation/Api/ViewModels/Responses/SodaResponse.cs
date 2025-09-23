namespace Api.ViewModels.Responses;

public class SodaResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Price { get; set; }

    public int Count { get; set; }

    public Guid BrandId { get; set; }
    public BrandResponse Brand { get; set; }
}