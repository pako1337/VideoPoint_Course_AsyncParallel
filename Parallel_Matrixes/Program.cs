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
            processingTimes["Parallel Rows"] = Measure(() => m1.MultiplyParallelRows(m2));
            processingTimes["Parallel RowsCols"] = Measure(() => m1.MultiplyParallelRowsCols(m2));
            processingTimes["Parallel Cols"] = Measure(() => m1.MultiplyParallelCols(m2));

            PrintSpeedup(baseTime, processingTimes);
        }

        private static void PrintSpeedup(TimeSpan baseTime, Dictionary<string, TimeSpan> processingTimes)
        {
            Write("Czas bazowy".PadRight(15));
            WriteLine(baseTime);

            Write("Rodzaj obliczeń".PadRight(30));
            Write("Czas trawania".PadRight(20));
            WriteLine("Przyspieszenie");
            foreach (var actionWithMeasure in processingTimes)
            {
                double speedupScale = baseTime.TotalMilliseconds / actionWithMeasure.Value.TotalMilliseconds;
                Write(actionWithMeasure.Key.PadRight(30));
                Write(actionWithMeasure.Value.ToString().PadRight(20));
                WriteLine(speedupScale);
            }
        }
    }
}
