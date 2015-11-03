using System;
using System.Linq;

namespace HackerNewsWindows10
{
    public static class Util
    {
        public static string[] StringToArray(string s)
        {
            return s.TrimBrackets().Split(',').Select(t => t).ToArray();
        }


        private static string TrimBrackets(this string s)
        {
            return s.TrimStart('[').TrimEnd(']').Trim();
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}