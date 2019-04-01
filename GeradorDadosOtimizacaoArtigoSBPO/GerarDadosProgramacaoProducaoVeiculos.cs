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
            FuncoesGerais.GenerateMatrixRowsByColumns(dp, 25, 25, qViagens, qPontosCarga);
            FuncoesGerais.WriteMatrixNxNToFile(file, dp, qViagens, qPontosCarga, "dp");

            double[,] dv = new double[qViagens, qPontosCarga];
            FuncoesGerais.GenerateMatrixRowsByColumns(dv, 8, 40, qViagens, qPontosCarga);
            FuncoesGerais.WriteMatrixNxNToFile(file, dv, qViagens, qPontosCarga, "dv");

            double[] td = new double[qViagens];
            FuncoesGerais.GenerateRandomArray(td, 10, 20, qViagens);
            FuncoesGerais.WriteArrayToFile(file, td, qViagens, "td");

            double[] tmaxvc = new double[qViagens];
            FuncoesGerais.GenerateRandomArray(tmaxvc, 120, 120, qViagens);
            FuncoesGerais.WriteArrayToFile(file, tmaxvc, qViagens, "tmaxvc");

            double[] hs = new double[qViagens];
            FuncoesGerais.GenerateRandomArray(hs, 360, 1200, qViagens);
            FuncoesGerais.WriteArrayToFile(file, hs, qViagens, "hs");

            double[,] c = new double[qViagens, qPontosCarga];
            FuncoesGerais.GenerateMatrixRowsByColumns(c, 40, 80, qViagens, qPontosCarga);
            FuncoesGerais.WriteMatrixNxNToFile(file, c, qViagens, qPontosCarga, "c");

            double[] qvMax = new double[qPontosCarga];
            FuncoesGerais.GenerateRandomArray(qvMax, 33, 33, qPontosCarga);
            FuncoesGerais.WriteArrayToFile(file, qvMax, qPontosCarga, "qvMax");

            double[] cuve = new double[qPontosCarga];
            FuncoesGerais.GenerateRandomArray(cuve, 5, 10, qPontosCarga);
            FuncoesGerais.WriteArrayToFile(file, cuve, qPontosCarga, "cuve");

            file.Close();
        }
    }
}
