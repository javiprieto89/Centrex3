using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Centrex
{
    public class EncriptarType
    {
        private readonly TripleDES m_des = TripleDES.Create();
        private readonly UTF8Encoding m_utf8 = new UTF8Encoding();

        private readonly byte[] m_key = new byte[]
        {
            1, 2, 3, 4, 5, 6, 7, 8,
            9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24
        };

        private readonly byte[] m_iv = new byte[]
        {
            43, 16, 44, 35, 56, 32, 41, 14
        };

        public string Encriptar(string text)
        {
            var input = m_utf8.GetBytes(text);
            using var encryptor = m_des.CreateEncryptor(m_key, m_iv);
            var output = Transformar(input, encryptor);
            return Convert.ToBase64String(output);
        }

        public string Desencriptar(string text)
        {
            try
            {
                var input = Convert.FromBase64String(text);
                using var decryptor = m_des.CreateDecryptor(m_key, m_iv);
                var output = Transformar(input, decryptor);
                return m_utf8.GetString(output);
            }
            catch
            {
                return "error";
            }
        }

        private static byte[] Transformar(byte[] input, ICryptoTransform cryptoTransform)
        {
            using var memStream = new MemoryStream();
            using var cryptStream = new CryptoStream(memStream, cryptoTransform, CryptoStreamMode.Write);
            cryptStream.Write(input, 0, input.Length);
            cryptStream.FlushFinalBlock();
            return memStream.ToArray();
        }
    }
}

