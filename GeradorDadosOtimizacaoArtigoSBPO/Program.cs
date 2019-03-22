﻿namespace GeradorDadosOtimizacaoArtigoSBPO
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\richa\\opl\\ArtigoSBPO201904\\MDVHRTWSDR.dat";
            //GerarDadosAleatoriosMDVHRPTW.Execute(path, 5, 1, 10, 4);

            int qConcreto = 1; // Quantidade de tipos de concreto 
            int qPeriodo = 4; // Quantidade de dias de planejamento
            int qPontosCarga = 8; // Quantidade de pontos de carga
            int qCimento = 1; // Quantidade de cimentos
            int qAdicao = 1;
            int qAgregadoMiudo = 1;
            int qAgregadoGraudo = 1;
            int qAgua = 1;
            int qAditivo = 1;
            int qMateriaisAdicionais = 1;

            path = "C:\\Users\\richa\\opl\\ArtigoSBPO201904\\Dieta.dat";
            GerarDadosAleatoriosProblemaDieta.Execute(path, qConcreto, qPeriodo, 
                qPontosCarga, qCimento, qAdicao, qAgregadoMiudo, qAgregadoGraudo, 
                qAgua, qAditivo, qMateriaisAdicionais, 500, 1000);
        }
    }


}
