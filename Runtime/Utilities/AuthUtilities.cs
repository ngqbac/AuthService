using System.Security.Cryptography;
using System.Text;

namespace AuthService.Utilities
{
    public static class AuthUtilities
    {
        public static string GenerateRandomNonce(int length = 32)
        {
            const string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var result = new StringBuilder(length);
            var bytes = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            for (int i = 0; i < length; i++)
            {
                result.Append(charset[bytes[i] % charset.Length]);
            }

            return result.ToString();
        }

        public static string Sha256(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(bytes);
            var builder = new StringBuilder();
            foreach (var b in hashBytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}