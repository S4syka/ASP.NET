using System.Diagnostics;
using static System.Console;

namespace WorkingWithTasks;

partial class Program
{
    static void Main(string[] args)
    {
        OutputThreadInfo();
        Stopwatch timer = Stopwatch.StartNew();
        SectionTitle("Passing the result of one task as an input into another.");
        Task<string> taskServiceThenSProc = Task.Factory
          .StartNew(CallWebService) // returns Task<decimal>
          .ContinueWith(previousTask => // returns Task<string>
            CallStoredProcedure(previousTask.Result));
        WriteLine($"Result: {taskServiceThenSProc.Result}");
        WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
    }
}