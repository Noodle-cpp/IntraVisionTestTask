using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Coin
{
    [Key] 
    public Guid Id { get; set; }
    
    [Required]
    public int Banknote { get; set; }

    [Required]
    public int Count { get; set; }
}