using BenchmarkDotNet.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrailAndError.Models;

namespace TrailAndError.Core
{
    public class RetrainingSessionScheduler
    {
        private List<Track>? Tracks;

        internal RetrainingSessionScheduler Schedule(List<Session> sessions)
        {
            if (sessions is null || sessions.Count == 0)
                throw new ArgumentNullException("Sesssions Not Available");

            var totalSessionsDuration = sessions.Sum(x => x.Duration);
            int trackIndex = 0, avarageDurationForEachSession = 0, avarageDurationForEachTracK = 0;
            var evaluationRequired = false;
            int noOfTracks = (int)Math.Ceiling((decimal)totalSessionsDuration / Track.TotalTracKDuration);

            sessions = sessions.OrderByDescending(x => x.Duration).ToList();

            do
            {
                evaluationRequired = false;
                avarageDurationForEachTracK = (int)Math.Ceiling((decimal)totalSessionsDuration / noOfTracks);
                avarageDurationForEachSession = (int)Math.Ceiling((decimal)avarageDurationForEachTracK / 2);// since we have only 2 sessions per day (Morning & Afternoon)
                if (avarageDurationForEachSession > 180)
                    avarageDurationForEachSession = 180;

                Tracks = Enumerable.Range(0, noOfTracks).Select(x => new Track(x + 1)).ToList();
                trackIndex = 0;
                
                foreach (var session in sessions)
                {
                    trackIndex = 0;
                    AddSession(session);
                    if (evaluationRequired)
                        break;
                }
            } while (evaluationRequired); // for evaluating the All tracks 


            void AddSession(Session session)
            {
                var track = Tracks[trackIndex];

                if (track.TotalOcupiedDuration() >= avarageDurationForEachTracK)
                {
                    trackIndex++;
                    track = Tracks[trackIndex];
                }

                var morningSessionDuration = track.MorningSessionsOcupiedDuration();

                if (morningSessionDuration + session.Duration <= Track.MorningSessionDuration && morningSessionDuration + +session.Duration <= avarageDurationForEachSession)
                {
                    track.MorningSessions.Add(session);
                    return;
                }

                if (track.AfternoonSessionsOcupiedDuration() + session.Duration <= Track.AfternoonSessionDuration) 
                {
                    track.AfternoonSessions.Add(session);
                    return;
                }

                if (trackIndex + 1 >= noOfTracks)
                {
                    noOfTracks++;
                    evaluationRequired = true; // flag for evaluation of all the Tracks
                    return;
                }

                trackIndex++;
                AddSession(session);

            }

            return this;
        }

        internal RetrainingSessionScheduler Schedule_Low_TimeComplexity(List<Session> sessions) 
        {
            if (sessions is null || sessions.Count == 0)
                throw new ArgumentNullException("Sesssions Not Available");

            var totalSessionsDuration = sessions.Sum(x => x.Duration);

            int noOfTracks = (int)Math.Ceiling((decimal)totalSessionsDuration / Track.TotalTracKDuration);
            int avarageDurationForEachTracK = (int)Math.Ceiling((decimal)totalSessionsDuration / noOfTracks);
            int avarageDurationForEachSession = (int)Math.Ceiling((decimal)avarageDurationForEachTracK / 2);// since we have only 2 sessions per day (Morning & Afternoon)
            if (avarageDurationForEachSession > 180)
                avarageDurationForEachSession = 180;

            Tracks = Enumerable.Range(0, noOfTracks).Select(x => new Track(x + 1)).ToList();

            var trackIndex = 0;

            sessions = sessions.OrderBy(x => x.Duration).ToList(); //asc
            foreach (var session in sessions)
            {
                AddSession(session);
            }

            void AddSession(Session session)
            {
                var track = Tracks[trackIndex];

                if (track.TotalOcupiedDuration() >= avarageDurationForEachTracK)
                {
                    trackIndex++;
                    track = Tracks[trackIndex];
                }

                var morningSessionDuration = track.MorningSessionsOcupiedDuration();

                if (morningSessionDuration + session.Duration <= Track.MorningSessionDuration && morningSessionDuration + +session.Duration <= avarageDurationForEachSession)
                {
                    track.MorningSessions.Add(session);
                    return;
                }

                if (track.AfternoonSessionsOcupiedDuration() + session.Duration <= Track.AfternoonSessionDuration)
                {
                    track.AfternoonSessions.Add(session);
                    return;
                }

                trackIndex++;
                AddSession(session);
            }

            return this;
        }

        public bool Print()
        {
            if (Tracks is null || Tracks.Count == 0)
                return false;

            foreach (var track in Tracks)
            {
                track.Print();
            }

            return true;
        }


    }
}
