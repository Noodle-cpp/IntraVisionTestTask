using Api.ViewModels.Requests;

namespace Api.ViewModels.Responses;

public class CartResponse
{
    public Guid Id { get; set; }

    public Guid SodaId { get; set; }

    public int Price { get; set; }

    public int Count { get; set; }

    public Guid BrandId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string SodaName { get; set; }
    
    public string BrandName { get; set; }   
}