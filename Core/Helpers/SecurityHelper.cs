namespace SerhendKumbara.Core.Helpers;

public static class SecurityHelper
{
    static readonly string key = "6c879798-ee9a-4c97-9eec-6fe5b1b5204b";
    static readonly byte[] salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

    public static string Encrypt(this string plainText)
    {
        byte[] clearBytes = Encoding.UTF8.GetBytes(plainText);
        using (Aes encryptor = Aes.Create())
        {
            var rfc = new Rfc2898DeriveBytes(key, salt);
            encryptor.Key = rfc.GetBytes(32);
            encryptor.IV = rfc.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }
            plainText = Convert.ToBase64String(ms.ToArray());
        }
        return plainText;
    }

    public static string Decrypt(this string encryptedText)
    {
        byte[] cipherBytes = Convert.FromBase64String(encryptedText);
        using (Aes encryptor = Aes.Create())
        {
            var rfc = new Rfc2898DeriveBytes(key, salt);
            encryptor.Key = rfc.GetBytes(32);
            encryptor.IV = rfc.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
            }
            encryptedText = Encoding.UTF8.GetString(ms.ToArray());
        }
        return encryptedText;

    }
}