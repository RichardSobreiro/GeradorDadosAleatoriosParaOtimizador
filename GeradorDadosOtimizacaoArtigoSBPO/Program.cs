namespace GeradorDadosOtimizacaoArtigoSBPO
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\richa\\opl\\ArtigoSBPO201904\\MDVHRTWSDR.dat";
            GerarDadosAleatoriosMDVHRPTW.Execute(path, 5, 1, 10, 4);
        }
    }


}
