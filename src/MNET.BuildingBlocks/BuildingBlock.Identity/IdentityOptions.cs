namespace BuildingBlock.Identity;

public class IdentityOptions
{
    public string TokenSigningKey { get; set; } = null!;
    
    public int AccessTokenDurationInMinutes { get; set; }

    public int RefreshTokenDurationInDays { get; set; }
    
    public string? Issuer { get; set; }

    public string? Audience { get; set; }
}