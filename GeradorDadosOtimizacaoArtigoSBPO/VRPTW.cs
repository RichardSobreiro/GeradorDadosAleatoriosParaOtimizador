using System.IO;

namespace GeradorDadosOtimizacaoArtigoSBPO
{
    public static class VRPTW
    {
        public static void Execute(string path)
        {
            File.WriteAllText(path, string.Empty);
            var file = new StreamWriter(path);

            int K = 5;
            float Q = 8;
            int nV = 10;

            file.WriteLine($"K = {K};");
            file.WriteLine($"Q = {Q};");
            int nVp = nV + 2;
            file.WriteLine($"nV = {nV};");

            // float d[Ap][Ap] = ...; // Distance between each node including the base
            double[,] d = new double[nVp, nVp];
            FuncoesGerais.GenerateSymmetricMatrixNxN(nVp, d, 3, 20);
            d[0, (nVp - 1)] = 0;
            d[(nVp - 1), 0] = 0;
            for (int i = 0; i < nVp; i++)
                d[i, i] = 0;
            FuncoesGerais.WriteMatrixNxNToFile(file, d, nVp, nVp, "d");

            // float t[Ap][Ap] = ...; // Travelling time between each node including the base
            double[,] t = new double[nVp, nVp];
            FuncoesGerais.GenerateSymmetricMatrixNxN(nVp, t, 3, 20);
            t[0, (nVp - 1)] = 0;
            t[(nVp - 1), 0] = 0;
            for (int i = 0; i < nVp; i++)
                t[i, i] = 0;
            FuncoesGerais.WriteMatrixNxNToFile(file, t, nVp, nVp, "t");

            // float g[Vp] = ...; // Revenue per customer
            double[] g = new double[nVp];
            FuncoesGerais.GenerateRandomArray(g, 5, 10, nVp);
            g[0] = 0;
            g[(nVp - 1)] = 0;
            FuncoesGerais.WriteArrayToFile(file, g, nVp, "g");
            
            // float q[Vp] = ...; // Demand per customer
            double[] q = new double[nVp];
            FuncoesGerais.GenerateRandomArray(q, 5, 10, nVp);
            q[0] = 0;
            q[(nVp - 1)] = 0;
            FuncoesGerais.WriteArrayToFile(file, q, nVp, "q");

            // float s[Vp] = ...; // Service or dwell time
            double[] s = new double[nVp];
            FuncoesGerais.GenerateRandomArray(s, 5, 10, nVp);
            s[0] = 0;
            s[(nVp - 1)] = 0;
            FuncoesGerais.WriteArrayToFile(file, s, nVp, "s");

            // float a[Vp] = ...; // Earliest time to begin service at customer
            // float b[Vp] = ...; // Latest time to begin service at customer
            double[] a = new double[nVp];
            double[] b = new double[nVp];
            FuncoesGerais.GenerateRandomArray(a, 1, 22, nVp);
            a[0] = 0;
            a[(nVp - 1)] = 0;
            for(int i = 1; i < (nVp-1); i++)
            {
                b[i] = a[i] + 0.25;
            }
            FuncoesGerais.WriteArrayToFile(file, a, nVp, "a");
            FuncoesGerais.WriteArrayToFile(file, b, nVp, "b");

            // int mR = ...; // Maximal number of routes that the fleet can perform in a day
            int mR = K * nV;
            file.WriteLine($"mR = {mR};");

            // float mK = ...; // Number of workdays part of the solution 
            int mK = 1;
            file.WriteLine($"mK = {mK};");

            // float alpha = ...; // Wheight for total revenue
            int alpha = 1;
            file.WriteLine($"alpha = {alpha};");

            // float tmax = ...; // Maximal duration of a route
            int tmax = 10;
            file.WriteLine($"tmax = {tmax};");

            // float beta = ...; // Multiplier setup time for each route
            float beta = 0.2f;
            file.WriteLine($"beta = {beta};");

            // float M = ...;
            float M = 1000;
            file.WriteLine($"M = {M};");

            file.Close();
        }
    }
}
