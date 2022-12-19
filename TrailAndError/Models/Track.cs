using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrailAndError.Core;

namespace TrailAndError.Models
{
    internal class Track
    {
        public int Id { get; set; }

        public Track(int id)
        {
            Id = id;
        }

        public const int MorningSessionDuration = 3 * 60; // 180
        public const int AfternoonSessionDuration = 4 * 60; // 180 (+60!)
        public const int TotalTracKDuration = MorningSessionDuration + AfternoonSessionDuration; // 360 (+60!)
        public List<Session> MorningSessions { get; set; } = new List<Session>();
        public List<Session> AfternoonSessions { get; set; } = new List<Session>();
        private List<Session> LunchSession { get; set; } = new List<Session> { new Session("Lunch", "60") };
        private List<Session> SharingSession { get; set; } = new List<Session> { new Session("Sharing Session", "30") };

        public int MorningSessionsOcupiedDuration()
        {
            return MorningSessions.Sum(x => x.Duration);
        }

        public int AfternoonSessionsOcupiedDuration()
        {
            return AfternoonSessions.Sum(x => x.Duration);
        }

        public int TotalOcupiedDuration()
        {
            return AfternoonSessions.Sum(x => x.Duration) + MorningSessions.Sum(x => x.Duration);
        }


        public void Print()
        {
            TimeOnly time = new TimeOnly(9, 0);

            Console.WriteLine($"TRACK - {Id} {Environment.NewLine}");
            Console.WriteLine($"Time \t\t | Duration\t | \t\t\tSession  ");
            Console.WriteLine($"---------------------------------------------------------------------------------");

            log(MorningSessions);
            time = new TimeOnly(12, 0);
            log(LunchSession);
            log(AfternoonSessions);
            time = new TimeOnly(5, 0);
            log(SharingSession);

            Console.WriteLine(Environment.NewLine);



            void log(List<Session> sessions)
            {
                if (sessions is null)
                    return;
                foreach (var session in sessions)
                {
                    var duration = session.DurationDesc is null ? $"{session.Duration}min" : session.DurationDesc;

                    Console.WriteLine($"{time:hh:mm tt} \t | {duration} \t | {session.Name}");
                    time = time.AddMinutes(session.Duration);
                }
            }
        }
    }

}
