using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryModels;


namespace InventorySimulation
{
    class ReadFile
    {
        String path = @"D:\Faculty\CS\Modeling and sim\task3\inventorysimulation\InventorySimulation\TestCases\TestCase1.txt";
        string[] lines;

        public int getOrderUpTo()
        {
            return int.Parse(lines[1]);

        }
        public int getReviewPeriod()
        {
            return int.Parse(lines[4]);

        }
        public int getStartInventoryQuantity()
        {
            return int.Parse(lines[7]);

        }
        public int getStartLeadDays()
        {
            return int.Parse(lines[10]);

        }
        public int getStartOrderQuantity()
        {
            return int.Parse(lines[13]);

        }
        public int getNumberOfDays()
        {
            return int.Parse(lines[16]);

        }
        public List<Distribution> getDemandDistribution()
        {
            List<Distribution> demand = new List<Distribution>();
            double[] cum = { 0, 0, 0, 0, 0 };
            double num = 0;
            int lastmax = 0;
            for (int i = 19; i < 24; i++)
            {
                Distribution row = new Distribution();
                string line = lines[i];
                double[] Rarr = Array.ConvertAll(line.Split(','), Double.Parse);
                row.Value = int.Parse(Rarr[0].ToString());
                row.Probability = decimal.Parse(Rarr[1].ToString());
                num += Rarr[1];
                
                if (i == 19)
                {
                    cum[0] = num;
                    row.MinRange = 1;
                    row.MaxRange = int.Parse(((cum[0]) * 100).ToString());
                }
                else
                {
                    cum[i - 19] = num;
                    row.MinRange = lastmax + 1;
                    row.MaxRange = int.Parse(((cum[i - 20] + Rarr[1]) * 100).ToString());
                }
                lastmax = row.MaxRange;
                row.CummProbability = decimal.Parse(cum[i - 19].ToString());
                

                //MessageBox.Show(row.Value.ToString() + " " + row.Probability.ToString() + " " + row.CummProbability.ToString()
                //    + " " + row.MinRange.ToString() + " " + row.MaxRange.ToString());
                //
                demand.Add(row);

            }
            
            return demand;

        }

        public List<Distribution> getLeadDaysDistribution()
        {
            List<Distribution> demand = new List<Distribution>();
            double[] cum = { 0, 0, 0};
            double num = 0;
            int lastmax = 0;
            for (int i = 26; i < lines.Length; i++)
            {
                Distribution row = new Distribution();
                string line = lines[i];
                double[] Rarr = Array.ConvertAll(line.Split(','), Double.Parse);
                row.Value = int.Parse(Rarr[0].ToString());
                row.Probability = decimal.Parse(Rarr[1].ToString());
                num += Rarr[1];

                if (i == 26)
                {
                    cum[0] = num;
                    row.MinRange = 1;
                    row.MaxRange = int.Parse(((cum[0]) * 100).ToString());
                }
                else
                {
                    cum[i - 26] = num;
                    row.MinRange = lastmax + 1;
                    row.MaxRange = int.Parse(((cum[i - 27] + Rarr[1]) * 100).ToString());
                }
                lastmax = row.MaxRange;
                row.CummProbability = decimal.Parse(cum[i - 26].ToString());


                /*MessageBox.Show(row.Value.ToString() + " " + row.Probability.ToString() + " " + row.CummProbability.ToString()
                    + " " + row.MinRange.ToString() + " " + row.MaxRange.ToString());*/
                demand.Add(row);

            }
            
            return demand;
        }

        public void readfile()
        {
            lines = System.IO.File.ReadAllLines(path);
            
            SimulationSystem simulationSystem = new SimulationSystem();
            simulationSystem.OrderUpTo = getOrderUpTo();
            simulationSystem.ReviewPeriod = getReviewPeriod();
            simulationSystem.StartInventoryQuantity = getStartInventoryQuantity();
            simulationSystem.StartLeadDays = getStartLeadDays();
            simulationSystem.StartOrderQuantity = getStartOrderQuantity();
            simulationSystem.NumberOfDays = getNumberOfDays();
            simulationSystem.DemandDistribution = getDemandDistribution();
            simulationSystem.LeadDaysDistribution = getLeadDaysDistribution();
            Program.simulationSystem = simulationSystem;
        }

    }
}
