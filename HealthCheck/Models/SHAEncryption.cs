using System.Security.Cryptography;
using System.Text;


namespace HealthCheck.Models
{
    public class SHAEncryption
    {
        public static string Encrypt(string raw)
        {
            string encryptedtext = string.Empty;
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                encryptedtext = builder.ToString();
            }
            return encryptedtext;
        }
    }
}