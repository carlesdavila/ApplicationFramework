using ApplicationFramework.Infrastructure.HttpClient.Polly;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationFramework.Infrastructure.HttpClient;

public static class HttpClientBuilderExtensions
{
    /// <summary>
    ///     Add default message handlers and policy handlers:
    ///     <list type="bullet">
    ///         <item>
    ///             <description>ProcessError DelegatingHandler</description>
    ///         </item>
    ///         <item>
    ///             <description>Retry Policy With Jitter Strategy (5 retries)</description>
    ///         </item>
    ///         <item>
    ///             <description>CircuitBreaker Policy</description>
    ///         </item>
    ///     </list>
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="timeout">Timeout of the Optimistic Timeout Policy (in seconds)</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddDefaultHandlers(this IHttpClientBuilder builder, int timeout = 15)
    {
        return builder
            .AddHttpMessageHandler<HttpClientNoCacheDelegatingHandler>()
            .AddPolicyHandler((services, request) => RetryPolicy.GetPolicyWithJitterStrategy(services, 3))
            .AddPolicyHandler((services, request) => CircuitBreakerPolicy.GetCircuitBreakerPolicy(services))
            .AddPolicyHandler((services, request) => TimeoutPolicy.GetOptimisticTimeoutPolicy(services, timeout));
    }

    /// <summary>
    ///     Registers default delegating handlers:
    ///     <list type="bullet">
    ///         <item>
    ///             <description>Registers ProcessError DelegatingHandler</description>
    ///         </item>
    ///         <item>
    ///             <description>Registers HttpContextAccessor that is needed</description>
    ///         </item>
    ///     </list>
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddDefaultDelegatingHandlers(this IServiceCollection services)
    {
        return services
            .AddHttpContextAccessor()
            .AddTransient<HttpClientNoCacheDelegatingHandler>();
    }

    //Todo Change this to T instead of type
    public static IHttpClientBuilder AddClientIf(this IHttpClientBuilder builder, bool condition, Func<IHttpClientBuilder, IHttpClientBuilder> action)
    {
        return condition ? action(builder) : builder;
    }
}