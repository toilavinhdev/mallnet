using BuildingBlock.MongoDB.Abstractions;
using BuildingBlock.MongoDB.Attributes;
using BuildingBlock.SeedWorks;

namespace Service.Identity.Domain.Aggregates.UserAggregate;

public class User : MongoAuditableDocument, IAggregateRoot
{
    [BsonUnderscoreElement]
    public string Email { get; set; } = null!;
    
    [BsonUnderscoreElement]
    public bool IsEmailVerified { get; set; }
    
    [BsonUnderscoreElement]
    public string PhoneNumber { get; set; } = null!;
    
    [BsonUnderscoreElement]
    public bool IsPhoneVerified { get; set; }
    
    [BsonUnderscoreElement]
    public string PasswordHash { get; set; } = null!;
    
    [BsonUnderscoreElement]
    public List<string> RoleIds { get; set; } = [];

    [BsonUnderscoreElement]
    public List<string> Policies { get; set; } = [];
}