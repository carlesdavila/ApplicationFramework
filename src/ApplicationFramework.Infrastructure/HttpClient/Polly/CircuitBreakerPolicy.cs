using System.Net;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace ApplicationFramework.Infrastructure.HttpClient.Polly;

public static class CircuitBreakerPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(ILogger logger, int handledEventsAllowedBeforeBreaking, int durationOfBreakInSeconds)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(message => message.StatusCode == HttpStatusCode.InternalServerError)
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking,
                TimeSpan.FromSeconds(durationOfBreakInSeconds),
                (ex, span) =>
                {
                    logger.LogWarning("Failed! Circuit open, waiting {0}", span);
                    logger.LogWarning("Error was {0}", ex.GetType().Name);
                },
                () => logger.LogWarning("First execution after circuit break succeeded, circuit is reset."),
                () => logger.LogWarning("Circuit is half open.")
            );
    }
}