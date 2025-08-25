using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;

namespace BuildingBlock.Identity;

public class IdentityUserClaims
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;
    
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = null!;
    
    [JsonPropertyName("policies")]
    public List<string> Policies { get; set; } = null!;
    
    public string GenerateAccessToken(IdentityOptions options)
    {
        return GenerateAccessToken(
            options.TokenSigningKey,
            options.AccessTokenDurationInMinutes,
            options.Issuer,
            options.Audience);
    }
    
    private string GenerateAccessToken(
        string signingKey,
        int accessTokenDurationInMinutes,
        string? issuer,
        string? audience)
    {
        var claims = new List<Claim>
        {
            new("id", Id),
            new("email", Email),
            new("phoneNumber", PhoneNumber),
            new("policies",  string.Join(",", Policies))
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(accessTokenDurationInMinutes);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            IssuedAt = DateTime.UtcNow,
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            SigningCredentials = credential
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);
        return accessToken;
    }

    public static IdentityUserClaims DecryptAccessToken(string accessToken)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(accessToken);
        
        var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "id")?.Value ??
                 throw new NullReferenceException("Claim type 'id' cannot access");
        var email = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "email")?.Value ??
                    throw new NullReferenceException("Claim type 'email' cannot access");
        var phoneNumber = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "phoneNumber")?.Value ??
                          throw new NullReferenceException("Claim type 'phoneNumber' cannot access");
        var policies = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "policies")?.Value ??
                       throw new NullReferenceException("Claim type 'policies' cannot access");
        return new IdentityUserClaims
        {
            Id = id,
            Email = email,
            PhoneNumber = phoneNumber,
            Policies = policies.Split(",").ToList(),
        };
    }
}