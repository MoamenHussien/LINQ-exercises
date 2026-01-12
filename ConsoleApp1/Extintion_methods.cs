using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1322;

public static class Extintion_methods
{
    public static void Print<T>(this IEnumerable<T> info)
    {
        foreach (var item in info)
        {
            Console.WriteLine(item);
        }
    }
}

