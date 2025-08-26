using System.Net;
using BuildingBlock.Shared.ValueModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace BuildingBlock.Shared.Extensions;

public static class ExceptionHandlerExtensions
{
    public static void UseCoreExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(errApp =>
        {
            errApp.Run(async httpContext =>
            {
                var feature = httpContext.Features.Get<IExceptionHandlerFeature>();

                if (feature is not null)
                {
                    var exception = feature.Error;
                    httpContext.Response.ContentType = "application/problem+json";
                    httpContext.Response.StatusCode = exception switch
                    {
                        UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                        _ => (int)HttpStatusCode.InternalServerError,
                    };
                    await httpContext.Response.WriteAsJsonAsync(
                        new ApiResponse
                        {
                            Message = exception.Message
                        });
                }
            });
        });
    }
}