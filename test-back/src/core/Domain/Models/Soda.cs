using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Soda
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string ImgPath { get; set; }

    public int Price { get; set; }

    public int Count { get; set; }

    public Guid BrandId { get; set; }
    public Brand Brand { get; set; }
}