using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrailAndError.Models
{
    internal class AcademyRoute
    {
        public char From { get; set; }
        public char To { get; set; }
        public int Distance { get; set; }

        public AcademyRoute(string Route)
        {
            if(string.IsNullOrEmpty(Route) || Route.Length < 3 || !int.TryParse(Route[2..], out int distance))
                throw new ArgumentNullException("Route is not valid");

            From = Route[0];
            To = Route[1];
            Distance = distance;
        }

        public override string ToString()
        {
            return $"{To}({Distance})";

        }
    }
}
