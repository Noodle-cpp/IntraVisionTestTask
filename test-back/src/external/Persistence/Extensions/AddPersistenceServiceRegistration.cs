using Domain.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class AddPersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        var dbConfiguration = configuration.GetConnectionString("Postgres") ?? throw new ArgumentNullException(nameof(configuration));
        services.AddDbContext<SodaDbContext>(options => options.UseNpgsql(dbConfiguration));

        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICoinRepository, CoinRepository>();
        services.AddScoped<ISodaRepository, SodaRepository>();

        return services;
    }
}