using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace ApplicationFramework.Infrastructure.HttpClient.Polly;

public static class CircuitBreakerPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(ILogger logger, int handledEventsAllowedBeforeBreaking = 5, int durationOfBreakInSeconds = 30)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking,
                TimeSpan.FromSeconds(durationOfBreakInSeconds),
                (ex, span) =>
                {
                    logger.LogWarning("Failed! Circuit open, waiting {0}", span);
                    logger.LogWarning("Error was {0}", ex.GetType().Name);
                },
                () => logger.LogWarning("First execution after circuit break succeeded, circuit is reset.")
            );
    }
}