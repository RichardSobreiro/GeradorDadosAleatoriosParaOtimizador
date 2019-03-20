using System.IO;

namespace GeradorDadosOtimizacaoArtigoSBPO
{
    public static class GerarDadosAleatoriosProblemaDieta
    {
        static int qConcreto = 4; // Quantidade de tipos de concreto 
        static int qPeriodo = 10; // Quantidade de dias de planejamento
        static int qPontosCarga = 2; // Quantidade de pontos de carga
        static int qCimento = 4; // Quantidade de cimentos
        static int qAdicao = 8;
        static int qAgregadoMiudo = 4;
        static int qAgregadoGraudo = 4;
        static int qAgua = 4;
        static int qAditivo = 4;
        static int qMateriaisAdicionais = 4;

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

            double[,,] cct = new double[qCimento, qPontosCarga, qConcreto]; // Custo do metro cúbico do cimento ct no ponto de carga p
            double[,,] cad = new double[qAdicao, qPontosCarga, qConcreto]; // Custo unitário da adição ad no ponto de carga p
            double[,,] cam = new double[qAgregadoMiudo, qPontosCarga, qConcreto]; // Custo unitário da areia am no ponto de carga p 
            double[,,] cag = new double[qAgregadoGraudo, qPontosCarga, qConcreto]; // Custo unitário da brita ag no ponto de carga p
            double[,,] ca = new double[qAgua, qPontosCarga, qConcreto];  // Custo unitário da água a no ponto de carga p
            double[,,] cav = new double[qAditivo, qPontosCarga, qConcreto]; // Custo unitário do aditivo av no ponto de carga p
            double[,,] cma = new double[qMateriaisAdicionais, qPontosCarga, qConcreto]; // Custo unitário do material adicional ma no ponto de carga p 

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

            FuncoesGerais.GenerateMatrixNxNxN(cct, 200, 400, qCimento, qPontosCarga, qConcreto);
            FuncoesGerais.GenerateMatrixNxNxN(cad, 50, 100, qAdicao, qPontosCarga, qConcreto);
            FuncoesGerais.GenerateMatrixNxNxN(cam, 100, 200, qAgregadoMiudo, qPontosCarga, qConcreto);
            FuncoesGerais.GenerateMatrixNxNxN(cag, 100, 200, qAgregadoGraudo, qPontosCarga, qConcreto);
            FuncoesGerais.GenerateMatrixNxNxN(ca, 20, 40, qAgua, qPontosCarga, qConcreto);
            FuncoesGerais.GenerateMatrixNxNxN(cav, 30, 40, qAditivo, qPontosCarga, qConcreto);
            FuncoesGerais.GenerateMatrixNxNxN(cma, 100, 200, qMateriaisAdicionais, qPontosCarga, qConcreto);

            FuncoesGerais.GenerateMatrixRowsByColumns(ecto, 400, 400, qCimento, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(eado, 40, 40, qAdicao, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(eamo, 600, 600, qAgregadoMiudo, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(eago, 600, 600, qAgregadoGraudo, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(eao, 600, 600, qAgua, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(eavo, 40, 40, qAditivo, qConcreto);
            FuncoesGerais.GenerateMatrixRowsByColumns(emao, 1, 1, qMateriaisAdicionais, qConcreto);

            FuncoesGerais.GenerateMatrixNxNxN(Mc, 140, 140, qConcreto, qPontosCarga, qConcreto);

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

                FuncoesGerais.WriteMatrixNxNxNToFile(file, cct, qCimento, qPontosCarga, qConcreto, "cct");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, cad, qAdicao, qPontosCarga, qConcreto, "cad");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, cam, qAgregadoMiudo, qPontosCarga, qConcreto, "cam");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, cag, qAgregadoGraudo, qPontosCarga, qConcreto, "cag");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, ca, qAgua, qPontosCarga, qConcreto, "ca");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, cav, qAditivo, qPontosCarga, qConcreto, "cav");
                FuncoesGerais.WriteMatrixNxNxNToFile(file, cma, qMateriaisAdicionais, qPontosCarga, qConcreto, "cma");

                FuncoesGerais.WriteMatrixNxNToFile(file, ecto, qCimento, qConcreto, "ecto");
                FuncoesGerais.WriteMatrixNxNToFile(file, eado, qAdicao, qConcreto, "eado");
                FuncoesGerais.WriteMatrixNxNToFile(file, eamo, qAgregadoMiudo, qConcreto, "eamo");
                FuncoesGerais.WriteMatrixNxNToFile(file, eago, qAgregadoGraudo, qConcreto, "eago");
                FuncoesGerais.WriteMatrixNxNToFile(file, eao, qAgua, qConcreto, "eao");
                FuncoesGerais.WriteMatrixNxNToFile(file, eavo, qAditivo, qConcreto, "eavo");
                FuncoesGerais.WriteMatrixNxNToFile(file, emao, qMateriaisAdicionais, qConcreto, "emao");

                FuncoesGerais.WriteMatrixNxNxNToFile(file, Mc, qConcreto, qPontosCarga, qConcreto, "Mc");
            }
        }
    }
}
