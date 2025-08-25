using BuildingBlock.DataExtensions;
using BuildingBlock.MongoDB.Collections;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace BuildingBlock.MongoDB;

public interface IMongoDbService
{
    IMongoCollection<T> Collection<T>();

    Task<long> NextSequenceAsync<T>(CancellationToken cancellationToken = default);

    Task<IClientSessionHandle> StartSessionAsync(ClientSessionOptions? options = null,
        CancellationToken cancellationToken = default);
}

public class MongoService : IMongoDbService
{
    private readonly MongoClient _client;
    
    private readonly IMongoDatabase _database;

    public MongoService(ILogger<MongoService> logger, MongoOptions options)
    {
        var settings = MongoClientSettings.FromConnectionString(options.Url);
        settings.ClusterConfigurator = builder =>
        {
            builder.Subscribe<CommandStartedEvent>(e =>
            {
                logger.LogInformation("Mongo driver execute command {Name}: {@Command}", e.CommandName, BsonExtensionMethods.ToJson(e.Command));
            });
        };
        _client = new MongoClient(settings);
        _database = _client.GetDatabase(options.Database);
    }
    
    public IMongoCollection<T> Collection<T>()
    {
        var collectionName = typeof(T).Name;
        return _database.GetCollection<T>(collectionName.ToUnderscoreCase());
    }

    public async Task<long> NextSequenceAsync<T>(CancellationToken cancellationToken = default)
    {
        var sequenceFilter =  Builders<MongoSequence>.Filter.Eq(x => x.CollectionName, typeof(T).Name);
        var sequenceAsyncCursor = await Collection<MongoSequence>()
            .FindAsync(sequenceFilter, cancellationToken: cancellationToken);
        var sequence = await sequenceAsyncCursor.FirstOrDefaultAsync(cancellationToken);
        if (sequence is null)
        {
            sequence = new MongoSequence
            {
                Id = ObjectId.GenerateNewId().ToString(),
                CollectionName = typeof(T).Name.ToUnderscoreCase(),
                Value = 1,
                CreatedAt = DateTimeExtensions.Now,
            };
            await Collection<MongoSequence>().InsertOneAsync(sequence, cancellationToken: cancellationToken);
        }
        else
        {
            sequence.Value++;
            sequence.ModifiedAt = DateTimeExtensions.Now;
            await Collection<MongoSequence>().UpdateOneAsync(
                sequenceFilter,
                Builders<MongoSequence>.Update
                    .Set(x => x.Value, sequence.Value)
                    .Set(x => x.ModifiedAt, sequence.ModifiedAt),
                cancellationToken: cancellationToken);
        }
        return sequence.Value;
    }

    public async Task<IClientSessionHandle> StartSessionAsync(ClientSessionOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _client.StartSessionAsync(options, cancellationToken);
    }
}