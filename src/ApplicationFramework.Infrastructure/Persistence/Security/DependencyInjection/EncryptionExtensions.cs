using ApplicationFramework.Infrastructure.Persistence.Security.Encryption;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationFramework.Infrastructure.Persistence.Security.DependencyInjection;

public static class EncryptionExtensions
{
    /// <summary>
    ///     Enables the database encryption.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="encryptionKey">The encryption key.</param>
    /// <returns></returns>
    public static IServiceCollection EnableDbEncryption(this IServiceCollection services, string encryptionKey)
    {
        return services.AddTransient<IEncryptionProvider, AesEncryptionProvider>(x =>
            new AesEncryptionProvider(encryptionKey));
    }
}