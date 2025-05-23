﻿using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace database_api.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger): IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            _logger.LogError(
                exception,
                "Could not process a request on machine {MachineName}. TraceId: {TraceId}",
                Environment.MachineName,
                traceId
            );

            var (statusCode, title) = MapException(exception);

            await Results.Problem(
                title: title,
                statusCode: statusCode,
                extensions: new Dictionary<string, object?>
                {
                    { "traceId", traceId }
                }
            ).ExecuteAsync(httpContext);

            return true;
        }

        private static (int StatusCode, string Title) MapException(Exception exception)
        {
            return exception switch
            {
                BaseException e => ((int)e.StatusCode, e.Message),
                _ => (StatusCodes.Status500InternalServerError, "We made a mistake but we are working on it!")
            };
        }
    }
}
