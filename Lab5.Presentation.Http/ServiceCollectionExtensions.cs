using Lab5.Applications.Contracts.Config;

namespace Lab5.Presentation.Http;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationHttp(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddControllers();
        collection.Configure<SystemSettings>(configuration.GetSection("SystemSettings"));

        return collection;
    }
}