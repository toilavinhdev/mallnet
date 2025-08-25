using BuildingBlock.Shared.CQRS.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Shared.CQRS;

public static class MediatorExtensions
{
    public static void AddCoreMediator<TAssembly>(this IServiceCollection services, Action<MediatRServiceConfiguration>? configuration = null)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<TAssembly>();
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration?.Invoke(config);
        });
    }
}