using System;
using System.Text;

namespace sdk_lpl_mvc5.Utils
{
    public class Timestamp
    {
        /// <summary>
        /// Création d'un timestamp au format unix (en secondes)
        /// </summary>
        /// <returns></returns>
        public static Int32 UnixTimestamp()
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
