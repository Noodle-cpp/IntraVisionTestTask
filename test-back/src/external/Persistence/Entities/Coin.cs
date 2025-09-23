using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities;

public class Coin
{
    [Key] 
    public Guid Id { get; set; }
    
    [Required]
    public int Banknote { get; set; }
}