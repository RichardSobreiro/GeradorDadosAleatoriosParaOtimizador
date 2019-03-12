namespace GeradorDadosOtimizacaoArtigoSBPO
{
    public static class GerarDadosAleatoriosMDVHRPTW
    {
        public static void Executar(int qC, int qP, int qB, int qVg)
        {
            int V = qC + qP;
            double[,] rC = new double[V, V];
            double[,] cac = new double[V, qP];
            double[] cuc = new double[qB]; // Custo de uso do caminhao k
            int ti[V][V] = ...; // Tempo trajeto ida entre i e j
int tv[V][V] = ...; // Tempo trajeto volta entre i e j
int tp[rP] = ...; // Tempo de pesagem em cada ponto de carga p
        int td[V] = ...; // Tempo de descarga no cliente c
        int hc[rC][Vg] = ...; // Hora de chega para cada viagem do cliente
float vc[rC][Vg] = ...; // Volume de material solicitado pelo cliente em cada viagem 
int qv[V] = ...; // Quantidade de viagens para cada cliente
        int kp[rP] = ...; // Quantidade veiculos disponiveis por central
    }

        static GerarDados()
        {

        }
    }
}
