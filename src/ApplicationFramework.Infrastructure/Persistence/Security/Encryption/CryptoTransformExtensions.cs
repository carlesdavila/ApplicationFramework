using System.Security.Cryptography;

namespace ApplicationFramework.Infrastructure.Persistence.Security.Encryption;

public static class CryptoTransformExtensions
{
    public static byte[] Encrypt(this ICryptoTransform cryptoTransform, string plainText)
    {
        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, cryptoTransform, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            //Write all data to the stream.
            swEncrypt.Write(plainText);
        }
        return msEncrypt.ToArray();
    }

    public static string Decrypt(this ICryptoTransform cryptoTransform, byte[] cipher)
    {
        using var msDecrypt = new MemoryStream(cipher);
        using var csDecrypt = new CryptoStream(msDecrypt, cryptoTransform, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        // Read the decrypted bytes from the decrypting stream
        // and place them in a string.
        return srDecrypt.ReadToEnd();
    }
}