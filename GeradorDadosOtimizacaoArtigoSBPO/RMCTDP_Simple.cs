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
        public int RMCType { get; set; }
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
        public int RMCType { get; set; }
    }

    public static class RMCTDP_Simple
    {
        public static void Execute(string path)
        {
            int nLP = 10;
            int nMT = 60;
            int numberOrders = 50;
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
            int numberOfTypesOfRMC = 10;

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
                order.RMCType = random.Next(0, numberOfTypesOfRMC);
                for (int i = 0; i < nLP; i++)
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
                    newDelivery.RMCType = order.RMCType;
                    order.Deliveries.Add(newDelivery);
                }
                order.MustBeServed = mustBeServed;
                orders.Add(order);
                numberOrders--;
            }
            // If RMC is available at the loading place
            int[,] rmclp = new int[numberOfTypesOfRMC, nLP];
            for (int i = 0; i < numberOfTypesOfRMC; i++)
            {
                for (int j = 0; j < nLP; j++)
                {
                    rmclp[i, j] = random.Next(0, 2);
                }
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
            int[] lpmt = new int[nMT];
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
                    lpmt[aux] = i;
                    aux++;
                }
                if(aux >= nMT)
                {
                    break;
                }
                ctrl += numberOfPartitions;
            }
            FuncoesGerais.WriteArrayToFile(file, lpmt, nMT, "lpmt");
            //--------------------------------------------------------------

            double[,] c = new double[nMT, nD];
            double[,] t = new double[nMT, nD];
            double[] d = new double[nD];
            double[] a = new double[nD];
            double[] b = new double[nD];
            int[] cfr = new int[nD];
            int[] dmbs = new int[nD];
            int[,] dmt = new int[nMT, nD];
            double[] r = new double[nD];
            int fdno = 0; // Index of the first delivery of the new order
            bool firstDeliveryOfTheNewOrderWasNotCapturedYet = true;
            for (int i = 0; i < nMT; i++)
            {
                for (int j = 0; j < nD; j++)
                {
                    c[i, j] = deliveries[j].CostEachLoadingPlace[lpmt[i] - 1];
                    t[i, j] = deliveries[j].TravelTimeEachLoadingPlace[lpmt[i] - 1];
                    d[j] = deliveries[j].Volume;
                    a[j] = deliveries[j].ServiceTime;
                    b[j] = a[j] + 5;
                    cfr[j] = 2;
                    r[j] = deliveries[j].Revenue;
                    dmbs[j] = deliveries[j].MustBeServed;
                    dmt[i, j] = rmclp[deliveries[j].RMCType, lpmt[i] - 1];
                    if(deliveries[j].MustBeServed == 0 && firstDeliveryOfTheNewOrderWasNotCapturedYet)
                    {
                        fdno = j + 1;
                        firstDeliveryOfTheNewOrderWasNotCapturedYet = false;
                    }
                }
            }
            FuncoesGerais.WriteMatrixNxNToFile(file, c, nMT, nD, "c");
            FuncoesGerais.WriteMatrixNxNToFile(file, t, nMT, nD, "t");
            FuncoesGerais.WriteMatrixNxNToFile(file, dmt, nMT, nD, "dmt");

            float q = 8;
            file.WriteLine($"q = {q};");

            float tc = 50;
            file.WriteLine($"tc = {tc};");

            file.WriteLine($"fdno = {fdno};");

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
