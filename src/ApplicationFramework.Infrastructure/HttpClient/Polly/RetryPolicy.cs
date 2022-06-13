using System.Net;
using Polly;
using Polly.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApplicationFramework.Infrastructure.HttpClient.Polly;

public static class RetryPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetPolicyWithJitterStrategy(ILogger logger, int retryCount)
    {
        //https://github.com/App-vNext/Polly/wiki/Retry-with-jitter

        var jitterer = new Random();

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(message => message.StatusCode == HttpStatusCode.InternalServerError)
            .WaitAndRetryAsync(
                retryCount,
                retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) +
                    TimeSpan.FromMilliseconds(jitterer.Next(0, 100)),
                onRetryAsync: (outcome, timespan, retryAttempt, context) =>
                {
                    logger.LogWarning("Delaying for {delay}ms, then making retry {retry}.",
                        timespan.TotalMilliseconds,
                        retryAttempt);

                    return Task.CompletedTask;
                }
            );
    }
}