using Lab5.Applications.Abstractions.Repositories;
using Lab5.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection collection)
    {
        collection.AddSingleton<IAccountRepository, AccountRepository>();
        collection.AddSingleton<ISessionRepository, SessionRepository>();
        collection.AddSingleton<ITransactionRepository, TransactionRepository>();

        return collection;
    }
}