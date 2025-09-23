using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class AddInfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        return services;
    }
}