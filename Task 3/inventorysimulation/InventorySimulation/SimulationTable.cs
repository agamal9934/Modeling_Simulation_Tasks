using InventoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySimulation
{
    class SimulationTable
    {
        List<SimulationCase> simulationTable;
        public SimulationTable()
        {
            ReadFile readFile = new ReadFile();
            readFile.readfile();
            Program.simulationSystem.LeadDaysDistribution = readFile.getLeadDaysDistribution();
            Program.simulationSystem.DemandDistribution = readFile.getDemandDistribution();
            simulationTable = new List<SimulationCase>();
            
            Random rnd = new Random();

            //for first row
           
            

            int totalShortage = 0;
            int prevEndingInventory = readFile.getStartInventoryQuantity();
            int daysUntilOrder = readFile.getStartLeadDays();
            int orderQty = readFile.getStartOrderQuantity();
            int cycle = 0;

            for (int i = 0; i < Program.simulationSystem.NumberOfDays; )
            {
               
                cycle++;
                for (int j = 0; j < Program.simulationSystem.ReviewPeriod; j++)
                {
                    SimulationCase simulationCase = new SimulationCase();
                    i++;
                   
                    
                    simulationCase.Day = i;
                    simulationCase.Cycle = cycle;
                    simulationCase.DayWithinCycle = j+1;
                    
                    if (daysUntilOrder > 0 || (daysUntilOrder == 0 && orderQty > 0))
                        daysUntilOrder--;
                    simulationCase.RandomDemand = rnd.Next(1, 101);
                    simulationCase.Demand = DemandMapping(simulationCase.RandomDemand);
                    
                    if (daysUntilOrder == -1 && orderQty > 0)
                    {
                        simulationCase.BeginningInventory = prevEndingInventory + orderQty;
                    
                        simulationCase.EndingInventory = simulationCase.BeginningInventory
                                                    - simulationCase.Demand - totalShortage;
                        orderQty = 0;
                        daysUntilOrder = 0;
                        totalShortage = 0;
                    }
                    else { 
                        simulationCase.BeginningInventory = prevEndingInventory;
                        simulationCase.EndingInventory = simulationCase.BeginningInventory
                                                    - simulationCase.Demand;
                    }
                    
                    
                    if(simulationCase.EndingInventory < 0)
                    {
                        simulationCase.EndingInventory = 0;
                        
                    }
                    prevEndingInventory = simulationCase.EndingInventory;
                    if (simulationCase.Demand <= simulationCase.BeginningInventory)
                    {
                        simulationCase.ShortageQuantity = 0;
                        totalShortage = 0;
                    }
                    else
                    {
                        simulationCase.ShortageQuantity = totalShortage + simulationCase.Demand - simulationCase.BeginningInventory;
                        totalShortage = simulationCase.ShortageQuantity;
                    }
                    //check last day in cycle
                    if (j == Program.simulationSystem.ReviewPeriod - 1)
                    {
                        simulationCase.RandomLeadDays = rnd.Next(1, 101);
                        simulationCase.OrderQuantity = Program.simulationSystem.OrderUpTo - simulationCase.EndingInventory + totalShortage;
                        orderQty = simulationCase.OrderQuantity;
                       
                        simulationCase.LeadDays = LeadDaysMapping(simulationCase.RandomLeadDays);
                        daysUntilOrder = simulationCase.LeadDays;
                    }
                    else
                    {   simulationCase.OrderQuantity = 0;
                        simulationCase.LeadDays = 0;
                        simulationCase.RandomLeadDays = 0;
                    }
                    simulationTable.Add(simulationCase);
                }

               
            }
            Program.simulationSystem.SimulationCases= simulationTable;
            
        }

        public int LeadDaysMapping(int rand)
        {
            List<Distribution> LeadDaysDistributions = Program.simulationSystem.LeadDaysDistribution;

            foreach (var LeadDay in LeadDaysDistributions)
            {
                if (rand >= LeadDay.MinRange && rand <= LeadDay.MaxRange)
                    return LeadDay.Value;
            }
            return 0;
        }
        public int DemandMapping(int rand)
        {
            List<Distribution> DemandDistributions = Program.simulationSystem.DemandDistribution;

            foreach (var demand in DemandDistributions)
            {
                if (rand >= demand.MinRange && rand <= demand.MaxRange)
                    return demand.Value;
            }
            return 0;
        }
        public void CalcPerformanceMeasures()
        {

            Program.simulationSystem.PerformanceMeasures.EndingInventoryAverage = 
                Convert.ToDecimal( Program.simulationSystem.SimulationCases
                .Average(e => e.EndingInventory));
            Program.simulationSystem.PerformanceMeasures.ShortageQuantityAverage =
                Convert.ToDecimal(Program.simulationSystem.SimulationCases
                .Average(e => e.ShortageQuantity));
        }
    }

}
