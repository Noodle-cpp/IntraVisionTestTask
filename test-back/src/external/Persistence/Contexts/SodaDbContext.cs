using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Contexts;

public class SodaDbContext : DbContext
{
    protected SodaDbContext()
    {
    }

    public SodaDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Coin> Coins { get; set; }
    public DbSet<Soda> Sodas { get; set; }
}