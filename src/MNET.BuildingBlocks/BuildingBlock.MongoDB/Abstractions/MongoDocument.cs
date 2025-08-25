using BuildingBlock.MongoDB.Attributes;
using BuildingBlock.SeedWorks;

namespace BuildingBlock.MongoDB.Abstractions;

public abstract class MongoDocument : Entity<string>
{
    [BsonUnderscoreElement]
    public long SubId { get; set; }
}