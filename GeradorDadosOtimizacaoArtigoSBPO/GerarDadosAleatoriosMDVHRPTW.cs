using System;
using System.IO;

namespace GeradorDadosOtimizacaoArtigoSBPO
{
    public static class GerarDadosAleatoriosMDVHRPTW
    {
        public static void Execute(string path, int qC, int qP, int qB, int qVg)
        {
            int V = qC + qP;
            double[,] cr = new double[V, V]; // Custo rodoviario
            double[,] cac = new double[V, qP]; // Custo de atendimento do client j pelo ponto de carga p
            double[] cuc = new double[qB]; // Custo de uso do caminhao k
            int[,] ti = new int[V, V]; // Tempo trajeto ida entre i e j
            int[,] tv = new int[V, V]; // Tempo trajeto volta entre i e j
            int[] tp = new int[qP]; // Tempo de pesagem em cada ponto de carga p
            int[] td = new int[V]; // Tempo de descarga no cliente c
            int[,] hc = new int[V, qVg]; // Hora de chegada para cada viagem do cliente
            double[,] vc = new double[V, qVg]; // Volume de material solicitado pelo cliente em cada viagem 
            int[] qv = new int[V]; // Quantidade de viagens para cada cliente
            int[] kp = new int[qP]; // Quantidade veiculos disponiveis por central

            GenerateRandomData(V, qP, qB, qVg, cr, cac, cuc, ti, tv, tp, td, hc, vc,
                qv, kp);

            WriteValuesToDatFile(V, qC, qP, qB, qVg, cr, cac, cuc, ti, tv, tp, td, hc, vc,
                qv, kp, path);
        }

        static void GenerateRandomData(int V, int qP, int qB, int qVg, 
            double[,] cr, double[,] cac, double[] cuc,
            int[,] ti, int[,] tv, int[] tp, int[] td, int[,] hc, double[,] vc, 
            int[] qv, int[]kp)
        {
            GenerateSymmetricMatrixNxN(V, cr, 10, 40);
            GenerateMatrixRowsByColumns(cac, 200, 400, V, qP, qP);
            GenerateRandomArray(cuc, 10, 20, qB);
            GenerateSymmetricMatrixNxN(V, ti, 10, 80);
            GenerateSymmetricMatrixNxN(V, tv, 10, 80);
            GenerateRandomArray(tp, 7, 7, qP);
            GenerateRandomArray(td, 15, 30, V, qP);
            GenerateMatrixRowsByColumns(hc, 400, 1200, V, qVg, qP);
            GenerateMatrixRowsByColumns(vc, 8, 8, V, qVg, qP);
            GenerateRandomArray(qv, qVg, qVg, V, qP);
            GenerateRandomArray(kp, 10, 10, qP);
        }

        static void WriteValuesToDatFile(int V, int qC, int qP, int qB, int qVg,
            double[,] cr, double[,] cac, double[] cuc,
            int[,] ti, int[,] tv, int[] tp, int[] td, int[,] hc, double[,] vc,
            int[] qv, int[] kp, string path)
        {
            File.WriteAllText(path, string.Empty);
            using (var file = new StreamWriter(path))
            {
                file.WriteLine($"M = {20000};");
                file.WriteLine($"qC = {qC};");
                file.WriteLine($"qP = {qP};");
                file.WriteLine($"qB = {qB};");
                file.WriteLine($"qVg = {qVg};");
                WriteMatrixNxNToFile(file, cr, V, V, "cr");
                WriteMatrixNxNToFile(file, cac, V, qP, "cac");
                WriteArrayToFile(file, cuc, qB, "cuc");
                WriteMatrixNxNToFile(file, ti, V, V, "ti");
                WriteMatrixNxNToFile(file, tv, V, V, "tv");
                WriteArrayToFile(file, tp, qP, "tp");
                WriteArrayToFile(file, td, V, "td");
                WriteMatrixNxNToFile(file, hc, V, qVg, "hc");
                WriteMatrixNxNToFile(file, vc, V, qVg, "vc");
                WriteArrayToFile(file, qv, V, "qv");
                WriteArrayToFile(file, kp, qP, "kp");
            }
        }

        static void GenerateSymmetricMatrixNxN(int size, double[,] matrix, double min,
            double max)
        {
            Random random = new Random();
            for(int i = 0; i < size; i++)
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

        static void GenerateSymmetricMatrixNxN(int size, int[,] matrix, int min,
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

        static void GenerateMatrixRowsByColumns(double[,] matrix, double min,
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

        static void GenerateMatrixRowsByColumns(int[,] matrix, int min,
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

        static void GenerateRandomArray(double[] matrix, double min, double max,
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

        static void GenerateRandomArray(int[] matrix, int min, int max,
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

        static void WriteMatrixNxNToFile(StreamWriter file, 
            double[,] matrix, int rows, int columns, string arrayName)
        {
            Random random = new Random();
            file.Write($"{arrayName} = [{Environment.NewLine}");
            for (int i = 0; i < rows; i++)
            {
                file.Write($"[");
                for (int j = 0; j < columns; j++)
                {
                    if(j == 0)
                        file.Write($"{matrix[i,j]}");
                    else
                        file.Write($", {matrix[i, j]}");
                }
                if(i == (rows - 1))
                    file.Write($"]{Environment.NewLine}");
                else
                    file.Write($"],{Environment.NewLine}");
            }
            file.Write($"];{Environment.NewLine}");
        }

        static void WriteMatrixNxNToFile(StreamWriter file,
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

        static void WriteArrayToFile(StreamWriter file,
            double[] array, int size, string arrayName)
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

        static void WriteArrayToFile(StreamWriter file,
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
