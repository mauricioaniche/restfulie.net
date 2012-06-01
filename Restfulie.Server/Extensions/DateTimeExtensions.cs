using System;

namespace Restfulie.Server.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToRFC3339(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");
        }
    }
}