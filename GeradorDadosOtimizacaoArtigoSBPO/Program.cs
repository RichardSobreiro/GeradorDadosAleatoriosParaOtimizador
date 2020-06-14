namespace GeradorDadosOtimizacaoArtigoSBPO
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = $"C:\\Users\\Richard Sobreiro\\opl\\ReadyMixedConcreteTruckDispatching\\MDVRPTWMultiTrips.dat";

            MDVRPTWMultiTrips.Execute(path);
        }
    }


}
