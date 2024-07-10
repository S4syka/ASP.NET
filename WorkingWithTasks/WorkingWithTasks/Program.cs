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
        WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed. \n \n \n");
        timer.Restart();

        SectionTitle("Runnig methods asynchronously on multiple threads.");
        Task taskA = new(MethodA);
        taskA.Start();
        Task taskB = Task.Factory.StartNew(MethodB);
        Task taskC = Task.Run(MethodC);

        Task.WaitAll(new Task[] { taskA, taskB, taskC });
        WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
    }
}