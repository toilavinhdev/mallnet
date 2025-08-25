using Microsoft.AspNetCore.Http;

namespace BuildingBlock.Identity.Extensions;

public static class HttpContextAccessorExtensions
{
    public static IdentityUserClaims UserClaims(this IHttpContextAccessor httpContextAccessor)
    {
        var accessToken = httpContextAccessor.HttpContext?.Request.Headers
            .FirstOrDefault(x => x.Key.Equals("Authentication"))
            .Value
            .ToString()
            .Split(" ")
            .LastOrDefault();
        if (string.IsNullOrEmpty(accessToken)) throw new UnauthorizedAccessException("Unauthorized");
        return IdentityUserClaims.DecryptAccessToken(accessToken);
    }
}