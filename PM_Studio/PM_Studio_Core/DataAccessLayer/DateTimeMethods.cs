using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    public static class DateTimeMethods
    {
        public static long GetTimeStamp(this DateTime dateTime)
        {
            Console.WriteLine(dateTime.Kind.ToString());
            DateTimeOffset dateTimeOffset = dateTime.ToUniversalTime();
            return dateTimeOffset.ToUnixTimeSeconds();
        }
        public static DateTime GetDateTime(this long timeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timeStamp).ToLocalTime();
            return dt;
        }
    }
}
