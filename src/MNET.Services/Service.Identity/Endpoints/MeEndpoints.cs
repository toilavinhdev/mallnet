using BuildingBlock.Shared.OpenApi.Abstractions;
using BuildingBlock.Shared.ValueModels;

namespace Service.Identity.Endpoints;

public class MeEndpoints : IEndpoints
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/me")
            .WithTags("Me");
        V1(group);
    }
    
    public void V1(RouteGroupBuilder group)
    {
        group.MapGet("", () => Results.Ok())
            .RequireAuthorization()
            .WithSummary("Get account information")
            .Produces<ApiResponse>()
            .MapToApiVersion(1);
        
        group.MapPost("/update-password", () => Results.Ok())
            .RequireAuthorization()
            .WithSummary("Update password")
            .Produces<ApiResponse>()
            .MapToApiVersion(1);
    }
}