using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncEnumerable;

internal partial class Program
{
    private static async IAsyncEnumerable<int> GetNumbersAsync()
    {
        Random r = new Random();
        
        await Task.Delay(r.Next(1500, 3000));
        yield return r.Next(1, 1001);

        await Task.Delay(r.Next(1500, 3000));
        yield return r.Next(1, 1001);
        
        await Task.Delay(r.Next(1500, 3000));
        yield return r.Next(1, 1001);
    }
}
