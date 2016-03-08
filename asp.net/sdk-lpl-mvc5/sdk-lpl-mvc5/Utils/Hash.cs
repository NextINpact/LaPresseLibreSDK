using System.Security.Cryptography;
using System.Text;

namespace sdk_lpl_mvc5.Utils
{
    public class Hash
    {
        /// <summary>
        /// Fonction de hashage d'un tableau de byte avec SHA-1
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string SHA1Hash(byte[] temp)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(temp);
                return ByteArrayToString(hash);
            }
        }

        /// <summary>
        /// Converti un tableau de byte en string hexadecimal
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
