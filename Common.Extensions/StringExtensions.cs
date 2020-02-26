using System;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string GetRandom(this List<string> list)
        {
            var max = list.Count;
            var rnd = new Random();
            var rndNmb = rnd.Next(0, max);
            return list[rndNmb];
        }

        public static bool IsInt(this string s)
        {
            var x = 0;
            return int.TryParse(s, out x);
        }
    }
}