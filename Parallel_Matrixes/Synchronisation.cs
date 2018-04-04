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

        public static void MultiplyMatrixes()
        {
            Task.Run(GenerateMatrixesPeriodically);

            while (calculated < matrixCount)
            {
                Multiply();
                PrintStatus();
            }
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
                toCalculate.Enqueue(MatrixGenerator.GenerateMatrix(matrixSize));
                await Task.Delay(100);
            }
        }

        private static void Multiply()
        {
            while (toCalculate.Count == 0)
            { }

            var m2 = toCalculate.Dequeue();
            m1.Multiply(m2);
            Interlocked.Increment(ref calculated);
        }
    }
}
