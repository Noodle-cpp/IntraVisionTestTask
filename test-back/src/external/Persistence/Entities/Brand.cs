using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities;

public class Brand
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }
}