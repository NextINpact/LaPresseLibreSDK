using System;
using System.Text;

namespace sdk_lpl_mvc5.Utils
{
    public class Timestamp
    {
        public static Int32 UnixTimestamp()
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
