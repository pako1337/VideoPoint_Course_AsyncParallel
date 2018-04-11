using System;
using System.Collections.Generic;
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

        private static Queue<Matrix> toCalculate = new Queue<Matrix>();
        private static int calculated = 0;
        private static object sync = new object();

        public static void MultiplyMatrixes()
        {
            var generator = Task.Run(GenerateMatrixesPeriodically);
            var multiplicator1 = Task.Run((Action)Multiply);
            var multiplicator2 = Task.Run((Action)Multiply);

            Task.WhenAll(generator, multiplicator1, multiplicator2)
                .Wait();
        }

        private static void PrintStatus()
        {
            Write("Queue size: " + toCalculate.Count.ToString().PadLeft(4));
            Write("\tCalculated: " + calculated.ToString().PadLeft(4));
            WriteLine();
        }

        private static async Task GenerateMatrixesPeriodically()
        {
            for (int i = 0; i < matrixCount; i++)
            {
                lock (sync)
                {
                    toCalculate.Enqueue(MatrixGenerator.GenerateMatrix(matrixSize));
                }
                await Task.Delay(100);
            }
        }

        private static void Multiply()
        {
            while (calculated < matrixCount)
            {
                lock (sync)
                {
                    while (toCalculate.Count == 0)
                    { }

                    var m2 = toCalculate.Dequeue();
                    m1.Multiply(m2);
                    Interlocked.Increment(ref calculated);
                    PrintStatus();
                }
            }
        }
    }
}
