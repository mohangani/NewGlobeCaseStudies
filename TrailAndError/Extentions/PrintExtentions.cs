using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrailAndError.Extentions
{
    public static class PrintExtentions
    {
        public static void Print(this int val)
        {
            log(val.ToString());
        }
        public static void Print(this string val)
        {
            log(val);
        }
        public static void Print(this List<Tuple<string, int, List<string>>> val)
        {
            //log($"{val.Count} ({string.Join(", ", val.Select(x => $"{x.Item1}@{x.Item2}").ToList())})");
            log($"{val.Count}");
        }

        private static void log(string val)
        {
            Console.WriteLine(val);
            //Console.WriteLine("------");
        }
    }
}
