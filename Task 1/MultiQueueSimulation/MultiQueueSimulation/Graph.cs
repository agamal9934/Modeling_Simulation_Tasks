using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace MultiQueueSimulation
{
    public partial class Gragh : Form
    {
        //   List<int> ServerID = new List<int>();
        public Gragh()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void showdata(int id)
        {
            // MessageBox.Show(Program.MainSystem.SimulationTable.Count.ToString());
            for (int j = 0; j < Program.MainSystem.SimulationTable.Count; j++)
            {
                if (id == Program.MainSystem.SimulationTable[j].AssignedServer.ID)
                {
                    for (int k = Program.MainSystem.SimulationTable[j].StartTime;
                        k < Program.MainSystem.SimulationTable[j].EndTime; k++)
                    {
                        chart1.Series["Busy Time"].Points.AddXY(k, 1);

                    }

                }
                else
                {
                    for (int n = Program.MainSystem.SimulationTable[j].StartTime;
                       n < Program.MainSystem.SimulationTable[j].EndTime; n++)
                    {
                        chart1.Series["Busy Time"].Points.AddXY(n, 0);

                    }
                }

            }

        }




        private void Gragh_Load_1(object sender, EventArgs e)
        {
            for (int i = 0; i < Program.MainSystem.Servers.Count; i++)
            {
                // ServerID.Add(Program.MainSystem.Servers[i].ID);
                comboBox1.Items.Add(Program.MainSystem.Servers[i].ID);

            }


            chart1.ChartAreas[0].AxisX.Minimum = 0;


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox1.Text.ToString());
            showdata(ID);

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            chart1.Series.Clear();
            chart1.Series.Add("Busy Time");

        }
    }
}
