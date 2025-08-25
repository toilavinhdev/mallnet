using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Shared.OpenApi;

public static class OpenApiExtensions
{
    public static void AddCoreOpenApi<TAssembly>(this IServiceCollection services)
    {
        var assembly = typeof(TAssembly).Assembly;
        services.AddEndpointsApiExplorer();
        services.AddCoreMinimalApis(assembly);
        services.AddCoreSwagger(assembly.GetName().Name!);
    }

    public static void UseCoreOpenApi(this WebApplication app, [StringSyntax("Route")] string endpointPrefix,
        Action<ApiVersionSetBuilder> apiVersionSetBuilderAction)
    {
        app.UseCoreMinimalApis(endpointPrefix, apiVersionSetBuilderAction);
        app.UseCoreSwagger();
    }
}