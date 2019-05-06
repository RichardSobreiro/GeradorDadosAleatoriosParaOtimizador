using System.IO;

namespace GeradorDadosOtimizacaoArtigoSBPO
{
    public static class GerarDadosProgramacaoProducaoVeiculos
    {
        public static void Execute(string path, int qViagens, int qPontosCarga,
            int qBetoneiras, int M)
        {
            File.WriteAllText(path, string.Empty);
            var file = new StreamWriter(path);

            file.WriteLine($"qViagens = {qViagens};");
            file.WriteLine($"qPontosCarga = {qPontosCarga};");
            file.WriteLine($"qBetoneiras = {qBetoneiras};");
            file.WriteLine($"M = {M};");

            double[,] dp = new double[qViagens, qPontosCarga];
            FuncoesGerais.GenerateMatrixRowsByColumns(dp, 7, 7, qViagens, qPontosCarga);
            for (int i = 0; i < qPontosCarga; i++)
                dp[0, i] = 0;
            FuncoesGerais.WriteMatrixNxNToFile(file, dp, qViagens, qPontosCarga, "dp");

            double[,] dv = new double[qViagens, qPontosCarga];
            FuncoesGerais.GenerateMatrixRowsByColumns(dv, 10, 50, qViagens, qPontosCarga);
            for (int i = 0; i < qPontosCarga; i++)
                dv[0, i] = 0;
            FuncoesGerais.WriteMatrixNxNToFile(file, dv, qViagens, qPontosCarga, "dv");

            double[] td = new double[qViagens];
            FuncoesGerais.GenerateRandomArray(td, 10, 20, qViagens);
            td[0] = 0;
            FuncoesGerais.WriteArrayToFile(file, td, qViagens, "td");

            double[] tmaxvc = new double[qViagens];
            FuncoesGerais.GenerateRandomArray(tmaxvc, 120, 120, qViagens);
            tmaxvc[0] = 0;
            FuncoesGerais.WriteArrayToFile(file, tmaxvc, qViagens, "tmaxvc");

            double[] hs = new double[qViagens];
            FuncoesGerais.GenerateRandomArray(hs, 360, 1200, qViagens);
            hs[0] = 0;
            FuncoesGerais.WriteArrayToFile(file, hs, qViagens, "hs");

            double[,] c = new double[qViagens, qPontosCarga];
            FuncoesGerais.GenerateMatrixRowsByColumns(c, 40, 80, qViagens, qPontosCarga);
            for (int i = 0; i < qPontosCarga; i++)
                c[0, i] = 0;
            FuncoesGerais.WriteMatrixNxNToFile(file, c, qViagens, qPontosCarga, "c");

            double[] qvMax = new double[qPontosCarga];
            FuncoesGerais.GenerateRandomArray(qvMax, 33, 33, qPontosCarga);
            FuncoesGerais.WriteArrayToFile(file, qvMax, qPontosCarga, "qvMax");

            double[] cuve = new double[qPontosCarga];
            FuncoesGerais.GenerateRandomArray(cuve, 5, 10, qPontosCarga);
            FuncoesGerais.WriteArrayToFile(file, cuve, qPontosCarga, "cuve");

            double[,] pb = new double[qPontosCarga, qBetoneiras];
            FuncoesGerais.Generate01MatrixRowsByColumnsByIntervals(pb, qPontosCarga, qBetoneiras);
            FuncoesGerais.WriteMatrixNxNToFile(file, pb, qPontosCarga, qBetoneiras, "pb");

            double[] cnv = new double[qViagens];
            FuncoesGerais.GenerateRandomArray(cnv, 10000, 20000, qViagens);
            FuncoesGerais.WriteArrayToFile(file, cnv, qViagens, "cnv");

            file.Close();
        }
    }
}
