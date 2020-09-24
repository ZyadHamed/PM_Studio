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
    }
}
