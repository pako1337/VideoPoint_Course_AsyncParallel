﻿using System;
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
        private static Mutex mutex = new Mutex();

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

                try
                {
                    mutex.WaitOne();
                    toCalculate.Enqueue(item);
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        private static void Multiply()
        {
            while (calculated < matrixCount)
            {
                Matrix m2 = null;

                if (toCalculate.Count > 0)
                {
                    try
                    {
                        mutex.WaitOne();
                        if (toCalculate.Count > 0)
                        {
                            m2 = toCalculate.Dequeue();
                        }
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }

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