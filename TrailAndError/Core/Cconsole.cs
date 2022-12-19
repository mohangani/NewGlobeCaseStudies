using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrailAndError.Core
{
    public static class Cconsole
    {
        private const bool PrintLogs = false;
        public static void WriteLine(string? val)
        {
            if (PrintLogs)
                Console.WriteLine(val);
        }

    }
}
