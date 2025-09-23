using Application.Extensions;
using Domain.Extensions;
using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Extensions;

namespace DependencyResolver;

public static class DependencyContainer
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services,
                                                        IConfiguration configuration)
    {
        services.AddPersistenceServices(configuration);
        services.AddInfrastructureServices();
        services.AddApplicationServices();
        services.AddDomainServices();
        
        return services;
    }
}