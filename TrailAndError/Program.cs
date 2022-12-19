using BenchmarkDotNet.Running;
using TrailAndError;
using TrailAndError.Core;
using TrailAndError.Extentions;


try
{
    Console.WriteLine($"{Environment.NewLine}----------------------TeacherComputerRetrieval----------------------- \n"); 
    
    TeacherComputerRetrievalExecutor.Run();

    Console.WriteLine($"\n ----------------------RetrainingSessionScheduler----------------------- \n"); 

    new RetrainingSessionSchedulerExecutor().Run().Print();

    //for bench marks
    //var summary = BenchmarkRunner.Run<RetrainingSessionSchedulerExecutor>();
    //new RetrainingSessionSchedulerExecutor().Run_Low_TimeComplexity().Print();

}
catch (Exception ex)
{
    Console.WriteLine(ex);
}









