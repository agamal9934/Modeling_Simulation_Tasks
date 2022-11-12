using NewspaperSellerTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewspaperSellerSimulation
{
    public partial class MeasureForm : Form
    {
        public MeasureForm()
        {
            InitializeComponent();
        }

        private void MeasureForm_Load(object sender, EventArgs e)
        {
            label9.Text = 
                Program.simulationSystem.PerformanceMeasures.TotalSalesProfit.ToString();

            label10.Text =
                Program.simulationSystem.PerformanceMeasures.TotalCost.ToString();
           
            label11.Text =
                Program.simulationSystem.PerformanceMeasures.TotalLostProfit.ToString();

            label12.Text =
                Program.simulationSystem.PerformanceMeasures.TotalScrapProfit.ToString();

            label13.Text =
                Program.simulationSystem.PerformanceMeasures.TotalNetProfit.ToString();

            label14.Text =
                Program.simulationSystem.PerformanceMeasures.DaysWithMoreDemand.ToString();
            
            label15.Text =
                Program.simulationSystem.PerformanceMeasures.DaysWithUnsoldPapers.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = TestingManager.Test(Program.simulationSystem, Constants.FileNames.TestCase1);
            MessageBox.Show(result);
        }
    }
}
