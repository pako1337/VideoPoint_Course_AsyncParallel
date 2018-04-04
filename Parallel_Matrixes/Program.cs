using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;
using static Parallel_Matrixes.Measurements;

namespace Parallel_Matrixes
{
    internal static class Program
    {
        private static int matrixSize = 500;

        private static void Main()
        {
            //TwoMatrixesMultiplication();

            //ListOfMatrixesMultiplication();

            Synchronisation.MultiplyMatrixes();
        }

        private static void ListOfMatrixesMultiplication()
        {
            var processingTimes = new Dictionary<string, TimeSpan>();
            var m1 = MatrixGenerator.GenerateMatrix(matrixSize);
            var matrixes = Enumerable.Range(0, 20)
                .Select(_ => MatrixGenerator.GenerateMatrix(matrixSize))
                .ToList();

            var baseTime = Measure(() => MultiplyAll(m1, matrixes));

            processingTimes["Parallel Foreach"] = Measure(() => MultiplyAllParallel(m1, matrixes));
            processingTimes["Plinq"] = Measure(() => MultiplyAllPlinq(m1, matrixes));

            PrintSpeedup(baseTime, processingTimes);
        }

        private static void MultiplyAll(Matrix m1, List<Matrix> matrixes)
        {
            foreach (var m2 in matrixes)
            {
                m1.Multiply(m2);
            }
        }

        private static void MultiplyAllParallel(Matrix m1, List<Matrix> matrixes)
        {
            Parallel.ForEach(matrixes, m2 =>
            {
                m1.Multiply(m2);
            });
        }

        private static void MultiplyAllPlinq(Matrix m1, List<Matrix> matrixes)
        {
            matrixes.AsParallel()
                .Select(m2 => m1.Multiply(m2))
                .ToList();
        }

        private static void TwoMatrixesMultiplication()
        {
            var processingTimes = new Dictionary<string, TimeSpan>();
            var m1 = MatrixGenerator.GenerateMatrix(matrixSize);
            var m2 = MatrixGenerator.GenerateMatrix(matrixSize);

            var baseTime = Measure(() => m1.Multiply(m2));
            processingTimes["Parallel Rows"] = Measure(() => m1.MultiplyParallelRows(m2));
            processingTimes["Parallel RowsCols"] = Measure(() => m1.MultiplyParallelRowsCols(m2));
            processingTimes["Parallel Cols"] = Measure(() => m1.MultiplyParallelCols(m2));
            processingTimes["Prallel Rows Manual"] = Measure(() => m1.MultiplyParallelRowManual(m2));
            processingTimes["Prallel Rows Thread Pool"] = Measure(() => m1.MultiplyParallelRowManualThreadPool(m2));

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
