using ApplicationFramework.Infrastructure.HttpClient.Polly;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApplicationFramework.Infrastructure.HttpClient;

public static class HttpClientBuilderExtensions
{
    /// <summary>
    ///     Add Resilient policy handlers:
    ///     <list type="bullet">
    ///         <item>
    ///             <description>ProcessError DelegatingHandler</description>
    ///         </item>
    ///         <item>
    ///             <description>Retry Policy With Jitter Strategy (default 3 retries)</description>
    ///         </item>
    ///         <item>
    ///             <description>CircuitBreaker Policy</description>
    ///         </item>
    ///     </list>
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="timeout">Timeout of the Optimistic Timeout Policy (in seconds)</param>
    /// <param name="retry"> Number of retries</param>
    /// <param name="handledEventsAllowedBeforeBreaking">CircuitBreaker Param: handledEventsAllowedBeforeBreaking</param>
    /// <param name="durationOfBreakInSeconds">CircuitBreaker Param: durationOfBreakInSeconds</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddResilientHandlers(this IHttpClientBuilder builder, int timeout = 15, int retry = 3, int handledEventsAllowedBeforeBreaking = 5, int durationOfBreakInSeconds = 30)
    {
        return builder
            .AddPolicyHandler((services, _) => RetryPolicy.GetPolicyWithJitterStrategy(services.GetService<ILoggerFactory>()!.CreateLogger("ApplicationFramework.RetryPolicy"), retry))
            .AddPolicyHandler((services, _) => CircuitBreakerPolicy.GetCircuitBreakerPolicy(services.GetService<ILoggerFactory>()!.CreateLogger("ApplicationFramework.CircuitBreakerPolicy"), handledEventsAllowedBeforeBreaking, durationOfBreakInSeconds))
            .AddPolicyHandler((services, _) => TimeoutPolicy.GetOptimisticTimeoutPolicy(services.GetService<ILoggerFactory>()!.CreateLogger("ApplicationFramework.TimeoutPolicy"), timeout));
    }
}