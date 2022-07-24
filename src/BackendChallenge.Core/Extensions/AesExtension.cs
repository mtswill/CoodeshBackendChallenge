using System.Security.Cryptography;
using System.Text;

namespace BackendChallenge.Core.Extensions
{
    public static class AesExtension
    {
        private static string _key = string.Empty;
        private static string _iv = string.Empty;

        public static void Configure(string key, string iv)
        {
            _key = key;
            _iv = iv;
        }

        public static string Encrypt(this string plainText)
        {
            var bytes = EncryptStringToBytes_Aes(plainText);
            return Encoding.UTF8.GetString(bytes);
        }

        private static byte[] EncryptStringToBytes_Aes(string plainText)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");

            byte[] encrypted;

            var key = Encoding.UTF8.GetBytes(_key);
            var iv = Encoding.UTF8.GetBytes(_iv);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, iv);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }
    }
}
