using Application.Abstractions.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class AddApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ISodaService, SodaService>();
        services.AddScoped<ICoinService, CoinService>();
        
        return services;
    }
}