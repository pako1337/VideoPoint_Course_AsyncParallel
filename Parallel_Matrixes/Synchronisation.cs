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
        private static Semaphore semaphore = new Semaphore(0, matrixCount);

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
            WriteLine(
                "Queue size: " + toCalculate.Count.ToString().PadLeft(4)
                + "\tCalculated: " + calculated.ToString().PadLeft(4));
        }

        private static async Task GenerateMatrixesPeriodically()
        {
            for (int i = 0; i < matrixCount; i++)
            {
                Matrix item = MatrixGenerator.GenerateMatrix(matrixSize);

                toCalculate.Enqueue(item);
                semaphore.Release();
            }
        }

        private static void Multiply()
        {
            while (calculated < matrixCount)
            {
                Matrix m2 = null;

                if (semaphore.WaitOne(100))
                {
                    m2 = toCalculate.Dequeue();

                    if (m2 != null)
                    {
                        m1.Multiply(m2);
                        Interlocked.Increment(ref calculated);
                        PrintStatus();
                    }
                }
            }
        }
    }
}
