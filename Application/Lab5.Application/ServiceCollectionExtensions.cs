using Lab5.Applications.Contracts.Accounts;
using Lab5.Applications.Contracts.Sessions;
using Lab5.Applications.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Applications;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<ISessionService, SessionService>();
        collection.AddScoped<IAccountService, AccountService>();

        return collection;
    }
}