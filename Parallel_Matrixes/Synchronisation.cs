using System;
using System.Collections.Concurrent;
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

        private static ConcurrentQueue<Matrix> toCalculate = new ConcurrentQueue<Matrix>();
        private static int calculated = 0;

        public static void MultiplyMatrixes()
        {
            var generator = Task.Run(GenerateMatrixesPeriodically);
            var multiplicator1 = Task.Run((Action)Multiply);
            var multiplicator2 = Task.Run((Action)Multiply);

            Task.WhenAll(generator, multiplicator1, multiplicator2)
                .Wait();
        }

        private static void PrintStatus(int resultedCount)
        {
            WriteLine(
                "Queue size: " + toCalculate.Count.ToString().PadLeft(4)
                + "\tCalculated: " + resultedCount.ToString().PadLeft(4));
        }

        private static async Task GenerateMatrixesPeriodically()
        {
            for (int i = 0; i < matrixCount; i++)
            {
                Matrix item = MatrixGenerator.GenerateMatrix(matrixSize);

                toCalculate.Enqueue(item);
            }
        }

        private static void Multiply()
        {
            while (calculated < matrixCount)
            {
                Matrix m2 = null;

                if (toCalculate.TryDequeue(out m2))
                {
                    m1.Multiply(m2);
                    var resultedCount = Interlocked.Increment(ref calculated);
                    PrintStatus(resultedCount);
                }
            }
        }
    }
}
