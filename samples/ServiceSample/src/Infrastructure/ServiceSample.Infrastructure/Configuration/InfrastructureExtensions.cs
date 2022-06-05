using Microsoft.Extensions.DependencyInjection;
using ServiceSample.Infrastructure.Configuration.Extensions;

namespace ServiceSample.Infrastructure.Configuration;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClientServices();
    }
}
