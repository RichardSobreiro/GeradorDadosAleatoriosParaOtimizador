using System.IO;

namespace GeradorDadosOtimizacaoArtigoSBPO
{
    public static class MDVRPTWMultiTrips
    {
        public static void Execute(string path)
        {
            File.WriteAllText(path, string.Empty);
            var file = new StreamWriter(path);

            float inf = 100000000;

            int nD = 3;
            file.WriteLine($"nD = {nD};");

            int nV = 3;
            file.WriteLine($"nV = {nV};");

            int nC = 10;
            file.WriteLine($"nC = {nC};");

            int nN = nC + (2 * nD);

            double[,] c = new double[nN, nN];
            FuncoesGerais.GenerateSymmetricMatrixNxN(nN, c, 100, 200);
            for(int r = 0; r < nD; r++)
            {
                for (int i = 0; i < nN; i++)
                {
                    c[nN - r - 1, i] = c[r, i];
                    c[i, r] = c[r, i];
                    c[i, nN - r - 1] = c[i, r];
                    c[r, nN - r - 1] = 0;
                    c[nN - r - 1, r] = 0;
                }
            }
            for (int i = 0; i < nN; i++)
                c[i, i] = inf;
            FuncoesGerais.WriteMatrixNxNToFile(file, c, nN, nN, "c");

            double[,] t = new double[nN, nN];
            FuncoesGerais.GenerateSymmetricMatrixNxN(nN, t, 20, 60);
            for (int r = 0; r < nD; r++)
            {
                for (int i = 0; i < nN; i++)
                {
                    t[nN - r - 1, i] = t[r, i];
                    t[i, r] = t[r, i];
                    t[i, nN - r - 1] = t[i, r];
                    t[r, nN - r - 1] = 0;
                    t[nN - r - 1, r] = 0;
                }
            }
            for (int i = 0; i < nN; i++)
                t[i, i] = inf;
            FuncoesGerais.WriteMatrixNxNToFile(file, t, nN, nN, "t");

            float q = 10;
            file.WriteLine($"q = {q};");

            double[] tc = new double[nV];
            FuncoesGerais.GenerateRandomArray(tc, 30, 30, nV);
            FuncoesGerais.WriteArrayToFile(file, tc, nV, "tc");

            double[] d = new double[nN];
            FuncoesGerais.GenerateRandomArray(d, 4, 10, nN);
            for (int r = 0; r < nD; r++)
            {

                d[r] = 0;
                d[nN - r - 1] = 0;
            }
            FuncoesGerais.WriteArrayToFile(file, d, nN, "d");

            double[] p = new double[nN];
            FuncoesGerais.GenerateRandomArray(p, 520, 660, nN);
            for (int r = 0; r < nD; r++)
            {

                p[r] = 0;
                p[nN - r - 1] = 0;
            }
            FuncoesGerais.WriteArrayToFile(file, p, nN, "p");

            double[] a = new double[nN];
            FuncoesGerais.GenerateRandomArray(a, 420, 1140, nN);
            for (int r = 0; r < nD; r++)
            {

                a[r] = 0;
                a[nN - r - 1] = 0;
            }
            FuncoesGerais.WriteArrayToFile(file, a, nN, "a");

            double[] b = new double[nN];
            for (int i = 0; i < nN; i++)
            {
                b[i] = a[i] + 15;
            }
            for (int r = 0; r < nD; r++)
            {

                b[r] = 1455;
                b[nN - r - 1] = 1455;
            }
            FuncoesGerais.WriteArrayToFile(file, b, nN, "b");

            int[] sd = new int[nV];
            FuncoesGerais.GenerateRandomArray(sd, 1, (nD + 1), nV);
            FuncoesGerais.WriteArrayToFile(file, sd, nV, "sd");

            float M = 2000;
            file.WriteLine($"M = {M};");

            file.Close();
        }
    }
}
