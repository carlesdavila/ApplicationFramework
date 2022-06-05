using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace ApplicationFramework.Infrastructure.HttpClient.Polly;

public static class CircuitBreakerPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(IServiceProvider services)
    {
        var loggerFactory = services.GetService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("ApplicationFramework.CircuitBreakerPolicy");

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5,
                TimeSpan.FromSeconds(30),
                (ex, span) =>
                {
                    logger.LogWarning("Failed! Circuit open, waiting {0}", span);
                    logger.LogWarning("Error was {0}", ex.GetType().Name);
                },
                () => logger.LogWarning("First execution after circuit break succeeded, circuit is reset.")
            );

    }
}