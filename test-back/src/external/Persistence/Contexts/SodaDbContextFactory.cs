using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence.Contexts;

public class SodaDbContextFactory : IDesignTimeDbContextFactory<SodaDbContext>
{
    public SodaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SodaDbContext>();
        optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5433;User ID=postgres;Password=postgres;database=soda;");

        return new SodaDbContext(optionsBuilder.Options);
    }
}