using Microsoft.AspNetCore.Authorization;

namespace BuildingBlock.Identity.Authorization;

public class AuthorizationRequirement : IAuthorizationRequirement
{
    public string Permission { get; set; } = null!;
}