using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewspaperSellerModels;
using NewspaperSellerTesting;

namespace NewspaperSellerSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

      

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileInput fileInput = new FileInput();
            fileInput.ReadFile();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Day");
            dataTable.Columns.Add("Rand for NewDay");
            dataTable.Columns.Add("Type of NewDay");

            dataTable.Columns.Add("Rand for Demand");
            dataTable.Columns.Add("Demand");


            dataTable.Columns.Add("Sales");
            dataTable.Columns.Add("Lost Profit");
            dataTable.Columns.Add("Scarp Profit");
            dataTable.Columns.Add("Dialy Profit");






            SimulationTable table = new SimulationTable();

            List<SimulationCase> simulationTable = Program.simulationSystem.SimulationTable;

            foreach (var value in simulationTable ){

                dataTable.Rows.Add(value.DayNo , value.RandomNewsDayType , value.NewsDayType
                    , value.RandomDemand , value.Demand   , value.SalesProfit ,
                    value.LostProfit , value.ScrapProfit , value.DailyNetProfit);
            }



            dataTable.Rows.Add("", "", ""
                , "", "", Program.simulationSystem.PerformanceMeasures.TotalSalesProfit,
                Program.simulationSystem.PerformanceMeasures.TotalLostProfit,
                Program.simulationSystem.PerformanceMeasures.TotalScrapProfit,
                Program.simulationSystem.PerformanceMeasures.TotalNetProfit);

                dataGridView1.DataSource = dataTable;


            MessageBox.Show(Program.simulationSystem.PerformanceMeasures.DaysWithMoreDemand.ToString() +"  \n"+
                   Program.simulationSystem.PerformanceMeasures.DaysWithUnsoldPapers.ToString());


        }

        private void button1_Click(object sender, EventArgs e)
        {
            MeasureForm measureForm = new MeasureForm();
            measureForm.Show();
        }
    }
}
