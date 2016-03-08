using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace sdk_lpl_mvc5.Utils
{
    public static class Encrypt
    {
        /// <summary>
        /// Chiffre une chaine de caractères à l'aide de Rijndael 256 (AES 256)
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="keyString"></param>
        /// <param name="ivString"></param>
        /// <returns></returns>
        static public string EncryptRJ256(string plainText, string keyString, string ivString)
        {
            string sRet;

            var encoding = new UTF8Encoding();
            var key = encoding.GetBytes(keyString);
            var iv = encoding.GetBytes(ivString);

            using (var rj = new RijndaelManaged())
            {
                try
                {
                    rj.Padding = PaddingMode.Zeros;
                    rj.Mode = CipherMode.CBC;
                    rj.KeySize = 256;
                    rj.BlockSize = 128;
                    rj.Key = key;
                    rj.IV = iv;

                    var ms = new MemoryStream();

                    using (var cs = new CryptoStream(ms, rj.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        sRet = Convert.ToBase64String(ms.ToArray());
                    }
                }
                finally
                {
                    rj.Clear();
                }
            }

            return sRet;
        }

        /// <summary>
        /// Déchiffre une chaine de caractères à l'aide de Rijndael 256 (AES 256)
        /// </summary>
        /// <param name="cypherText"></param>
        /// <param name="keyString"></param>
        /// <param name="ivString"></param>
        /// <returns></returns>
        static public string DecryptRJ256(string cypherText, string keyString, string ivString)
        {
            string sRet;

            byte[] cypher = Convert.FromBase64String(cypherText);

            var encoding = new UTF8Encoding();
            var key = encoding.GetBytes(keyString);
            var iv = encoding.GetBytes(ivString);

            using (var rj = new RijndaelManaged())
            {
                try
                {
                    rj.Padding = PaddingMode.Zeros;
                    rj.Mode = CipherMode.CBC;
                    rj.KeySize = 256;
                    rj.BlockSize = 128;
                    rj.Key = key;
                    rj.IV = iv;

                    var ms = new MemoryStream(cypher);

                    using (var cs = new CryptoStream(ms, rj.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            sRet = sr.ReadLine();
                        }
                    }
                }
                finally
                {
                    rj.Clear();
                }
            }

            return sRet;
        }
    }
}
