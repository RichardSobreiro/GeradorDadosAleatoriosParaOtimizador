using System.IO;

namespace GeradorDadosOtimizacaoArtigoSBPO
{
    public static class VRPTWClassical
    {
        public static void Execute(string path)
        {
            File.WriteAllText(path, string.Empty);
            var file = new StreamWriter(path);

            float inf = 100000000;

            int nV = 1;
            file.WriteLine($"nV = {nV};");

            int nC = 4;
            file.WriteLine($"nC = {nC};");

            int nN = nC + 2;
            int nA = nC + 2;

            double[,] c = new double[nN, nN];
            FuncoesGerais.GenerateSymmetricMatrixNxN(nN, c, 100, 200);
            for (int i = 0; i < nN; i++)
                c[i, i] = inf;
            for (int i = 0; i < nN; i++)
            {
                c[(nN - 1), i] = c[0, i];
                c[i, 0] = c[0, i];
                c[i, (nN - 1)] = c[i, 0];
            }
            c[0, (nN - 1)] = 0;
            c[(nN - 1), 0] = 0;
            FuncoesGerais.WriteMatrixNxNToFile(file, c, nN, nN, "c");

            double[,] t = new double[nN, nN];
            FuncoesGerais.GenerateSymmetricMatrixNxN(nN, t, 20, 60);
            for (int i = 0; i < nN; i++)
                t[i, i] = inf;
            for (int i = 0; i < nN; i++)
            {
                t[(nN - 1), i] = t[0, i];
                t[i, 0] = t[0, i];
                t[i, (nN - 1)] = t[i, 0];
            }
            t[0, (nN - 1)] = 0;
            t[(nN - 1), 0] = 0;
            FuncoesGerais.WriteMatrixNxNToFile(file, t, nN, nN, "t");

            float q = 10;
            file.WriteLine($"q = {q};");

            double[] d = new double[nN];
            FuncoesGerais.GenerateRandomArray(d, 1, 2, nN);
            d[0] = 0;
            d[(nN - 1)] = 0;
            FuncoesGerais.WriteArrayToFile(file, d, nN, "d");

            double[] a = new double[nN];
            FuncoesGerais.GenerateRandomArray(a, 420, 1140, nN);
            a[0] = 0;
            a[(nN - 1)] = 0;
            FuncoesGerais.WriteArrayToFile(file, a, nN, "a");

            double[] b = new double[nN];
            for (int i = 0; i < nN; i++)
            {
                b[i] = a[i] + 15;
            }
            b[0] = 1440;
            b[(nN - 1)] = 1455;
            FuncoesGerais.WriteArrayToFile(file, b, nN, "b");

            float M = 2000;
            file.WriteLine($"M = {M};");

            file.Close();
        }
    }
}
