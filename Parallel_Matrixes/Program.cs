using System;
using System.Collections.Generic;
using static System.Console;
using static Parallel_Matrixes.Measurements;

namespace Parallel_Matrixes
{
    internal static class Program
    {
        private static int matrixSize = 500;
        private static Dictionary<string, TimeSpan> processingTimes = new Dictionary<string, TimeSpan>();

        private static void Main()
        {
            var m1 = MatrixGenerator.GenerateMatrix(matrixSize);
            var m2 = MatrixGenerator.GenerateMatrix(matrixSize);

            var baseTime = Measure(() => m1.Multiply(m2));

            PrintSpeedup(baseTime, processingTimes);
        }

        private static void PrintSpeedup(TimeSpan baseTime, Dictionary<string, TimeSpan> processingTimes)
        {
            WriteLine("Rodzaj obliczeń\t\t\tCzas trawania\t\t\tPrzyspieszenie");
            foreach (var actionWithMeasure in processingTimes)
            {
                double speedupScale = baseTime.TotalMilliseconds / actionWithMeasure.Value.TotalMilliseconds;
                WriteLine($"{actionWithMeasure.Key}\t\t{actionWithMeasure.Value}\t\t{speedupScale}");
            }
        }
    }
}
