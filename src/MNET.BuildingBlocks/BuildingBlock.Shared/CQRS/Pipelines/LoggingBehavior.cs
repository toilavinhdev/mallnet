using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlock.Shared.CQRS.Pipelines;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "[{RequestName}]: {@RequestValue}",
            typeof(TRequest).Name,
            request);

        var start = Stopwatch.GetTimestamp();
        
        var response = await next(cancellationToken);
        
        var elapsed = Stopwatch.GetElapsedTime(start);
        
        logger.Log(
            elapsed.Seconds < 3 ? LogLevel.Information : LogLevel.Warning,
            "[{RequestName}] finished in {RequestDuration} seconds.",
            typeof(TRequest).Name,
            Stopwatch.GetElapsedTime(start).TotalMilliseconds);
        
        return response;
    }
}