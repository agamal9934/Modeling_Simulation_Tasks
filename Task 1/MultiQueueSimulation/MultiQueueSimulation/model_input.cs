using MultiQueueModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiQueueSimulation
{
    public partial class model_input : Form
    {
        public int num_ser;
        public int num_stop;
        public int stop_cre;
        public int ser_meth;
        public static int test_case;

        public static int finish_time = 0;


        public model_input()
        {
            InitializeComponent();
        }
        private void model_input_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetNames(typeof(Enums.StoppingCriteria));

            comboBox2.DataSource = Enum.GetNames(typeof(Enums.SelectionMethod));
        }
        private void ntx_btn_Click(object sender, EventArgs e)
        {
        }

        private void ntx_btn_Click_1(object sender, EventArgs e)
        {

            num_ser = int.Parse(numSer.Text.ToString());
            num_stop = int.Parse(numStop.Text.ToString());
            test_case = int.Parse(testcase.Text.ToString());


            numSer.Text = "";
            numStop.Text = "";
            if (time_number.Enabled != false)
                finish_time = int.Parse(time_number.Text.ToString());

            Program.MainSystem.NumberOfServers = num_ser;
            Program.MainSystem.StoppingNumber = num_stop;
            //Program.MainSystem.StoppingCriteria = (Enums.StoppingCriteria)Enum.Parse(typeof(Enums.StoppingCriteria), comboBox1.Text.ToString());
            if (comboBox1.Text != "SimulationEndTime")
                Program.MainSystem.StoppingCriteria = Enums.StoppingCriteria.NumberOfCustomers;
            else
                Program.MainSystem.StoppingCriteria = Enums.StoppingCriteria.SimulationEndTime;
            
            Program.MainSystem.SelectionMethod = (Enums.SelectionMethod)Enum.Parse(typeof(Enums.SelectionMethod), comboBox2.Text.ToString());
           // MessageBox.Show("Selection: " + Program.MainSystem.SelectionMethod);
            //Program.MainSystem.SelectionMethod = Enums.SelectionMethod.HighestPriority;

            //MessageBox.Show(Program.MainSystem.StoppingCriteria.ToString());

            distruputiontime fm = new distruputiontime(test_case);
            fm.Show();
            //this.Close();



        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "SimulationEndTime")
            {
                time_number.Enabled = false;
            }
            else
            {
                time_number.Enabled = true;
            }
        }
    }
}