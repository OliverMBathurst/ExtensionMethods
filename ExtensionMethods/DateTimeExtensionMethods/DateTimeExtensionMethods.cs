using System;

namespace ExtensionMethods.DateTimeExtensionMethods
{
    public static class DateTimeExtensionMethods
    {
        public static DateTime FromUnixTime(this long unixTime)
            => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);

        public static long ToUnixTime(this DateTime date) 
            => Convert.ToInt64((date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);        
    }
}
