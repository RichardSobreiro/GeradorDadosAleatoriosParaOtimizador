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
            FuncoesGerais.GenerateSymmetricMatrixNxN(V, cr, 10, 40);
            FuncoesGerais.GenerateMatrixRowsByColumns(cac, 200, 400, V, qP, qP);
            FuncoesGerais.GenerateRandomArray(cuc, 10, 20, qB);
            FuncoesGerais.GenerateSymmetricMatrixNxN(V, ti, 10, 80);
            FuncoesGerais.GenerateSymmetricMatrixNxN(V, tv, 10, 80);
            FuncoesGerais.GenerateRandomArray(tp, 7, 7, qP);
            FuncoesGerais.GenerateRandomArray(td, 15, 30, V, qP);
            FuncoesGerais.GenerateMatrixRowsByColumns(hc, 400, 1200, V, qVg, qP);
            FuncoesGerais.GenerateMatrixRowsByColumns(vc, 8, 8, V, qVg, qP);
            FuncoesGerais.GenerateRandomArray(qv, qVg, qVg, V, qP);
            FuncoesGerais.GenerateRandomArray(kp, 10, 10, qP);
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
                FuncoesGerais.WriteMatrixNxNToFile(file, cr, V, V, "cr");
                FuncoesGerais.WriteMatrixNxNToFile(file, cac, V, qP, "cac");
                FuncoesGerais.WriteArrayToFile(file, cuc, qB, "cuc");
                FuncoesGerais.WriteMatrixNxNToFile(file, ti, V, V, "ti");
                FuncoesGerais.WriteMatrixNxNToFile(file, tv, V, V, "tv");
                FuncoesGerais.WriteArrayToFile(file, tp, qP, "tp");
                FuncoesGerais.WriteArrayToFile(file, td, V, "td");
                FuncoesGerais.WriteMatrixNxNToFile(file, hc, V, qVg, "hc");
                FuncoesGerais.WriteMatrixNxNToFile(file, vc, V, qVg, "vc");
                FuncoesGerais.WriteArrayToFile(file, qv, V, "qv");
                FuncoesGerais.WriteArrayToFile(file, kp, qP, "kp");
            }
        }     
    }
}
