using ApplicationFramework.Infrastructure.HttpClient.Polly;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApplicationFramework.Infrastructure.HttpClient;

public static class HttpClientBuilderExtensions
{
    private const int DefaultRetry = 3;
    private const int DefaultTimeoutInSeconds = 15;
    private const int DefaultHandledEventsAllowedBeforeBreaking = 3;
    private const int DefaultDurationOfBreakInSeconds = 30;

    /// <summary>
    ///     Add Resilient policy handlers:
    ///     <list type="bullet">
    ///         <item>
    ///             <description>Retry Policy With Jitter Strategy (default 3 retries)</description>
    ///         </item>
    ///         <item>
    ///             <description>CircuitBreaker Policy (default 5 handledEvents, duration of break: 30 secs) </description>
    ///         </item>
    ///         <item>
    ///             <description>Timeout Policy (default 15 seconds)</description>
    ///         </item>
    ///     </list>
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="timeout">Timeout of the Optimistic Timeout Policy (in seconds)</param>
    /// <param name="retry"> Number of retries</param>
    /// <param name="handledEventsAllowedBeforeBreaking">CircuitBreaker Param: handledEventsAllowedBeforeBreaking</param>
    /// <param name="durationOfBreakInSeconds">CircuitBreaker Param: durationOfBreakInSeconds</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddResiliencePoliciesHandler(this IHttpClientBuilder builder, int timeout = DefaultTimeoutInSeconds, int retry = DefaultRetry, int handledEventsAllowedBeforeBreaking = DefaultHandledEventsAllowedBeforeBreaking,
        int durationOfBreakInSeconds = DefaultDurationOfBreakInSeconds)
    {
        return builder
            .AddPolicyHandler((services, _) => DefaultResilienceWrapPolicy.GetDefaultResilienceWrapPolicy(services.GetService<ILoggerFactory>()!.CreateLogger("ApplicationFramework.CommonResiliencePolicy"), retry, handledEventsAllowedBeforeBreaking, durationOfBreakInSeconds, timeout));
    }

    /// <summary>
    ///     Add Policy Handler: Timeout Policy (default 15 seconds)
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="timeout">Timeout of the Optimistic Timeout Policy (in seconds)</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddTimeoutPolicyHandler(this IHttpClientBuilder builder, int timeout = DefaultTimeoutInSeconds)
    {
        return builder
            .AddPolicyHandler((services, _) => TimeoutPolicy.GetOptimisticTimeoutPolicy(services.GetService<ILoggerFactory>()!.CreateLogger("ApplicationFramework.TimeoutPolicy"), timeout));
    }

    /// <summary>
    ///     Add Policy Handler: Retry Policy With Jitter Strategy (default 3 retries).
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="retry">Number of retries</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddRetryPolicyHandler(this IHttpClientBuilder builder, int retry = DefaultRetry)
    {
        return builder
            .AddPolicyHandler((services, _) => RetryPolicy.GetPolicyWithJitterStrategy(services.GetService<ILoggerFactory>()!.CreateLogger("ApplicationFramework.RetryPolicy"), retry));
    }

    /// <summary>
    /// Add Policy Handler: CircuitBreaker Policy (default 5 handledEvents, duration of break: 30 secs).
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="handledEventsAllowedBeforeBreaking">CircuitBreaker Param: handledEventsAllowedBeforeBreaking</param>
    /// <param name="durationOfBreakInSeconds">CircuitBreaker Param: durationOfBreakInSeconds</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddCircuitBreakerPolicyHandler(this IHttpClientBuilder builder, int handledEventsAllowedBeforeBreaking = DefaultHandledEventsAllowedBeforeBreaking, int durationOfBreakInSeconds = DefaultDurationOfBreakInSeconds)
    {
        return builder
            .AddPolicyHandler((services, _) => CircuitBreakerPolicy.GetCircuitBreakerPolicy(services.GetService<ILoggerFactory>()!.CreateLogger("ApplicationFramework.CircuitBreakerPolicy"), handledEventsAllowedBeforeBreaking, durationOfBreakInSeconds));
    }
}