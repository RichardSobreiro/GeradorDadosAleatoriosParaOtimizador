using System.IO;

namespace GeradorDadosOtimizacaoArtigoSBPO
{
    public static class GerarDadosAleatoriosProblemaDieta
    {
        static int qConcreto = 1; // Quantidade de tipos de concreto 
        static int qPeriodo = 2; // Quantidade de dias de planejamento
        static int qPontosCarga = 1; // Quantidade de pontos de carga
        static int qCimento = 1; // Quantidade de cimentos
        static int qAdicao = 1;
        static int qAgregadoMiudo = 1;
        static int qAgregadoGraudo = 1;
        static int qAgua = 1;
        static int qAditivo = 1;
        static int qMateriaisAdicionais = 1;

        public static void Execute(string path,
            int qConcreto,
            int qPeriodo,
            int qPontosCarga,
            int qCimento,
            int qAdicao,
            int qAgregadoMiudo,
            int qAgregadoGraudo,
            int qAgua,
            int qAditivo,
            int qMateriaisAdicionais)
        {
            double[,] d = new double[qConcreto, qPeriodo]; // Demanda do concreto c no dia t

            double[,] qct = new double[qCimento, qConcreto]; // Quantidade do cimento ct necessária para produzir um metro cúbico do concreto c
            double[,] qad = new double[qAdicao, qConcreto]; // Quantidade da adição ad necessária para produzir um metro cúbico do concreto c
            double[,] qam = new double[qAgregadoMiudo, qConcreto]; // Quantidade da areia am necessária para produzir um metro cúbico do concreto c 
            double[,] qag = new double[qAgregadoGraudo, qConcreto]; // Quantidade da brita ag necessária para produzir um metro cúbico do concreto c
            double[,] qa = new double[qAgua, qConcreto];  // Quantidade da água a necessária para produzir um metro cúbico do concreto c
            double[,] qav = new double[qAditivo, qConcreto]; // Quantidade do aditivo av necessário para produzir um metro cúbico do concreto c
            double[,] qma = new double[qMateriaisAdicionais, qConcreto]; // Quantidade do material adicional ma necessário para produzir um metro cúbico do concreto c

            double[,,] cct = new double[qCimento, qPontosCarga, qPeriodo]; // Custo do metro cúbico do cimento ct no ponto de carga p
            double[,,] cad = new double[qAdicao, qPontosCarga, qPeriodo]; // Custo unitário da adição ad no ponto de carga p
            double[,,] cam = new double[qAgregadoMiudo, qPontosCarga, qPeriodo]; // Custo unitário da areia am no ponto de carga p 
            double[,,] cag = new double[qAgregadoGraudo, qPontosCarga, qPeriodo]; // Custo unitário da brita ag no ponto de carga p
            double[,,] ca = new double[qAgua, qPontosCarga, qPeriodo];  // Custo unitário da água a no ponto de carga p
            double[,,] cav = new double[qAditivo, qPontosCarga, qPeriodo]; // Custo unitário do aditivo av no ponto de carga p
            double[,,] cma = new double[qMateriaisAdicionais, qPontosCarga, qPeriodo]; // Custo unitário do material adicional ma no ponto de carga p 

            double[,] ecto = new double[qCimento, qPontosCarga]; // Estoque inicial do cimento c
            double[,] eado = new double[qAdicao, qPontosCarga]; // Estoque inicial de adição ad no ponto de carga p
            double[,] eamo = new double[qAgregadoMiudo, qPontosCarga]; // Estoque inicial de areia am no ponto de carga p
            double[,] eago = new double[qAgregadoGraudo, qPontosCarga]; // Estoque inicial de brita ag no ponto de carga p
            double[,] eao = new double[qAgua, qPontosCarga]; // Estoque inicial de água a no ponto de carga p
            double[,] eavo = new double[qAditivo, qPontosCarga]; // Estoque inicial de aditivo av no ponto de carga p
            double[,] emao = new double[qMateriaisAdicionais, qPontosCarga]; // Estoque inicial de material adicional ma no ponto de carga p

            double[,,] Mc = new double[qConcreto, qPontosCarga, qPeriodo]; // Máxima quantidade de concreto c que pode ser demandada no ponto de carga p no dia t

            FuncoesGerais.GenerateMatrixRowsByColumns(d, 100, 140, qConcreto, qPeriodo);

            FuncoesGerais.GenerateMatrixRowsByColumns(qct, 200, 400, qCimento, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(qad, 20, 40, qAdicao, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(qam, 400, 600, qAgregadoMiudo, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(qag, 400, 600, qAgregadoGraudo, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(qa, 500, 600, qAgua, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(qav, 20, 40, qAditivo, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(qma, 0, 1, qMateriaisAdicionais, qConcreto);

            FuncoesGerais.GenerateMatrixNxNxN(cct, 200, 400, qCimento, qPontosCarga, qPeriodo);
            FuncoesGerais.GenerateMatrixNxNxN(cad, 50, 100, qAdicao, qPontosCarga, qPeriodo);
            FuncoesGerais.GenerateMatrixNxNxN(cam, 100, 200, qAgregadoMiudo, qPontosCarga, qPeriodo);
            FuncoesGerais.GenerateMatrixNxNxN(cag, 100, 200, qAgregadoGraudo, qPontosCarga, qPeriodo);
            FuncoesGerais.GenerateMatrixNxNxN(ca, 20, 40, qAgua, qPontosCarga, qPeriodo);
            FuncoesGerais.GenerateMatrixNxNxN(cav, 30, 40, qAditivo, qPontosCarga, qPeriodo);
            FuncoesGerais.GenerateMatrixNxNxN(cma, 100, 200, qMateriaisAdicionais, qPontosCarga, qPeriodo);

            FuncoesGerais.GenerateMatrixRowsByColumns(ecto, 400, 400, qCimento, qPontosCarga);
            FuncoesGerais.GenerateMatrixRowsByColumns(eado, 40, 40, qAdicao, qPontosCarga);
            FuncoesGerais.GenerateMatrixRowsByColumns(eamo, 600, 600, qAgregadoMiudo, qPontosCarga);
            FuncoesGerais.GenerateMatrixRowsByColumns(eago, 600, 600, qAgregadoGraudo, qPontosCarga);
            FuncoesGerais.GenerateMatrixRowsByColumns(eao, 600, 600, qAgua, qPontosCarga);
            FuncoesGerais.GenerateMatrixRowsByColumns(eavo, 40, 40, qAditivo, qPontosCarga);
            FuncoesGerais.GenerateMatrixRowsByColumns(emao, 1, 1, qMateriaisAdicionais, qPontosCarga);

            FuncoesGerais.GenerateMatrixNxNxN(Mc, 140, 140, qConcreto, qPontosCarga, qPeriodo);

            File.WriteAllText(path, string.Empty);
            using (var file = new StreamWriter(path))
            {
                file.WriteLine($"qConcreto = {qConcreto};");
                file.WriteLine($"qPeriodo = {qPeriodo};");
                file.WriteLine($"qPontosCarga = {qPontosCarga};");
                file.WriteLine($"qCimento = {qCimento};");
                file.WriteLine($"qAdicao = {qAdicao};");
                file.WriteLine($"qAgregadoMiudo = {qAgregadoMiudo};");
                file.WriteLine($"qAgregadoGraudo = {qAgregadoGraudo};");
                file.WriteLine($"qAgua = {qAgua};");
                file.WriteLine($"qAditivo = {qAditivo};");
                file.WriteLine($"qMateriaisAdicionais = {qMateriaisAdicionais};");

                FuncoesGerais.WriteMatrixNxNToFile(file, d, qConcreto, qPeriodo, "d");

                FuncoesGerais.WriteMatrixNxNToFile(file, qct, qCimento, qConcreto, "qct");
                FuncoesGerais.WriteMatrixNxNToFile(file, qad, qAdicao, qConcreto, "qad");
                FuncoesGerais.WriteMatrixNxNToFile(file, qam, qAgregadoMiudo, qConcreto, "qam");
                FuncoesGerais.WriteMatrixNxNToFile(file, qag, qAgregadoGraudo, qConcreto, "qag");
                FuncoesGerais.WriteMatrixNxNToFile(file, qa, qAgua, qConcreto, "qa");
                FuncoesGerais.WriteMatrixNxNToFile(file, qav, qAditivo, qConcreto, "qav");
                FuncoesGerais.WriteMatrixNxNToFile(file, qma, qMateriaisAdicionais, qConcreto, "qma");

                FuncoesGerais.WriteMatrixNxNxNToFile(file, cct, qCimento, qPontosCarga, qPeriodo, "cct");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, cad, qAdicao, qPontosCarga, qPeriodo, "cad");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, cam, qAgregadoMiudo, qPontosCarga, qPeriodo, "cam");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, cag, qAgregadoGraudo, qPontosCarga, qPeriodo, "cag");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, ca, qAgua, qPontosCarga, qPeriodo, "ca");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, cav, qAditivo, qPontosCarga, qPeriodo, "cav");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, cma, qMateriaisAdicionais, qPontosCarga, qPeriodo, "cma");

                FuncoesGerais.WriteMatrixNxNToFile(file, ecto, qCimento, qPontosCarga, "ecto");
                FuncoesGerais.WriteMatrixNxNToFile(file, eado, qAdicao, qPontosCarga, "eado");
                FuncoesGerais.WriteMatrixNxNToFile(file, eamo, qAgregadoMiudo, qPontosCarga, "eamo");
                FuncoesGerais.WriteMatrixNxNToFile(file, eago, qAgregadoGraudo, qPontosCarga, "eago");
                FuncoesGerais.WriteMatrixNxNToFile(file, eao, qAgua, qPontosCarga, "eao");
                FuncoesGerais.WriteMatrixNxNToFile(file, eavo, qAditivo, qPontosCarga, "eavo");
                FuncoesGerais.WriteMatrixNxNToFile(file, emao, qMateriaisAdicionais, qPontosCarga, "emao");

                FuncoesGerais.WriteMatrixNxNxNToFile(file, Mc, qConcreto, qPontosCarga, qPeriodo, "Mc");
            }
        }
    }
}
