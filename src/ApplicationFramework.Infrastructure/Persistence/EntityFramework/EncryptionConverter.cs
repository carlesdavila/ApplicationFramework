using ApplicationFramework.Infrastructure.Persistence.Security.Encryption;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApplicationFramework.Infrastructure.Persistence.EntityFramework;

public class EncryptionConverter : ValueConverter<string, string>
{
    /// <summary>
    /// Creates a new <see cref="EncryptionConverter"/> instance.
    /// </summary>
    /// <param name="encryptionProvider">Encryption provider</param>
    /// <param name="mappingHints">Entity Framework mapping hints</param>
    public EncryptionConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null) 
        : base(x => encryptionProvider.Encrypt(x), x => encryptionProvider.Decrypt(x), mappingHints)
    {
    }
}