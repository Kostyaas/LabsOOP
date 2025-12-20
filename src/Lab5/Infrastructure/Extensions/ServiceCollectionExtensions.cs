using Itmo.ObjectOrientedProgramming.Lab5.Domain.Ports;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
        services.AddSingleton<ITransactionRepository, InMemoryTransactionRepository>();
        services.AddSingleton<ISessionRepository, InMemorySessionRepository>();

        return services;
    }
}