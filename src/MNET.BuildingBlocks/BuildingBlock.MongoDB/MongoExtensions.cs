using BuildingBlock.MongoDB.Serializers;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace BuildingBlock.MongoDB;

public static class MongoExtensions
{
    public static void AddMongo(this IServiceCollection services, MongoOptions options)
    {
        BsonSerializer.RegisterSerializer(typeof(DateTimeOffset), new DateTimeOffsetSerializer());
        
        services.AddSingleton(options);
        services.AddSingleton<IMongoService, MongoService>();
    }
}