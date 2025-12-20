using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, string adminPassword)
    {
        services.AddScoped<IATMService>(provider =>
        {
            IAccountRepository accountRepository = provider.GetRequiredService<IAccountRepository>();
            ITransactionRepository transactionRepository = provider.GetRequiredService<ITransactionRepository>();
            ISessionRepository sessionRepository = provider.GetRequiredService<ISessionRepository>();

            return new ATMService(
                accountRepository,
                transactionRepository,
                sessionRepository,
                adminPassword);
        });

        return services;
    }
}