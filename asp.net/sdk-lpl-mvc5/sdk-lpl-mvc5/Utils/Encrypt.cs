using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/**
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
* 
* The MIT License (MIT) Copyright (c) 2016 INpact Mediagroup
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
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
