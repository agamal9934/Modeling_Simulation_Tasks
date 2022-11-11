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
    public partial class distruputiontime : Form
    {
        private int numRow = 4;
        public List<TimeDistribution> InterarrivalDistribution;
       
        public List<TimeDistribution> serviceDistribution;
        public List<Server> server=new List<Server>();
        String timeTitle;
        String col1;
        String col2;
        int count = 0;
        public int testcase;
        public distruputiontime(int test)
        {
            InitializeComponent();
            this.testcase = test;
        }
         public DataTable dataTable;
        private void DistruputionTime_Load(object sender, EventArgs e)
        {

            timeTitle = "Interarivall Destibution of calls";
            col1 = "InterArrival Time";
            col2 = "Probabilty";
            label1.Text = timeTitle;

            dataTable = new DataTable();
            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            if (testcase == 1)
            {
                dataTable.Rows.Add(1, 0.25);
                dataTable.Rows.Add(2, 0.4);
                dataTable.Rows.Add(3, 0.2);
                dataTable.Rows.Add(4, 0.15);
            }
            else
            {
                dataTable.Rows.Add(1, 0.9);
                dataTable.Rows.Add(2, 0.1);
            }



            dataGridView1.DataSource = dataTable;

            InterarrivalDistribution = new List<TimeDistribution>();



        }

        private void btn1_Click(object sender, EventArgs e)
        {

            numRow = dataGridView1.Rows.Count - 1;

            if (count == 0)
            {

               
                 dataGridView1.DataSource = dataTable;
                for (int rows = 0; rows < numRow; rows++)
                {
                    TimeDistribution x = new TimeDistribution();

                    string value1 = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                    string value2 = dataGridView1.Rows[rows].Cells[1].Value.ToString();

                    x.Time = int.Parse(value1);
                    x.Probability = decimal.Parse(value2);

                    if (rows == 0)
                    {
                        x.CummProbability = decimal.Parse(value2);
                        x.MinRange = 1;
                        x.MaxRange = Decimal.ToInt32(x.CummProbability * 100);
                       
                    }
                    else
                    {
                        x.CummProbability = decimal.Parse(value2)
                            + InterarrivalDistribution[rows - 1].CummProbability;

                        

                        x.MinRange = InterarrivalDistribution[rows - 1].MaxRange + 1;

                        x.MaxRange = Decimal.ToInt32(x.CummProbability * 100);
                      
                    }
                    InterarrivalDistribution.Add(x);
                    
                }
            }
            else
            {

              
                Server ser = new Server();

                for (int rows = 0; rows < numRow; rows++)
                {

                    TimeDistribution x = new TimeDistribution();

                    string value1 = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                    string value2 = dataGridView1.Rows[rows].Cells[1].Value.ToString();

                    x.Time = int.Parse(value1);
                    x.Probability = decimal.Parse(value2);

                    if (rows == 0)
                    {
                        x.CummProbability = decimal.Parse(value2);
                        
                        x.MinRange = 1;
                        x.MaxRange = Decimal.ToInt32(x.CummProbability * 100);
                        /*MessageBox.Show("Cumm: " + x.CummProbability.ToString() + "  Min: " +
                            x.MinRange + "  Max: " + x.MaxRange);*/
                    }
                    else
                    {
                        x.CummProbability = decimal.Parse(value2)
                            + ser.TimeDistribution[rows - 1].CummProbability;
                        
                        x.MinRange = ser.TimeDistribution[rows - 1].MaxRange + 1;

                        x.MaxRange = Decimal.ToInt32(x.CummProbability * 100);
                       /* MessageBox.Show("Cumm: " + x.CummProbability.ToString() + "  Min: " +
                            x.MinRange + "  Max: " + x.MaxRange);*/
                    }
                    ser.TimeDistribution.Add(x);


                }

                ser.ID = count;
                server.Add(ser);

            }


            if (count == Program.MainSystem.NumberOfServers)
            {

                Program.MainSystem.InterarrivalDistribution =
                    InterarrivalDistribution;
               

                Program.MainSystem.Servers = server;

                // this.Close();
                MultiChannel form = new MultiChannel();
                form.Show();
                return;

            }


            count++;

            ClearDataGridView();






        }

        public void ClearDataGridView()
        {
            timeTitle = "Service" + count.ToString() + " Time Destibution ";
            col1 = "Service Time";
            col2 = "Probabilty";
            label1.Text = timeTitle;

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);

            if (testcase == 1)
            {
                if (count == 1)
                {
                    dataTable.Rows.Add(2, 0.3);
                    dataTable.Rows.Add(3, 0.28);
                    dataTable.Rows.Add(4, 0.25);
                    dataTable.Rows.Add(5, 0.17);

                }
                else if (count == 2)
                {
                    dataTable.Rows.Add(3, 0.35);
                    dataTable.Rows.Add(4, 0.25);
                    dataTable.Rows.Add(5, 0.2);
                    dataTable.Rows.Add(6, 0.2);

                }
            }
            else
            {
                if (count == 1)
                {
                    dataTable.Rows.Add(1, 0.5);
                    dataTable.Rows.Add(2, 0.5);
                    
                }
                else if (count == 2)
                {
                    dataTable.Rows.Add(3, 0.1);
                    dataTable.Rows.Add(4, 0.9);
                    
                }
                else if (count == 3)
                {
                    dataTable.Rows.Add(5, 0.1);
                    dataTable.Rows.Add(6, 0.9);
                    
                }
                else if (count == 4)
                {
                    dataTable.Rows.Add(7, 0.1);
                    dataTable.Rows.Add(8, 0.9);
                    
                }
                else
                {
                    dataTable.Rows.Add(9, 0.1);
                    dataTable.Rows.Add(10, 0.9);
                }
            }



            dataGridView1.DataSource = dataTable;

        }

    }
}
