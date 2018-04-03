using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parallel_Matrixes
{
    public class Matrix
    {
        public int[][] Values;
        public int Rows { get; }
        public int Cols { get; }

        public Matrix(int[][] values)
        {
            Values = values;
            Rows = values.Length;
            Cols = values[0].Length;
        }

        public Matrix Multiply(Matrix other)
        {
            var resultMatrix = new int[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                resultMatrix[i] = new int[other.Cols];

                for (int j = 0; j < other.Cols; j++)
                {
                    var result = 0;
                    for (int k = 0; k < Cols; k++)
                    {
                        result += Values[i][k] * other.Values[k][j];
                    }

                    resultMatrix[i][j] = result;
                }
            }

            return new Matrix(resultMatrix);
        }

        public Matrix MultiplyParallelRows(Matrix other)
        {
            var resultMatrix = new int[Rows][];
            Parallel.For(0, Rows, i =>
            {
                resultMatrix[i] = new int[other.Cols];

                for (int j = 0; j < other.Cols; j++)
                {
                    var result = 0;
                    for (int k = 0; k < Cols; k++)
                    {
                        result += Values[i][k] * other.Values[k][j];
                    }

                    resultMatrix[i][j] = result;
                }
            });

            return new Matrix(resultMatrix);
        }

        public Matrix MultiplyParallelRowsCols(Matrix other)
        {
            var resultMatrix = new int[Rows][];
            Parallel.For(0, Rows, i =>
            {
                resultMatrix[i] = new int[other.Cols];

                Parallel.For(0, other.Cols, j =>
                {
                    var result = 0;
                    for (int k = 0; k < Cols; k++)
                    {
                        result += Values[i][k] * other.Values[k][j];
                    }

                    resultMatrix[i][j] = result;
                });
            });

            return new Matrix(resultMatrix);
        }

        public Matrix MultiplyParallelCols(Matrix other)
        {
            var resultMatrix = new int[Rows][];
            for(var i = 0; i < Rows; i++)
            {
                resultMatrix[i] = new int[other.Cols];

                Parallel.For(0, other.Cols, j =>
                {
                    var result = 0;
                    for (int k = 0; k < Cols; k++)
                    {
                        result += Values[i][k] * other.Values[k][j];
                    }

                    resultMatrix[i][j] = result;
                });
            }

            return new Matrix(resultMatrix);
        }

        public Matrix MultiplyParallelRowManual(Matrix other)
        {
            var resultMatrix = new int[Rows][];
            var tasks = new List<Task>(Rows);

            for (int i = 0; i < Rows; i++)
            {
                var counter = i;
                var task = Task.Run(() =>
                {
                    resultMatrix[counter] = new int[other.Cols];

                    for (int j = 0; j < other.Cols; j++)
                    {
                        var result = 0;
                        for (int k = 0; k < Cols; k++)
                        {
                            result += Values[counter][k] * other.Values[k][j];
                        }

                        resultMatrix[counter][j] = result;
                    }
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            return new Matrix(resultMatrix);
        }
    }
}
