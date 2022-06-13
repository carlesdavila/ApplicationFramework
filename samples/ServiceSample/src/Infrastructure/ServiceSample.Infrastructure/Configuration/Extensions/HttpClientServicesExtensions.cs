using Microsoft.Extensions.DependencyInjection;
using ServiceSample.Application.Interfaces;
using ServiceSample.Infrastructure.Services;

namespace ServiceSample.Infrastructure.Configuration.Extensions;

internal static class HttpClientServicesExtensions
{
    internal static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<ILicenseService, LicenseService>();

        return services;
    }

}
