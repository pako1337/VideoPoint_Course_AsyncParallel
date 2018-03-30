using System;

namespace Parallel_Matrixes
{
    internal static class MatrixGenerator
    {
        private static Random rnd = new Random(1);

        public static Matrix GenerateMatrix(int matrixSize)
        {
            return new Matrix(GenerateMatrixValues(matrixSize));
        }

        private static int[][] GenerateMatrixValues(int matrixSize)
        {
            var values = new int[matrixSize][];

            for (int i = 0; i < matrixSize; i++)
            {
                values[i] = new int[matrixSize];
                for (int j = 0; j < matrixSize; j++)
                    values[i][j] = rnd.Next(1_000);
            }

            return values;
        }
    }
}
