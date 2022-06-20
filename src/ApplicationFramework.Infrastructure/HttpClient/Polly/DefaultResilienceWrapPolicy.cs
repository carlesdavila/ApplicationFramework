using Microsoft.Extensions.Logging;
using Polly;

namespace ApplicationFramework.Infrastructure.HttpClient.Polly;

public static class DefaultResilienceWrapPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetDefaultResilienceWrapPolicy(ILogger logger, int retry, int handledEventsAllowedBeforeBreaking, int durationOfBreakInSeconds, int timeoutInSeconds)
    {
        return Policy.WrapAsync(
            RetryPolicy.GetPolicyWithJitterStrategy(logger, retry),
            CircuitBreakerPolicy.GetCircuitBreakerPolicy(logger, handledEventsAllowedBeforeBreaking, durationOfBreakInSeconds),
            TimeoutPolicy.GetOptimisticTimeoutPolicy(logger, timeoutInSeconds));
    }
}