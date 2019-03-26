using System.IO;

namespace GeradorDadosOtimizacaoArtigoSBPO
{
    public static class GerarDadosProgramacaoProducao
    {
        public static void Execute(string path, int qViagens, int qPontosCarga, int M)
        {
            File.WriteAllText(path, string.Empty);
            var file = new StreamWriter(path);

            file.WriteLine($"qViagens = {qViagens};");
            file.WriteLine($"qPontosCarga = {qPontosCarga};");
            file.WriteLine($"M = {M};");

            double[,] dp = new double[qViagens, qPontosCarga];
            FuncoesGerais.GenerateMatrixRowsByColumns(dp, 25, 25, qViagens, qPontosCarga);
            FuncoesGerais.WriteMatrixNxNToFile(file, dp, qViagens, qPontosCarga, "dp");

            double[] hs = new double[qViagens];
            FuncoesGerais.GenerateRandomArray(hs, 360, 1200, qViagens);
            FuncoesGerais.WriteArrayToFile(file, hs, qViagens, "hs");

            file.Close();
        }
    }
}
