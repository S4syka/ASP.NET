using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SynchronizingResourceAccess;

internal partial class Program
{
    private static void MethodA()
    {
        lock (SharedObject.Conch)
        {
            for (int i = 0; i < 5; i++)
            {
                // Simulate two seconds of work on the current thread.
                Thread.Sleep(Random.Shared.Next(2000));
                // Concatenate the letter "A" to the shared message.
                SharedObject.Message += "A";
                // Show some activity in the console output.
                Write(".");
            }
        }
    }
    private static void MethodB()
    {
        lock (SharedObject.Conch)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(Random.Shared.Next(2000));
                SharedObject.Message += "B";
                Write(".");
            }
        }
    }
}
