using BuildingBlock.Shared.OpenApi.Abstractions;
using BuildingBlock.Shared.ValueModels;

namespace Service.Identity.Endpoints;

public class AuthEndpoints : IEndpoints
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/auth")
            .WithTags("Auth");
        V1(group);
        V2(group);
    }

    public void V1(RouteGroupBuilder group)
    {
        group.MapPost("/sign-up", () => Results.Ok())
            .WithSummary("Register new account")
            .Produces<ApiResponse>()
            .MapToApiVersion(1);

        group.MapPost("/sign-in", () => Results.Ok())
            .WithSummary("Login your account")
            .Produces<ApiResponse>()
            .MapToApiVersion(1);
    }
    
    public void V2(RouteGroupBuilder group)
    {
        group.MapPost("/sign-up", () => Results.Ok())
            .WithSummary("Register new account")
            .Produces<ApiResponse>()
            .MapToApiVersion(2);

        group.MapPost("/sign-in", () => Results.Ok())
            .WithSummary("Login your account")
            .Produces<ApiResponse>()
            .MapToApiVersion(2);
    }
}