using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions;

public static class AddDomainServiceRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services;
    }
}