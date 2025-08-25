using BuildingBlock.MongoDB.Attributes;
using BuildingBlock.SeedWorks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BuildingBlock.MongoDB.Abstractions;

public abstract class MongoAuditableDocument : MongoDocument, IDateAuditableEntity, IModifierAuditableEntity
{
    [BsonUnderscoreElement]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTimeOffset? CreatedAt { get; set; }
    
    [BsonUnderscoreElement]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CreatedBy { get; set; }
    
    [BsonUnderscoreElement]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTimeOffset? ModifiedAt { get; set; }
    
    [BsonUnderscoreElement]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ModifiedBy { get; set; }
}