using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrailAndError.Models;

namespace TrailAndError.Core
{
    internal class TeacherComputerRetrieval
    {
        private readonly string Routes;
        private readonly Dictionary<char, Dictionary<char, int>> RouteMap;
        private readonly HashSet<char> Academies; // For All uniq Acadmies
        private const string seperator = ", ";

        public TeacherComputerRetrieval(string routes)
        {
            Routes = routes;
            RouteMap = new Dictionary<char, Dictionary<char, int>>();
            Academies = new HashSet<char>();
            AlignRoutes();
        }

        private void AlignRoutes()
        {

            var RouteMapList = Routes.Split(seperator).ToList();
            foreach (var route in RouteMapList)
            {
                var academyRoute = new AcademyRoute(route);

                Academies.Add(academyRoute.From);
                Academies.Add(academyRoute.To);

                if (RouteMap.ContainsKey(academyRoute.From))
                {
                    RouteMap[academyRoute.From].Add(academyRoute.To, academyRoute.Distance);
                    continue;
                }
                RouteMap.Add(academyRoute.From, new Dictionary<char, int> { { academyRoute.To, academyRoute.Distance } });
            }
        }


        public string CaluculateDistanceOfTheRoute(string route, string seperator)
        {
            if (string.IsNullOrEmpty(route) || Academies.Count == 0)
                return string.Empty;

            var list = route.Split(seperator).Select(x => Convert.ToChar(x)).ToList();

            if (list.Any(x => !Academies.Contains(x)))
                return "NO SUCH ROUTE";

            var distance = 0;
            bool validRoute = true;


            for (int i = 0; i < list.Count - 1; i++)
            {
                var from = list[i];
                var to = list[i + 1];
                if (RouteMap.ContainsKey(from) && RouteMap[from].ContainsKey(to))
                {
                    distance += RouteMap[from][to];
                    continue;
                }
                validRoute = false;
                break;
            }

            //Cconsole.WriteLine(validRoute ? distance.ToString() : "NO SUCH ROUTE");
            return validRoute ? distance.ToString() : "NO SUCH ROUTE";
        }

        public List<Tuple<string, int, List<string>>> FindDifferentRoutesBetweenTwoAcademies(char startAcadamy, char endAcadamy, int maxdistance = 0)
        {

            if (startAcadamy is ' ' || endAcadamy is ' ' || !Academies.Contains(startAcadamy) || !Academies.Contains(endAcadamy))
                throw new ArgumentException("Inputs are not Valid");

            // var touches = new HashSet<char>();
            // var distance = 0;
            // string route = startAcadamy.ToString();
            List<Tuple<string, int, List<string>>> routesList = new List<Tuple<string, int, List<string>>>();

            findPath(startAcadamy, 0, "", new HashSet<char>());

            void findPath(char academy, int distance, string route, HashSet<char> touches)
            {
                touches.Add(academy);
                route += $"-{academy}";

                foreach (var item in RouteMap[academy])
                {
                    if (item.Key == endAcadamy) //&& !routesList.Any(x=>x.Item1 == (route + $"-{item.Key}").Trim('-'))
                    {
                        if (maxdistance > 0 && !(distance + item.Value < maxdistance))
                            continue;
                        else
                            routesList.Add(Tuple.Create((route + $"-{item.Key}").Trim('-'), distance + item.Value, (route + $"-{item.Key}").Trim('-').Split('-').ToList()));
                    }

                    if (RouteMap.ContainsKey(item.Key))// && ((touches.Count < 2) || item.Key != touches.ElementAt(touches.Count-2)))
                    { //&& !touches.Contains(item.Key)

                        if ((maxdistance > 0 && (distance + item.Value < maxdistance)) || (maxdistance == 0 && !touches.Contains(item.Key)))
                        {
                            //distance += item.Value;
                            findPath(item.Key, distance + item.Value, route, new HashSet<char>(touches));
                        }
                    }
                }
            }

            Cconsole.WriteLine(string.Join(", ", routesList.Select(x => $"{x.Item1}@{x.Item2}").ToList()));

            return routesList;
        }


        public int FindTheLengthOfTheShortestRoute(char startAcadamy, char endAcadamy)
        {
            var res = FindDifferentRoutesBetweenTwoAcademies(startAcadamy, endAcadamy);
            Cconsole.WriteLine($"{res.Where(x => x.Item2 == res.Min(x => x.Item2)).First().Item1} ({res.Min(x => x.Item2)})");

            return res.Min(x => x.Item2);
        }

        public int FindTheNoOfTripsWithMaximumSpots(char startAcadamy, char endAcadamy, int maxSpots)
        {
            maxSpots++; // to consider the Starting Node As per our code
            var res = FindDifferentRoutesMaximumSpots(startAcadamy, endAcadamy, maxSpots);
            Cconsole.WriteLine(string.Join(", ", res.Where(x => x.Item3.Count <= maxSpots).Select(x => $"{x.Item1}_{x.Item2}").ToList()));
            return res.Where(x => x.Item3.Count <= maxSpots).Count();
        }

        public int FindTheNoOfTripsWithExactSpots(char startAcadamy, char endAcadamy, int exactSpots)
        {
            exactSpots++;
            var res = FindDifferentRoutesMaximumSpots(startAcadamy, endAcadamy, exactSpots);
            Cconsole.WriteLine(string.Join(", ", res.Where(x => x.Item3.Count == exactSpots).Select(x => $"{x.Item1}_{x.Item2}").ToList()));
            return res.Where(x => x.Item3.Count == exactSpots).Count();
        }

        public List<Tuple<string, int, List<string>>> FindDifferentRoutesMaximumSpots(char startAcadamy, char endAcadamy, int maxspots)
        {

            if (startAcadamy is ' ' || endAcadamy is ' ' || !Academies.Contains(startAcadamy) || !Academies.Contains(endAcadamy))
                throw new ArgumentException("Inputs are not Valid");

            List<Tuple<string, int, List<string>>> routesList = new List<Tuple<string, int, List<string>>>();

            findPath(startAcadamy, 0, "", new HashSet<char>());

            void findPath(char academy, int distance, string route, HashSet<char> touches)
            {
                touches.Add(academy);
                route += $"-{academy}";

                foreach (var item in RouteMap[academy])
                {
                    if (item.Key == endAcadamy)
                    {
                        if (maxspots > 0 && (route + $"-{item.Key}").Trim('-').Split('-').Length > maxspots)
                            continue;
                        else
                            routesList.Add(Tuple.Create((route + $"-{item.Key}").Trim('-'), distance + item.Value, (route + $"-{item.Key}").Trim('-').Split('-').ToList()));
                    }

                    if (RouteMap.ContainsKey(item.Key) && !((route + $"-{item.Key}").Trim('-').Split('-').Length > maxspots))
                        findPath(item.Key, distance + item.Value, route, new HashSet<char>(touches));
                }
            }

            Cconsole.WriteLine(string.Join(", ", routesList.Select(x => $"{x.Item1}@{x.Item2}").ToList()));

            return routesList;
        }




        public void PrintMap()
        {

            foreach (var item in RouteMap)
            {
                var academies = string.Join(", ", item.Value);
                Console.WriteLine($"{item.Key}: {academies}");
            }
            Console.WriteLine();
        }
    }
}
