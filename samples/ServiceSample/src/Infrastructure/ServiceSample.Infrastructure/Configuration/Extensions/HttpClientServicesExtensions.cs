using Microsoft.Extensions.DependencyInjection;

namespace ServiceSample.Infrastructure.Configuration.Extensions;

internal static class HttpClientServicesExtensions
{
    internal static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        //services.AddHttpClient();

        return services;
    }

}
