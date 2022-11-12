using NewspaperSellerModels;
using NewspaperSellerTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewspaperSellerSimulation
{
    class SimulationTable
    {
        List<SimulationCase> simulationTable;


        
        public SimulationTable()
        {

            Program.simulationSystem.PerformanceMeasures.TotalSalesProfit = 0;
            Program.simulationSystem.PerformanceMeasures.TotalLostProfit = 0;
            Program.simulationSystem.PerformanceMeasures.TotalNetProfit = 0;
            Program.simulationSystem.PerformanceMeasures.TotalScrapProfit = 0;
            Program.simulationSystem.PerformanceMeasures.TotalCost =
                ( Program.simulationSystem.NumOfNewspapers * Program.simulationSystem.PurchasePrice) *
                Program.simulationSystem.NumOfRecords ;

            int DaysWithMoreDemand = 0;
            int DaysWithUnsoldPapers = 0;


            simulationTable = new List<SimulationCase>();
            Random rnd = new Random();
            Random rnd2 = new Random();



            for (int i =0; i<Program.simulationSystem.NumOfRecords; i++)
            {
                SimulationCase simulationCase = new SimulationCase();
                simulationCase.DayNo = i + 1;

                simulationCase.RandomDemand = rnd.Next(1,101);

                
                simulationCase.RandomNewsDayType = rnd.Next(1,101);

                simulationCase.NewsDayType = MapingDayType(simulationCase.RandomNewsDayType);
                simulationCase.Demand = MapingDemande(simulationCase.RandomDemand, simulationCase.NewsDayType);
                simulationCase.DailyCost = Program.simulationSystem.NumOfNewspapers * Program.simulationSystem.PurchasePrice;

                if (simulationCase.Demand >= Program.simulationSystem.NumOfNewspapers)
                {
                    simulationCase.SalesProfit = Program.simulationSystem.NumOfNewspapers * Program.simulationSystem.SellingPrice;
         
                    simulationCase.LostProfit = (simulationCase.Demand - Program.simulationSystem.NumOfNewspapers)
                           * (Program.simulationSystem.SellingPrice - Program.simulationSystem.PurchasePrice);
                    simulationCase.DailyNetProfit = simulationCase.SalesProfit - simulationCase.DailyCost - simulationCase.LostProfit;
                    if( simulationCase.LostProfit != 0)DaysWithMoreDemand++;
                }
                else
                {
                    simulationCase.SalesProfit = simulationCase.Demand * Program.simulationSystem.SellingPrice;

                    simulationCase.ScrapProfit = ( Program.simulationSystem.NumOfNewspapers -  simulationCase.Demand)
                         * Program.simulationSystem.ScrapPrice ;

                    simulationCase.DailyNetProfit =   simulationCase.SalesProfit - simulationCase.DailyCost  + simulationCase.ScrapProfit;
                    if (simulationCase.ScrapProfit != 0) DaysWithUnsoldPapers++;
                }

                Program.simulationSystem.PerformanceMeasures.TotalSalesProfit += simulationCase.SalesProfit;
                Program.simulationSystem.PerformanceMeasures.TotalNetProfit += simulationCase.DailyNetProfit;
                Program.simulationSystem.PerformanceMeasures.TotalScrapProfit += simulationCase.ScrapProfit;
                Program.simulationSystem.PerformanceMeasures.TotalLostProfit += simulationCase.LostProfit;
               
               simulationTable.Add(simulationCase);
            }

            Program.simulationSystem.PerformanceMeasures.DaysWithMoreDemand = DaysWithMoreDemand;
            Program.simulationSystem.PerformanceMeasures.DaysWithUnsoldPapers = DaysWithUnsoldPapers;


            Program.simulationSystem.SimulationTable = simulationTable;

          

     





        }


        public Enums.DayType MapingDayType(int rand)
        {

            List<DayTypeDistribution> DayTypeDistributions = Program.simulationSystem.DayTypeDistributions;

            foreach(DayTypeDistribution obj in DayTypeDistributions)
            {
                if(rand >= obj.MinRange && rand <= obj.MaxRange)
                   return obj.DayType;
            }

            return Enums.DayType.Good; 

        } 

        public int MapingDemande(int rand , Enums.DayType  type)
        {
            foreach(DemandDistribution demand in Program.simulationSystem.DemandDistributions)
            {
                if(type == Enums.DayType.Good)
                {
                    if( rand >= demand.DayTypeDistributions[0].MinRange &&
                        rand <= demand.DayTypeDistributions[0].MaxRange)
                    {
                        return demand.Demand;
                    }

                }
                else if (type == Enums.DayType.Fair)
                {
                    if (rand >= demand.DayTypeDistributions[1].MinRange &&
                        rand <= demand.DayTypeDistributions[1].MaxRange)
                    {
                        return demand.Demand;
                    }

                }
                else if (type == Enums.DayType.Poor)
                {
                    if (rand >= demand.DayTypeDistributions[2].MinRange &&
                        rand <= demand.DayTypeDistributions[2].MaxRange)
                    {
                        return demand.Demand;
                    }

                }
            }

            return 0; 

        }
         
    }
}
