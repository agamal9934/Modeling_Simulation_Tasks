using NewspaperSellerSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewspaperSellerModels
{

    // path E:\computer scince\fourth year\Mod-Sim\Labs\Lab3\Lab 3\NewspaperSellerSimulation_Students\NewspaperSellerSimulation\TestCases
    class FileInput
    {

        String path = @"D:\Faculty\Modeling and sim\task2\NewspaperSellerSimulation_Students\NewspaperSellerSimulation\TestCases\TestCase1.txt";
        string[] lines;
        public FileInput()
        {
            //Program.simulationSystem.

        }

        public int getNumOfNewspapers()
        {
            return int.Parse(lines[1]);

        }
        public int getNumOfRecords()
        {
            return int.Parse(lines[4]);

        }

        public decimal getPurchasePrice()
        {
            return decimal.Parse(lines[7]);

        }

        public decimal getScrapPrice()
        {
            return decimal.Parse(lines[10]);

        }

        public decimal getSellingPrice()
        {
            return decimal.Parse(lines[13]);

        }

        public List<DayTypeDistribution> getDayTypeDistributions()
        {
            double[] arr =  Array.ConvertAll(lines[16].Split(','), Double.Parse);
           // double cum = 1;
            List<DayTypeDistribution> dayTypeDistributions = new List<DayTypeDistribution>();

            DayTypeDistribution dayType;
            double[] cum = { arr[0], arr[0] + arr[1], arr[0] + arr[1] + arr[2] };
            Enums.DayType[] dayTypes = { Enums.DayType.Good, Enums.DayType.Fair, Enums.DayType.Poor };

            for (int i =0; i< arr.Length; i++)
            {
                dayType = new DayTypeDistribution();
                dayType.DayType = dayTypes[i];
                dayType.CummProbability= decimal.Parse( cum[i].ToString());
                dayType.Probability = decimal.Parse( arr[i].ToString() );
                if (i == 0)
                    dayType.MinRange = 1;
                    
                else
                {
                    dayType.MinRange = int.Parse(    (   cum[i - 1] * 100 + 1 ).ToString()  );
                }
                dayType.MaxRange = int.Parse(  (cum[i] * 100).ToString()  );

                dayTypeDistributions.Add(dayType);
            }
            return dayTypeDistributions;
        }
        public Dictionary<int, List<DayTypeDistribution>> getDayTypeDistributionsTable()
        {
            Dictionary<int, List<DayTypeDistribution>> demandDistributions
                 = new Dictionary<int, List<DayTypeDistribution>>();

            

            double[] cum = { 0, 0, 0 } ;
            Enums.DayType[] dayTypes = { Enums.DayType.Good , Enums.DayType.Fair , Enums.DayType.Poor };

            for (int i = 19; i < lines.Length; i++)
            {
                string line = lines[i];
                double[] DArr = Array.ConvertAll(line.Split(','), Double.Parse);
                int key = int.Parse(DArr[0].ToString());
                List<DayTypeDistribution> dayType = new List<DayTypeDistribution>();

                for (int j = 1; j <= 3; j++)
                {
                        DayTypeDistribution dayType1 = new DayTypeDistribution();

                    if (cum[j - 1]  != 1)
                    {
                        if (i == 19)
                        {
                            dayType1.MinRange = 1;
                        }
                        else
                        {
                            dayType1.MinRange = (int)(cum[j - 1] * 100) + 1;
                        }
                        dayType1.MaxRange = int.Parse(((cum[j - 1] + DArr[j]) * 100).ToString());
                    }
                        cum[j - 1] += DArr[j];

                        dayType1.DayType = dayTypes[j-1];
                        dayType1.Probability = decimal.Parse(DArr[j].ToString());

                        dayType1.CummProbability = decimal.Parse(cum[j - 1].ToString());
                    
                   

                    dayType.Add(dayType1);


                }

                demandDistributions.Add(key, dayType);
            }

            return demandDistributions;

        }
        public void ReadFile()
        {
            lines = System.IO.File.ReadAllLines(path);
       //     MessageBox.Show(lines[4]);
         //   MessageBox.Show(getNumOfRecords().ToString());

            string ss = "";
            foreach (string s in lines)
            {
                ss += "\n" + s;
            }


            Program.simulationSystem.NumOfRecords = getNumOfRecords();
            Program.simulationSystem.NumOfNewspapers= getNumOfNewspapers();
            Program.simulationSystem.PurchasePrice= getPurchasePrice();
            Program.simulationSystem.ScrapPrice= getScrapPrice();
            Program.simulationSystem.SellingPrice= getSellingPrice();
            Program.simulationSystem.DayTypeDistributions = getDayTypeDistributions();
            Program.simulationSystem.UnitProfit = Program.simulationSystem.SellingPrice
                                                - Program.simulationSystem.PurchasePrice;

            Dictionary<int, List<DayTypeDistribution>> dd = getDayTypeDistributionsTable();
            List<DemandDistribution> demandDistribution = new List<DemandDistribution>();
           
            foreach (KeyValuePair<int, List<DayTypeDistribution>> obj in dd)
            {
                DemandDistribution demandDistribution1 = new DemandDistribution();
                demandDistribution1.Demand = obj.Key;
                demandDistribution1.DayTypeDistributions = obj.Value;
                demandDistribution.Add(demandDistribution1);

            }
            Program.simulationSystem.DemandDistributions = demandDistribution;
            

          



        }

 
    }
}
