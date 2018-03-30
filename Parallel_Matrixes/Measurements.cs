using System;
using System.Diagnostics;

namespace Parallel_Matrixes
{
    public static class Measurements
    {
        public static TimeSpan Measure(Action action)
        {
            var watch = new Stopwatch();
            watch.Start();

            action();

            watch.Stop();

            return watch.Elapsed;
        }


    }
}
