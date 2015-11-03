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
    }
}