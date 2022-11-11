using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueTesting;

namespace MultiQueueSimulation
{
    public partial class PerformanceMeasure : Form
    {
        decimal totalsemulation = 
            Program.MainSystem.SimulationTable[Program.MainSystem.StoppingNumber - 1].EndTime;
        private int maxQueueLen;

        public PerformanceMeasure()
        {
            InitializeComponent();
        }

        public PerformanceMeasure(int maxQueueLen)
        {
            this.maxQueueLen = maxQueueLen;
            InitializeComponent();
        }

        /*public void averageServiceTimeforEachServer()
        {
            for (int j = 0; j < Program.MainSystem.Servers.Count; j++)
            {
                decimal totalserviceTime = TotalServerTime(Program.MainSystem.Servers[j].ID);
                decimal totalnum = Totalnumberofcustomers(Program.MainSystem.Servers[j].ID);
                Program.MainSystem.Servers[j].AverageServiceTime = totalserviceTime / totalnum;
            }
        }*/
        public void averageServiceTimeforEachServer()
        {
            for (int j = 0; j < Program.MainSystem.Servers.Count; j++)
            {
                decimal totalserviceTime = TotalServerTime(Program.MainSystem.Servers[j].ID);
                decimal totalnum = Totalnumberofcustomers(Program.MainSystem.Servers[j].ID);
                if (totalnum != 0)
                {
                    Program.MainSystem.Servers[j].AverageServiceTime = totalserviceTime / totalnum;
                }
                else
                {
                    Program.MainSystem.Servers[j].AverageServiceTime = totalserviceTime;
                }
            }
        }


        public decimal Totalnumberofcustomers(int id)
        {
            decimal count = 0;
            for (int n = 0; n < Program.MainSystem.SimulationTable.Count; n++)
            {
                if (id == Program.MainSystem.SimulationTable[n].AssignedServer.ID)
                {
                    count++;
                }
            }
            return count;
        }

        public void utlizationforEachServer()
        {
            for (int j = 0; j < Program.MainSystem.Servers.Count; j++)
            {
                decimal totalspendTime = Total_TimeServerSpend_On_Calls(Program.MainSystem.Servers[j].ID);
                Program.MainSystem.Servers[j].Utilization = (decimal ) (totalspendTime / totalsemulation);
            }
        }

        
       /* public void GetUtlization(int id)
        {
            //  decimal TimeSpentInCalls = Program.MainSystem.SimulationTable.Where(e => e.AssignedServer.ID == id).Sum(e => e.ServiceTime);
            decimal TimeSpentInCalls = Program.MainSystem.SimulationTable.Last(e => e.AssignedServer.ID == id).EndTime
                - Program.MainSystem.SimulationTable.Last(e => e.AssignedServer.ID == id).StartTime;
            decimal RunTime = Program.MainSystem.SimulationTable.Last().EndTime;

            MessageBox.Show("id: " + id + "  RunTime : " + RunTime);
            Program.MainSystem.Servers[id].Utilization = TimeSpentInCalls / RunTime;
        }*/

        public decimal Total_TimeServerSpend_On_Calls(int id)
        {
            decimal result = 0; decimal start = 0;
            decimal maxvalue = 0;
            for (int k = 0; k < Program.MainSystem.SimulationTable.Count; k++)
            {
                if (id == Program.MainSystem.SimulationTable[k].AssignedServer.ID)
                {
                    if (maxvalue <= Program.MainSystem.SimulationTable[k].EndTime)
                    {
                        maxvalue = Convert.ToDecimal(Program.MainSystem.SimulationTable[k].EndTime);
                        result = maxvalue;
                    }
                }

            }
            for (int a = 0; a < Program.MainSystem.SimulationTable.Count; a++)
            {
                if (id == Program.MainSystem.SimulationTable[a].AssignedServer.ID)
                {
                    start = Convert.ToDecimal(Program.MainSystem.SimulationTable[a].StartTime);
                    break;

                }
            }
            decimal time = result - start;

            return time;
        }
        public decimal TotalServerTime(int id)
        {
            decimal SUM = 0;
            for (int i = 0; i < Program.MainSystem.StoppingNumber; i++)
            {
                if (Program.MainSystem.SimulationTable[i].AssignedServer.ID == id)
                {
                    SUM += Convert.ToDecimal(Program.MainSystem.SimulationTable[i].ServiceTime);
                }
            }
            return SUM;
        }
        public decimal TotalIdelTime(int id)
        {
            decimal totalservertime = TotalServerTime(id);
            return totalsemulation - totalservertime;
        }
        public void probabiltyofIdelforEachServer()
        {
            for (int i = 0; i < Program.MainSystem.Servers.Count; i++)
            {
                decimal totalideitime = TotalIdelTime(Program.MainSystem.Servers[i].ID);
                Program.MainSystem.Servers[i].IdleProbability = Convert.ToDecimal(totalideitime / totalsemulation);
            }
        }

        public void GetAvgWaitingTime()
        {

            decimal waitedCustomersTime = Program.MainSystem.SimulationTable.Sum(e => e.TimeInQueue);
            decimal Customers = Program.MainSystem.SimulationTable.Last().CustomerNumber;
            Program.MainSystem.PerformanceMeasures.AverageWaitingTime = waitedCustomersTime / Customers;
        }
        public void GetWaitingProbability()
        {
            decimal waitedCustomers = Program.MainSystem.SimulationTable.Where(e => e.TimeInQueue != 0).Count();
            decimal Customers = Program.MainSystem.SimulationTable.Last().CustomerNumber;
            Program.MainSystem.PerformanceMeasures.WaitingProbability = waitedCustomers / Customers;

        }
        public void GetMaxQueueLength()
        {
            //int Max = Program.MainSystem.SimulationTable.Max(e => e.TimeInQueue);
            Program.MainSystem.PerformanceMeasures.MaxQueueLength = maxQueueLen ;
        }

        private void PerformanceMeasure_Load(object sender, EventArgs e)
        {

            
            averageServiceTimeforEachServer();
            probabiltyofIdelforEachServer();
            //utlizationforEachServer();

            GetMaxQueueLength();
            GetWaitingProbability();
            GetAvgWaitingTime();


            textBox1.Text = Program.MainSystem.PerformanceMeasures.AverageWaitingTime.ToString();
            textBox2.Text = Program.MainSystem.PerformanceMeasures.WaitingProbability.ToString();
            textBox3.Text = Program.MainSystem.PerformanceMeasures.MaxQueueLength.ToString();
            foreach (var server in Program.MainSystem.Servers)
            {
                comboBox1.Items.Add(server.ID - 1 );

            }


        }

        public void GetUtlization(int id)
        {
            decimal TimeSpentInCalls = Program.MainSystem.SimulationTable.
                Where(e => e.AssignedServer.ID == id + 1).Sum(e => e.ServiceTime);
            decimal RunTime = Program.MainSystem.SimulationTable.Last().EndTime;
            Program.MainSystem.Servers[id].Utilization = TimeSpentInCalls / RunTime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(comboBox1.Text) ;
            GetUtlization(id);
            textBox4.Text = Program.MainSystem.Servers[id].IdleProbability.ToString();
            textBox5.Text = Program.MainSystem.Servers[id].AverageServiceTime.ToString();
            textBox6.Text = Program.MainSystem.Servers[id].Utilization.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string result = TestingManager.Test(Program.MainSystem, Constants.FileNames.TestCase2);
            MessageBox.Show(result);

        }
    }
}
