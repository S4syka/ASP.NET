using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizingResourceAccess;

public static class SharedObject
{
    public static object Conch = new(); // A shared object to lock.
    public static string? Message;
}