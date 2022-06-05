using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace ApplicationFramework.Infrastructure.HttpClient.Polly;

public static class TimeoutPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetOptimisticTimeoutPolicy(IServiceProvider services, int timeout)
    {
        var loggerFactory = services.GetService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("ApplicationFramework.TimeoutPolicy");

        return Policy.TimeoutAsync<HttpResponseMessage>(
            timeout,
            (context, timeSpan, task) =>
            {
                logger.LogWarning("Timeout delegate fired after {delay}ms", timeSpan.TotalMilliseconds);
                return Task.CompletedTask;
            }
        );
    }
}