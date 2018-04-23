using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Parallel_Matrixes
{
    public static class Synchronisation
    {
        private const int matrixCount = 50;
        private const int matrixSize = 100;
        private static readonly Matrix m1 = MatrixGenerator.GenerateMatrix(matrixSize);

        private static BlockingCollection<Matrix> toCalculate = new BlockingCollection<Matrix>(new ConcurrentQueue<Matrix>());
        private static ConcurrentDictionary<long, int> timeHistogram = new ConcurrentDictionary<long, int>();
        private static ConcurrentBag<Matrix> secondLevel = new ConcurrentBag<Matrix>();
        private static int calculated = 0;
        private static int secondCalculated = 0;

        public static void MultiplyMatrixes()
        {
            var generator = Task.Run(GenerateMatrixesPeriodically);
            var multiplicator1 = Task.Run((Action)Multiply);
            var multiplicator2 = Task.Run((Action)Multiply);

            Task.WhenAll(generator, multiplicator1, multiplicator2)
                .Wait();

            foreach (var timeValue in timeHistogram.OrderBy(t => t.Key))
            {
                WriteLine($"Time: {timeValue.Key} \tValue {timeValue.Value}");
            }

            WriteLine("Value count: " + timeHistogram.Values.Sum());
        }

        private static void PrintStatus(int resultedCount, long timeElapsed)
        {
            WriteLine(
                "Queue size: " + toCalculate.Count.ToString().PadLeft(4)
                + "\tCalculated: " + resultedCount.ToString().PadLeft(4)
                + "\tTime: " + timeElapsed.ToString().PadLeft(4));
        }

        private static async Task GenerateMatrixesPeriodically()
        {
            for (int i = 0; i < matrixCount; i++)
            {
                Matrix item = MatrixGenerator.GenerateMatrix(matrixSize);

                toCalculate.TryAdd(item);
            }
        }

        private static void Multiply()
        {
            while (calculated < matrixCount)
            {
                Matrix m2 = null;

                if (toCalculate.TryTake(out m2, 300))
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    var result = m1.Multiply(m2);
                    watch.Stop();
                    var resultedCount = Interlocked.Increment(ref calculated);
                    secondLevel.Add(result);

                    timeHistogram.AddOrUpdate(watch.ElapsedMilliseconds, 1, (k, v) => v + 1);

                    PrintStatus(resultedCount, watch.ElapsedMilliseconds);
                }
                else
                {
                    WriteLine("Failed to dequeue item");
                }
            }

            while (secondCalculated < matrixCount)
            {
                Matrix m2 = null;
                if (secondLevel.TryTake(out m2))
                {
                    m1.Multiply(m2);
                    var calculatedCount = Interlocked.Increment(ref secondCalculated);
                    WriteLine("Second level calculated: " + calculatedCount);
                }
            }
        }
    }
}
