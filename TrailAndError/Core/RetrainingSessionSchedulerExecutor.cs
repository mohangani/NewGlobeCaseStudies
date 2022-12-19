using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrailAndError.Models;

namespace TrailAndError.Core
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]

    public class RetrainingSessionSchedulerExecutor
    {
        [Benchmark]
        public RetrainingSessionScheduler Run()
        {
            var scheduler = new RetrainingSessionScheduler();
            var Sessions = GetSessions();

            scheduler.Schedule(Sessions);

            return scheduler;
        }

        [Benchmark]
        public RetrainingSessionScheduler Run_Low_TimeComplexity()
        {
            var scheduler = new RetrainingSessionScheduler();
            var Sessions = GetSessions();
            scheduler.Schedule_Low_TimeComplexity(Sessions);
        
            return scheduler;
        }



        private static List<Session> GetSessions()
        {
            return new List<Session> {
                   //new Session("Organising Parents for Academy Improvements1","60"),
                   //new Session("Organising Parents for Academy Improvements2","60"),

                   new Session("Organising Parents for Academy Improvements","60"),
                   new Session("Teaching Innovations in the Pipeline","45"),
                   new Session("Teacher Computer Hacks","30"),
                   new Session("Making Your Academy Beautiful","45"),
                   new Session("Academy Tech Field Repair","45"),
                   new Session("Sync Hard","lightning"),
                   new Session("Unusual Recruiting","lightning"),
                   new Session("Parent Teacher Conferences","60"),
                   new Session("Managing Your Dire Allowance","45"),
                   new Session("Customer Care","30"),
                   new Session("AIMs – 'Managing Up'","30"),
                   new Session("Dealing with Problem Teachers","45"),
                   new Session("Hiring the Right Cook","60"),
                   new Session("Government Policy Changes and New Globe","60"),
                   new Session("Adjusting to Relocation","45"),
                   new Session("Public Works in Your Community","30"),
                   new Session("Talking To Parents About Billing","30"),
                   new Session("So They Say You're a Devil Worshipper","60"),
                   new Session("Two-Streams or Not Two-Streams","30"),
                   new Session("Piped Water","30"),
            };
        }
    }
}