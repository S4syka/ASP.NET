using System.Diagnostics;
using static System.Console;

namespace WorkingWithTasks;

partial class Program
{
    static void Main(string[] args)
    {
        OutputThreadInfo();
        Stopwatch timer = Stopwatch.StartNew();
        SectionTitle("Running methods synchronously on one thread.");
        MethodA();
        MethodB();
        MethodC();
        WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
    }
}