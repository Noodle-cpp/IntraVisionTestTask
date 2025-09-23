using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities;

public class Cart
{
    [Key]
    public Guid Id { get; set; }

    public Guid SodaId { get; set; }
    public Soda Soda { get; set; }

    public int Price { get; set; }

    public int Count { get; set; }

    public Guid BrandId { get; set; }
    public Brand Brand { get; set; }

    public DateTime CreatedAt { get; set; }

    public string SodaName { get; set; }
    
    public string BrandName { get; set; }
}