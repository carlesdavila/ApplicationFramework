using System.Security.Cryptography;

namespace ApplicationFramework.Infrastructure.Persistence.Security.Encryption;

public class AesEncryptionProvider : IEncryptionProvider
{
    private readonly byte[] _encryptionKey;
    private readonly int _initializationVectorSize = 24;
    private readonly int _initializationVersionSize = 8;
    public readonly string Version = "VjEuMDA=";       // Base64 -> V1.00

    public AesEncryptionProvider(string encryptionKey)
    {
        if (string.IsNullOrWhiteSpace(encryptionKey))
        {
            throw new ArgumentException("EncryptionKey must not be empty");
        }

        _encryptionKey = Convert.FromBase64String(encryptionKey);
    }

    public string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        var encryptor = aes.CreateEncryptor(_encryptionKey, aes.IV);
        var encryptedBytes = encryptor.Encrypt(plainText);

        return string.Concat(Version, Convert.ToBase64String(aes.IV),  Convert.ToBase64String(encryptedBytes));
    }

    public string Decrypt(string encryptedString)
    {
        if (string.IsNullOrEmpty(encryptedString))
        {
            return encryptedString;
        }

        var version = encryptedString.Substring(0, _initializationVersionSize);
        if (version != Version)
        {
            throw new  ArgumentException("EncryptionKey must has a correct Version");
        }

        var iv = Convert.FromBase64String(encryptedString.Substring(_initializationVersionSize, _initializationVectorSize));
        var cipher = Convert.FromBase64String(encryptedString.Substring(_initializationVersionSize + _initializationVectorSize));

        using var aes = Aes.Create();
        var decryptor = aes.CreateDecryptor(_encryptionKey, iv);
        var plainText = decryptor.Decrypt(cipher);

        return plainText;
    }
}