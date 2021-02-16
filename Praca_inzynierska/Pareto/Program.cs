using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pareto
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<(double, int)> points = new HashSet<(double, int)>();

            using (var file = new StreamReader("input"))
            {
                while (!file.EndOfStream)
                {
                    var inputs = file.ReadLine().Split(" ");
                    double x = Convert.ToDouble(inputs[0]);
                    int y = Convert.ToInt32(inputs[1]);

                    points.Add((x, y));
                }
            }

            Console.WriteLine(points.Count);

            var result = new List<(double, int)>();

            foreach (var (x, y) in points)
            {
                if (!points.Any(p => p.Item1 <= x && p.Item2 <= y && (p.Item1 < x || p.Item2 < y)))
                {
                    result.Add((x, y));
                    Console.WriteLine($"{x};{y}");
                }
            }

            Console.WriteLine(result.Count);
        }
    }
}
