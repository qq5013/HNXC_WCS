namespace THOK.Util
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class Coding
    {
        internal static string Decoding(string s)
        {
            string str = "FJXMTHOK";
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < (s.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(s.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte) num2;
            }
            provider.Key = System.Text.Encoding.ASCII.GetBytes(str);
            provider.IV = System.Text.Encoding.ASCII.GetBytes(str);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            new StringBuilder();
            return System.Text.Encoding.Default.GetString(stream.ToArray());
        }

        public static string Encoding(string s)
        {
            string str = "FJXMTHOK";
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.Default.GetBytes(s);
            provider.Key = System.Text.Encoding.ASCII.GetBytes(str);
            provider.IV = System.Text.Encoding.ASCII.GetBytes(str);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            builder.ToString();
            return builder.ToString();
        }
    }
}

