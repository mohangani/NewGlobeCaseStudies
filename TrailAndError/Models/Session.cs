using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrailAndError.Models
{
    public class Session
    {
        public Session(string name, string duration)
        {
            if (string.IsNullOrEmpty(duration))
                throw new ArgumentNullException($"Duration is not Proper");

            bool isDurationStr = duration == "lightning";

            Name = name;
            Duration = isDurationStr ? 5 : int.TryParse(duration.Replace("min", ""), out int durationInMin) ? durationInMin : 0;

            DurationDesc = isDurationStr ? duration : null;

            if (Duration > 180)
                throw new ArgumentNullException($"No Sessions slots have more than 3 hrs time. \n given Duration is {Duration} mins");
        }

        public string Name { get; }
        public int Duration { get; }

        public string? DurationDesc { get; init; }

    }
}
