namespace ApplicationFramework.Infrastructure.Persistence.Security.Encryption;

public interface IEncryptionProvider
{
    string Encrypt(string plainText);
    string Decrypt(string encryptedString);
}