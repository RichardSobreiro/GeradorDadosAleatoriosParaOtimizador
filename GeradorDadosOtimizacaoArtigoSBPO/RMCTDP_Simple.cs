using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GeradorDadosOtimizacaoArtigoSBPO
{
    class Order
    {
        public double TotalVolume { get; set; }
        public int ServiceTime { get; set; }
        public int Interval { get; set; }
        public double RevenuePerCubicMeter { get; set; }
        public List<double> TravelTimeEachLoadingPlace { get; set; } = new List<double>();
        public List<double> CostEachLoadingPlace { get; set; } = new List<double>();
        public List<Delivery> Deliveries { get; set; }
        public int MustBeServed { get; set; }
    }

    class Delivery
    {
        public double Volume { get; set; }
        public int ServiceTime { get; set; }
        public int Revenue { get; set; }
        public List<double> TravelTimeEachLoadingPlace { get; set; }
        public List<double> CostEachLoadingPlace { get; set; }
        public int MustBeServed { get; set; }
    }

    public static class RMCTDP_Simple
    {
        public static void Execute(string path)
        {
            int nLP = 5;
            int nMT = 30;
            int numberOrders = 15;
            int minVolumePerOrder = 12;
            int maxVolumePerOrder = 40;
            int beginTimeLoadingPlaces = 420;
            int endTimeLoadingPlaces = 1140;
            int minRevenuePerCubicMeter = 150;
            int maxRevenuePerCubicMeter = 500;
            int minCostLoadingPlace = 100;
            int maxCostLoadingPlace = 200;
            int minTravelTimeLoadingPlace = 15;
            int maxTravelTimeLoadingPlace = 40;
            int mixerTruckCubicMetersCapactity = 8;

            List<Order> orders = new List<Order>();
            Random random = new Random();
            while(numberOrders >= 1)
            {
                Order order = new Order();
                int nextServiceTime = random.Next(beginTimeLoadingPlaces, endTimeLoadingPlaces);
                order.ServiceTime = nextServiceTime;
                order.TotalVolume = random.NextDouble() * (maxVolumePerOrder - minVolumePerOrder) + minVolumePerOrder;
                order.Interval = 20;
                order.RevenuePerCubicMeter = random.Next(minRevenuePerCubicMeter, maxRevenuePerCubicMeter);
                for(int i = 0; i < nLP; i++)
                {
                    order.TravelTimeEachLoadingPlace.Add((random.NextDouble() * (maxTravelTimeLoadingPlace - minTravelTimeLoadingPlace) + minTravelTimeLoadingPlace));
                    order.CostEachLoadingPlace.Add((random.NextDouble() * (maxCostLoadingPlace - minCostLoadingPlace) + minCostLoadingPlace));
                }
                int nd = (int)Math.Ceiling(order.TotalVolume / mixerTruckCubicMetersCapactity);
                order.Deliveries = new List<Delivery>();
                double remainingVolume = order.TotalVolume;
                int mustBeServed = 1;
                if(numberOrders == 1)
                {
                    mustBeServed = 0;
                }
                for (int i = 0; i < nd; i++)
                {
                    Delivery newDelivery = new Delivery();
                    if (remainingVolume > mixerTruckCubicMetersCapactity)
                    {
                        newDelivery.Volume = mixerTruckCubicMetersCapactity;
                        remainingVolume -= mixerTruckCubicMetersCapactity;
                    }
                    else
                    {
                        newDelivery.Volume = remainingVolume;
                    }
                    newDelivery.ServiceTime = nextServiceTime;
                    nextServiceTime += order.Interval;
                    newDelivery.Revenue = (int)(order.RevenuePerCubicMeter * newDelivery.Volume);
                    newDelivery.TravelTimeEachLoadingPlace = order.TravelTimeEachLoadingPlace;
                    newDelivery.CostEachLoadingPlace = order.CostEachLoadingPlace;
                    newDelivery.MustBeServed = mustBeServed;
                    order.Deliveries.Add(newDelivery);
                }
                order.MustBeServed = mustBeServed;
                orders.Add(order);
                numberOrders--;
            }

            List<Delivery> deliveries = new List<Delivery>();
            orders.ForEach(order => deliveries.AddRange(order.Deliveries));

            File.WriteAllText(path, string.Empty);
            var file = new StreamWriter(path);

            file.WriteLine($"nLP = {nLP};");

            file.WriteLine($"nMT = {nMT};");

            int nD = deliveries.Count();
            file.WriteLine($"nD = {nD};");

            // RMC Mixer Trucks to Loading Places
            int[] lpctm = new int[nMT];
            int numberOfPartitions = nMT / nLP;
            int ctrl = numberOfPartitions;
            int aux = 0;
            for (int i = 1; i <= (nLP+1); i++)
            {
                if(i == (nLP + 1))
                {
                    i--;
                }
                while (aux < ctrl && aux < nMT)
                {
                    lpctm[aux] = i;
                    aux++;
                }
                if(aux >= nMT)
                {
                    break;
                }
                ctrl += numberOfPartitions;
            }
            FuncoesGerais.WriteArrayToFile(file, lpctm, nMT, "lpctm");
            //--------------------------------------------------------------

            double[,] c = new double[nMT, nD];
            double[,] t = new double[nMT, nD];
            double[] d = new double[nD];
            double[] a = new double[nD];
            double[] b = new double[nD];
            int[] cfr = new int[nD];
            int[] dmbs = new int[nD];
            double[] r = new double[nD];
            for (int i = 0; i < nMT; i++)
            {
                for (int j = 0; j < nD; j++)
                {
                    c[i, j] = deliveries[j].CostEachLoadingPlace[lpctm[i] - 1];
                    t[i, j] = deliveries[j].TravelTimeEachLoadingPlace[lpctm[i] - 1];
                    d[j] = deliveries[j].Volume;
                    a[j] = deliveries[j].ServiceTime;
                    b[j] = a[j] + 5;
                    cfr[j] = 2;
                    r[j] = deliveries[j].Revenue;
                    dmbs[j] = deliveries[j].MustBeServed;
                }
            }
            FuncoesGerais.WriteMatrixNxNToFile(file, c, nMT, nD, "c");
            FuncoesGerais.WriteMatrixNxNToFile(file, t, nMT, nD, "t");

            float q = 8;
            file.WriteLine($"q = {q};");

            float tc = 8;
            file.WriteLine($"tc = {tc};");

            FuncoesGerais.WriteArrayToFile(file, d, nD, "d");

            FuncoesGerais.WriteArrayToFile(file, a, nD, "a");

            FuncoesGerais.WriteArrayToFile(file, b, nD, "b");

            FuncoesGerais.WriteArrayToFile(file, cfr, nD, "cfr");

            FuncoesGerais.WriteArrayToFile(file, r, nD, "r");

            FuncoesGerais.WriteArrayToFile(file, dmbs, nD, "dmbs");

            float ld = mixerTruckCubicMetersCapactity;
            file.WriteLine($"ld = {ld};");

            float M = 5000;
            file.WriteLine($"M = {M};");

            file.Close();
        }
    }
}
