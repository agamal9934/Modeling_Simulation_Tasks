using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryModels;
using InventoryTesting;

namespace InventorySimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
         }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Day");
            dataTable.Columns.Add("Cycle");
            dataTable.Columns.Add("DayWithinCycle");
            dataTable.Columns.Add("BeginingInventory");
            dataTable.Columns.Add("Randam Digits for Demand");
            dataTable.Columns.Add("Demand");
            dataTable.Columns.Add("EndingInventory");
            dataTable.Columns.Add("ShortageQuantity");
            dataTable.Columns.Add("RandomLeadTime");
            dataTable.Columns.Add("LeadTime");
            dataTable.Columns.Add("OrderQty");
            //dataTable.Columns.Add("DaysUntilOrderArrives");


            SimulationTable table = new SimulationTable();

            List<SimulationCase> simulationTable = Program.simulationSystem.SimulationCases;

            foreach (var simCase in simulationTable)
            {
                dataTable.Rows.Add(simCase.Day, simCase.Cycle, simCase.DayWithinCycle, simCase.BeginningInventory
                    , simCase.RandomDemand, simCase.Demand, simCase.EndingInventory, simCase.ShortageQuantity, simCase.RandomLeadDays
                    , simCase.LeadDays ,simCase.OrderQuantity);
            }
            dataGridView1.DataSource = dataTable;
            table.CalcPerformanceMeasures();
            textBox1.Text = Program.simulationSystem.PerformanceMeasures
                .EndingInventoryAverage.ToString();
            textBox2.Text = Program.simulationSystem.PerformanceMeasures
                .ShortageQuantityAverage.ToString();

            string res = InventoryTesting.TestingManager.Test(Program.simulationSystem, InventoryTesting.Constants.FileNames.TestCase1);
            MessageBox.Show(res);
        }
    }
}
