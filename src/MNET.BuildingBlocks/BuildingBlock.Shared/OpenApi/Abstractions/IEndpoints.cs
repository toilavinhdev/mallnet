using Microsoft.AspNetCore.Routing;

namespace BuildingBlock.Shared.OpenApi.Abstractions;

public interface IEndpoints
{
    void MapEndpoints(IEndpointRouteBuilder app);

    void V1(RouteGroupBuilder group);
}