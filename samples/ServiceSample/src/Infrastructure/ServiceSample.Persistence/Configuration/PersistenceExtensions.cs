using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceSample.Application.Interfaces;

namespace ServiceSample.Persistence.Configuration;

public static class PersistenceExtensions
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ServiceSampleDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("ServiceSampleConnection"),
                x => x.MigrationsAssembly(typeof(ServiceSampleDbContext).Assembly.FullName)));

        services.AddScoped<IServiceSampleDbContext>(provider => provider.GetRequiredService<ServiceSampleDbContext>());
    }
}
