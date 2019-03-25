using System;
using System.IO;

namespace GeradorDadosOtimizacaoArtigoSBPO
{
    public static class FuncoesGerais
    {
        public static void GenerateSymmetricMatrixNxN(int size, double[,] matrix, double min,
            double max)
        {
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                        matrix[i, j] = 0;
                    else if (i < j)
                        matrix[i, j] = random.NextDouble() * (max - min) + min;
                    else
                        matrix[i, j] = matrix[j, i];
                }
            }
        }

        public static void GenerateSymmetricMatrixNxN(int size, int[,] matrix, int min,
            int max)
        {
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                        matrix[i, j] = 0;
                    else if (i < j)
                        matrix[i, j] = random.Next(min, max);
                    else
                        matrix[i, j] = matrix[j, i];
                }
            }
        }

        public static void GenerateMatrixRowsByColumns(double[,] matrix, double min,
            double max, int rows, int columns, int? controle = null)
        {
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (controle.HasValue && i < controle)
                    {
                        matrix[i, j] = 0;
                    }
                    else
                    {
                        matrix[i, j] = random.NextDouble() * (max - min) + min;
                    }
                }
            }
        }

        public static void GenerateMatrixNxNxN(double[,,] matrix, double min,
            double max, int dim1, int dim2, int dim3, int? controle = null)
        {
            Random random = new Random();
            for (int i = 0; i < dim1; i++)
            {
                for (int j = 0; j < dim2; j++)
                {
                    for (int k = 0; k < dim3; k++)
                    {
                        if (controle.HasValue && i < controle)
                        {
                            matrix[i, j, k] = 0;
                        }
                        else
                        {
                            matrix[i, j, k] = random.NextDouble() * (max - min) + min;
                        }
                    }
                }
            }
        }

        public static void GenerateSymmetricMatrixNxNxN(double[,,] matrix, double min,
            double max, int dim1, int dim2, int dim3)
        {
            Random random = new Random();
            for (int i = 0; i < dim1; i++)
            {
                for (int j = 0; j < dim2; j++)
                {
                    for (int k = 0; k < dim3; k++)
                    {
                        if (j == k)
                        {
                            matrix[i, j, k] = 0;
                        }
                        else if(j < k)
                        {
                            matrix[i, j, k] = random.NextDouble() * (max - min) + min;
                        }
                        else
                        {
                            matrix[i, j, k] = matrix[i, k, j];
                        }
                    }
                }
            }
        }

        public static void GenerateMatrixRowsByColumns(int[,] matrix, int min,
            int max, int rows, int columns, int? controle = null)
        {
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (controle.HasValue && i < controle)
                    {
                        matrix[i, j] = 0;
                    }
                    else
                    {
                        matrix[i, j] = random.Next(min, max);
                    }
                }
            }
        }

        public static void GenerateRandomArray(double[] matrix, double min, double max,
            int size, int? controle = null)
        {
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                if (controle.HasValue && i < controle)
                {
                    matrix[i] = 0;
                }
                else
                {
                    matrix[i] = random.NextDouble() * (max - min) + min;
                }
            }
        }

        public static void GenerateRandomArray(int[] matrix, int min, int max,
            int size, int? controle = null)
        {
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                if (controle.HasValue && i < controle)
                {
                    matrix[i] = 0;
                }
                else
                {
                    matrix[i] = random.Next(min, max);
                }
            }
        }

        public static void WriteMatrixNxNxNToFile(StreamWriter file,
            double[,,] matrix, int dim1, int dim2, int dim3, string arrayName)
        {
            Random random = new Random();
            file.Write($"{arrayName} = [{Environment.NewLine}");
            for (int i = 0; i < dim1; i++)
            {
                file.Write($"[");
                for (int j = 0; j < dim2; j++)
                {
                    file.Write($"[");
                    for (int k = 0; k < dim3; k++)
                    {
                        if (k == 0)
                            file.Write($"{(float)Math.Round(matrix[i, j, k], 1)}");
                        else
                            file.Write($", {(float)Math.Round(matrix[i, j, k], 1)}");
                    }
                    file.Write($"]");
                }
                if (i == (dim1 - 1))
                    file.Write($"]{Environment.NewLine}");
                else
                    file.Write($"],{Environment.NewLine}");
            }
            file.Write($"];{Environment.NewLine}");
        }

        public static void WriteMatrixNxNToFile(StreamWriter file,
            double[,] matrix, int rows, int columns, string arrayName)
        {
            file.Write($"{arrayName} = [{Environment.NewLine}");
            for (int i = 0; i < rows; i++)
            {
                file.Write($"[");
                for (int j = 0; j < columns; j++)
                {
                    if (j == 0)
                        file.Write($"{(float)Math.Round(matrix[i, j], 1)}");
                    else
                        file.Write($", {(float)Math.Round(matrix[i, j], 1)}");
                }
                if (i == (rows - 1))
                    file.Write($"]{Environment.NewLine}");
                else
                    file.Write($"],{Environment.NewLine}");
            }
            file.Write($"];{Environment.NewLine}");
        }

        public static void WriteMatrixNxNToFile(StreamWriter file,
            int[,] matrix, int rows, int columns, string arrayName)
        {
            file.Write($"{arrayName} = [{Environment.NewLine}");
            for (int i = 0; i < rows; i++)
            {
                file.Write($"[");
                for (int j = 0; j < columns; j++)
                {
                    if (j == 0)
                        file.Write($"{matrix[i, j]}");
                    else
                        file.Write($", {matrix[i, j]}");
                }
                if (i == (rows - 1))
                    file.Write($"]{Environment.NewLine}");
                else
                    file.Write($"],{Environment.NewLine}");
            }
            file.Write($"];{Environment.NewLine}");
        }

        public static void WriteArrayToFile(StreamWriter file,
            double[] array, int size, string arrayName)
        {
            file.Write($"{arrayName} = [");
            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                    file.Write($"{(float)Math.Round(array[i], 1)}");
                else
                    file.Write($", {(float)Math.Round(array[i], 1)}");
            }
            file.Write($"];{Environment.NewLine}");
        }

        public static void WriteArrayToFile(StreamWriter file,
            int[] array, int size, string arrayName)
        {
            file.Write($"{arrayName} = [");
            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                    file.Write($"{array[i]}");
                else
                    file.Write($", {array[i]}");
            }
            file.Write($"];{Environment.NewLine}");
        }
    }
}
