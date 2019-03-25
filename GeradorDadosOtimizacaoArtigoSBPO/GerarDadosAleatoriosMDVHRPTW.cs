using System;
using System.IO;

namespace GeradorDadosOtimizacaoArtigoSBPO
{
    public static class GerarDadosAleatoriosMDVHRPTW
    {
        public static void Execute(string path, int qNos, int qClusters, int qPontosCarga,
            int qBetoneiras)
        {
            File.WriteAllText(path, string.Empty);
            var file = new StreamWriter(path);

            file.Write($"qNos = {qNos};{Environment.NewLine}");
            file.Write($"qClusters = {qClusters};{Environment.NewLine}");
            file.Write($"qPontosCarga = {qPontosCarga};{Environment.NewLine}");
            file.Write($"qBetoneiras = {qBetoneiras};{Environment.NewLine}");

            file.Write($"Mc = {1440};{Environment.NewLine}");
            file.Write($"Mt = {1440};{Environment.NewLine}");
            file.Write($"M = {1440};{Environment.NewLine}");

            double[] rhoi = new double[qNos];
            FuncoesGerais.GenerateRandomArray(rhoi, 10, 20, qNos);
            FuncoesGerais.WriteArrayToFile(file, rhoi, qNos, "rhoi");

            double[] rhov = new double[qBetoneiras];
            FuncoesGerais.GenerateRandomArray(rhov, 50, 50, qBetoneiras);
            FuncoesGerais.WriteArrayToFile(file, rhov, qBetoneiras, "rhov");

            double[,,] c = new double[qBetoneiras, qNos, qNos];
            FuncoesGerais.GenerateSymmetricMatrixNxNxN(c, 100, 200, qBetoneiras, qNos, qNos);
            FuncoesGerais.WriteMatrixNxNxNToFile(file, c, qBetoneiras, qNos, qNos, "c");

            double[] cf = new double[qBetoneiras];
            FuncoesGerais.GenerateRandomArray(cf, 40, 40, qBetoneiras);
            FuncoesGerais.WriteArrayToFile(file, cf, qBetoneiras, "cf");

            double dMax = 25;
            file.WriteLine($"dMax = {dMax};{Environment.NewLine}");
            
            double[] q = new double[qBetoneiras];
            FuncoesGerais.GenerateRandomArray(q, 8, 8, qBetoneiras);
            FuncoesGerais.WriteArrayToFile(file, q, qBetoneiras, "q");

            double[,] st = new double[qNos, qBetoneiras];
            FuncoesGerais.GenerateMatrixRowsByColumns(st, 420, 1440, qNos, qBetoneiras);
            double[] a = new double[qNos];
            FuncoesGerais.GenerateRandomArray(a, 0, 0, qNos);
            double[] b = new double[qNos];
            FuncoesGerais.GenerateRandomArray(b, 0, 0, qNos);
            for(var i = 0; i < qNos; i++)
            {
                var aux = st[i, 0];
                for(var v = 0; v < qBetoneiras; v++)
                {
                    st[i, v] = aux;
                    a[i] = aux - 10;
                    b[i] = aux + 10;
                }
            }
            FuncoesGerais.WriteMatrixNxNToFile(file, st, qNos, qBetoneiras, "st");
            FuncoesGerais.WriteArrayToFile(file, a, qNos, "a");
            FuncoesGerais.WriteArrayToFile(file, b, qNos, "b");

            double[,,] t = new double[qBetoneiras, qNos, qNos];
            FuncoesGerais.GenerateSymmetricMatrixNxNxN(t, 20, 180, qBetoneiras, qNos, qNos);
            FuncoesGerais.WriteMatrixNxNxNToFile(file, t, qBetoneiras, qNos, qNos, "t");

            double[] tvMax = new double[qBetoneiras];
            FuncoesGerais.GenerateRandomArray(tvMax, 480, 480, qBetoneiras);
            FuncoesGerais.WriteArrayToFile(file, tvMax, qBetoneiras, "tvMax");

            double[] w = new double[qNos];
            FuncoesGerais.GenerateRandomArray(w, 2, 8, qNos);
            FuncoesGerais.WriteArrayToFile(file, w, qBetoneiras, "w");

            double ctr = 10;
            file.WriteLine($"ctr = {ctr};{Environment.NewLine}");

            file.Close();
        }
    }
}
