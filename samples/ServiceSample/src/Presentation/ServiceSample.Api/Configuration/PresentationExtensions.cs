using ServiceSample.Api.Configuration.Extensions;
using ServiceSample.Application.Configuration;
using ServiceSample.Infrastructure.Configuration;
using ServiceSample.Persistence.Configuration;

namespace ServiceSample.Api.Configuration;

public static class PresentationExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPresentation();
        services.AddApplication();
        services.AddInfrastructure();
        services.AddPersistence(configuration);
    }

    internal static void AddPresentation(this IServiceCollection services)
    {
        services.AddControllers().WithApplicationFrameworkConfiguration();

        services.AddOpenApi();
    }

}

