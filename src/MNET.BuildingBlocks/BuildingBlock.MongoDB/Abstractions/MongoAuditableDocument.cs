using BuildingBlock.MongoDB.Attributes;
using BuildingBlock.SeedWorks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BuildingBlock.MongoDB.Abstractions;

public abstract class MongoAuditableDocument : MongoDocument, IDateAuditableEntity, IModifierAuditableEntity
{
    [BsonUnderscoreElement]
    public DateTimeOffset? CreatedAt { get; set; }
    
    [BsonUnderscoreElement]
    public string? CreatedBy { get; set; }
    
    [BsonUnderscoreElement]
    public DateTimeOffset? ModifiedAt { get; set; }
    
    [BsonUnderscoreElement]
    public string? ModifiedBy { get; set; }
}