using System.Diagnostics;
using static System.Console;

namespace SynchronizingResourceAccess;

internal partial class Program
{
    static void Main(string[] args)
    {
        WriteLine("Please wait for the tasks to complete.");
        Stopwatch watch = Stopwatch.StartNew();
        Task a = Task.Factory.StartNew(MethodA);
        Task b = Task.Factory.StartNew(MethodB);

        Task.WaitAll(new Task[] { a, b });
        WriteLine();
        WriteLine($"Results: {SharedObject.Message}.");
        WriteLine($"{watch.ElapsedMilliseconds:N0} elapsed milliseconds.");
    }
}
