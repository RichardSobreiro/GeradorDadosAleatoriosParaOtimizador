namespace GeradorDadosOtimizacaoArtigoSBPO
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\richa\\opl\\ArtigoSBPO201904\\MDVHRTWSDR.dat";
            //GerarDadosAleatoriosMDVHRPTW.Execute(path, 5, 1, 10, 4);

            int qConcreto = 4; // Quantidade de tipos de concreto 
            int qPeriodo = 10; // Quantidade de dias de planejamento
            int qPontosCarga = 2; // Quantidade de pontos de carga
            int qCimento = 4; // Quantidade de cimentos
            int qAdicao = 8;
            int qAgregadoMiudo = 4;
            int qAgregadoGraudo = 4;
            int qAgua = 4;
            int qAditivo = 4;
            int qMateriaisAdicionais = 4;

            path = "C:\\Users\\richa\\opl\\ArtigoSBPO201904\\Dieta.dat";
            GerarDadosAleatoriosProblemaDieta.Execute(path, qConcreto, qPeriodo, qPontosCarga,
                qCimento, qAdicao, qAgregadoMiudo, qAgregadoGraudo, qAgua, qAditivo, qMateriaisAdicionais);
        }
    }


}
