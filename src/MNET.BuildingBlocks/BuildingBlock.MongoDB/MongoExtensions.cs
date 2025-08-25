using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.MongoDB;

public static class MongoExtensions
{
    public static void AddMongo(this IServiceCollection services, MongoOptions options)
    {
        services.AddSingleton(options);
        services.AddSingleton<IMongoDbService, MongoService>();
    }
}