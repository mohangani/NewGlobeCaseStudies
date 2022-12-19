using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrailAndError.Extentions;

namespace TrailAndError.Core
{
    internal static class TeacherComputerRetrievalExecutor
    {
        public static void Run()
        {
            var Tcr = new TeacherComputerRetrieval("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            Tcr.PrintMap();

            Tcr.CaluculateDistanceOfTheRoute("A-B-C", "-").Print();
            Tcr.CaluculateDistanceOfTheRoute("A-E-B-C-D", "-").Print();
            Tcr.CaluculateDistanceOfTheRoute("A-E-D", "-").Print();

            Tcr.FindTheNoOfTripsWithMaximumSpots('C', 'C', 3).Print();
            Tcr.FindTheNoOfTripsWithExactSpots('A', 'C', 4).Print();

            Tcr.FindTheLengthOfTheShortestRoute('A', 'C').Print();
            Tcr.FindTheLengthOfTheShortestRoute('B', 'B').Print();

            Tcr.FindDifferentRoutesBetweenTwoAcademies('C', 'C', 30).Print();

            
            
            //Tcr.FindDifferentRoutesBetweenTwoAcademies('A', 'C');
            //Tcr.FindDifferentRoutesBetweenTwoAcademies('B', 'B');


            //----My tests with new Graph 
            //Tcr = new TeacherComputerRetrieval("AB6, BC4, BF2, CB4, FE5, ED10, DF2, DC3");
            //Tcr.PrintMap();

            //Tcr.CaluculateDistanceOfTheRoute("A-B-C", "-").Print();
            //Tcr.CaluculateDistanceOfTheRoute("A-E-D", "-").Print();
            //Tcr.CaluculateDistanceOfTheRoute("A-B-F-E-D-C", "-").Print();
            //Tcr.CaluculateDistanceOfTheRoute("A-B-F-E-D-C-B-F-E", "-").Print();
            ////Tcr.CaluculateDistanceOfTheRoute("A-E-B-C-D-E-B-A", "-");

            //Tcr.FindTheNoOfTripsWithMaximumSpots('C', 'C', 3).Print();
            //Tcr.FindTheNoOfTripsWithMaximumSpots('A', 'C', 4).Print();
            //Tcr.FindTheNoOfTripsWithExactSpots('A', 'C', 4).Print();

            //Tcr.FindTheLengthOfTheShortestRoute('A', 'C').Print();
            //Tcr.FindTheLengthOfTheShortestRoute('B', 'B').Print();

            //Tcr.FindDifferentRoutesBetweenTwoAcademies('C', 'C', 30).Print();

            // --- for numerical nodes Testing
            //var Tcr = new TeacherComputerRetrieval("1212, 2312, 3412, 4720, 1512, 1630, 6740");
            //Tcr.PrintMap();
            //Tcr.FindTheLengthOfTheShortestRoute('1', '7').Print();

        }

    }
}
