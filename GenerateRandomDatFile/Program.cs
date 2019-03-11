using System;
using System.IO;

namespace GenerateRandomDatFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string testPath = "C:\\Users\\richa\\Google Drive\\Mestrado\\Artigo SBPO\\ProjetosCeplexStudio\\VehicleRoutingProject\\DataCeplex.txt";
            string path = "C:\\Users\\richa\\opl\\ArtigoSBPO201904\\MDVHRTWSDR.dat";

            CriarMDVHRTWSDRDAT(path);
        }

        static void CriarMDVHRTWSDRDAT(string path)
        {
            int M = 1440;
            int qC = 5;
            int qD = 1;
            int qB = 6;
            int qCD = qC + qD;
            double minq = 1, maxq = 3;
            double[] q = new double[qCD];
            int mins = 400, maxs = 1300;
            int[] s = new int[qCD];
            int diffel = 10;
            int[] e = new int[qCD];
            int[] l = new int[qCD];
            double minDist = 5, maxDist = 30;
            double[,] c = new double[qCD, qCD];
            double[] Q = new double[qB];
            int[] T = new int[qB];
            int[] Pd = new int[qD];
            int[] Kd = new int[qD];

            CriarParametros(qC, qD, qB, qCD,
            minq, maxq, q,
            mins, maxs, s,
            diffel, e, l,
            qCD, minDist, maxDist, c,
            Q,
            T,
            Pd,
            Kd);

            File.WriteAllText(path, string.Empty);
            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine($"M = {M};");
                file.WriteLine($"qC = {qC};");
                file.WriteLine($"qD = {qD};");
                file.WriteLine($"qB = {qB};");
                WriteArrayDatFile(file, q, qCD, "q");
                WriteArrayDatFile(file, s, qCD, "s");
                WriteArrayDatFile(file, e, qCD, "e");
                WriteArrayDatFile(file, l, qCD, "l");
                WriteMatrixDim2DatFile(file, c, qCD, "c");
                WriteArrayDatFile(file, Q, qB, "Q");
                WriteArrayDatFile(file, T, qB, "T");
                WriteArrayDatFile(file, Pd, qD, "Pd");
                WriteArrayDatFile(file, Kd, qD, "Kd");
                file.Flush();
            }
        }

        static void CriarParametros(int qC, int qD, int qB, int qCD, 
            double minq, double maxq, double[] q,
            int mins, int maxs, int[] s,
            int diffel, int[] e, int[]l,
            int m, double minDist, double maxDist, double[,] c,
            double[] Q,
            int[] T,
            int[] Pd,
            int[] Kd)
        {
            Random random = new Random();
            for (int i = 0; i < qCD; i++)
            {
                if (i < qD)
                {
                    q[i] = 0;
                    s[i] = 0;
                    e[i] = 0;
                    l[i] = 0;
                }
                else
                {
                    q[i] = random.NextDouble() * (maxq - minq) + minq;
                    s[i] = random.Next(0, (maxs-mins)) + mins;
                    e[i] = s[i] - diffel;
                    l[i] = s[i] + diffel;
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == j)
                        c[i, j] = 0;
                    else if (i < j)
                        c[i, j] = random.NextDouble() * (maxDist - minDist) + minDist;
                    else
                        c[i, j] = c[j, i];
                }
            }

            for (int i = 0; i < qB; i++)
            {
                Q[i] = 4 * maxq;
                T[i] = 6 * (int)maxDist;
            }

            for (int i = 0; i < qD; i++)
            {
                if(qB == 1)
                {
                    Pd[i] = qB;
                    Kd[i] = qB;
                }
                else if((qB % 2) == 0)
                {
                    Pd[i] = (qB / 2);
                    Kd[i] = (qB / 2);
                }
                else
                {
                    Pd[i] = qB;
                    Kd[i] = qB;
                }
            }
        }

        static void WriteArrayDatFile(StreamWriter file, double[] array, int size, string arrayName)
        {
            file.Write($"{arrayName} = [");
            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                    file.Write($"{(float)array[i]}");
                else
                    file.Write($", {(float)array[i]}");
            }
            file.Write($"];{Environment.NewLine}");
        }

        static void WriteArrayDatFile(StreamWriter file, int[] array, int size, string arrayName)
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

        static void WriteMatrixDim2DatFile(StreamWriter file, double[,] array, int size, string arrayName)
        {
            file.Write($"{arrayName} = [{Environment.NewLine}");
            for(int i = 0; i < size; i++)
            {
                if(i == 0)
                    file.Write($"[");
                else
                    file.Write($",{Environment.NewLine}[");
                for (int j = 0; j < size; j++)
                {
                    if (j == 0)
                        file.Write($"{(float)array[i, j]}");
                    else
                        file.Write($", {(float)array[i, j]}");
                }
                file.Write($"]");
            }
            file.Write($"{Environment.NewLine}];{Environment.NewLine}");
        }
    }
}
