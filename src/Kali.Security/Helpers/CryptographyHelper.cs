using System;
using System.Security.Cryptography;
using System.Text;

namespace Kali.Security.Helpers
{
    public static class CryptographyHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("zoMgRaMpAamuDezt");
        private static readonly byte[] Iv = Encoding.UTF8.GetBytes("A0f08A80E#A08s8f");

        public static string Encrypt(string text)
        {
            var inputBuffer = Encoding.UTF8.GetBytes(text);
            var key = Key;
            var transform = Algorithm.CreateEncryptor(key, Iv);
            var outputBuffer = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        private static SymmetricAlgorithm Algorithm => Aes.Create();

        public static string Decrypt(string text)
        {
            if (text == null)
                return string.Empty;
            var inputBuffer = Convert.FromBase64String(text);
            var key = Key;
            var transform = Algorithm.CreateDecryptor(key, Iv);
            var outputBuffer = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Encoding.UTF8.GetString(outputBuffer);
        }
    }
}
