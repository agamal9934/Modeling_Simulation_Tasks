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
using MultiQueueTesting;



namespace MultiQueueSimulation
{
    public partial class MultiChannel : Form
    {
        public MultiChannel()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private List<SimulationCase> simulationCases = new List<SimulationCase>();

        public  List<SimulationCase> waitingCutomer = new List<SimulationCase>();

        private SimulationCase simCase;

        private void MultiChannel_Load(object sender, EventArgs e)
        {
            //int idel = 0;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Customer Num");
            dataTable.Columns.Add(" Randam arrivals");
            dataTable.Columns.Add(" Time between arrivals");
            dataTable.Columns.Add("clock time of arrivals");
            dataTable.Columns.Add(" Randam Digits of Service");
            dataTable.Columns.Add(" Start Time");
            dataTable.Columns.Add(" Service Time");
            dataTable.Columns.Add(" Time Service End");
            dataTable.Columns.Add(" Time in queue");
            dataTable.Columns.Add(" Serval Index");
            Random rnd = new Random();

            int CurrentSerIndex = 0;


            int cont; bool end_time = false;
            if (Program.MainSystem.StoppingCriteria == Enums.StoppingCriteria.NumberOfCustomers)
            { cont = Program.MainSystem.StoppingNumber; end_time = false; }
            else
            { cont = 99999999; end_time = true; }




            setALLSerEndTimeTo0();
            for (int i = 0; i < cont; i++)
            {
                simCase = new SimulationCase();
                simCase.CustomerNumber = i + 1;

                simCase.RandomService = rnd.Next(1, 101);
                if (i == 0)
                {
                    simCase.RandomInterArrival = 1;
                    simCase.InterArrival = 0;
                    simCase.ArrivalTime = 0;

                    if (Program.MainSystem.SelectionMethod == Enums.SelectionMethod.HighestPriority)
                    {
                        simCase.ServiceTime = getservicetime(simCase.RandomService, 0);
                        simCase.StartTime = 0;
                        simCase.EndTime = simCase.StartTime + simCase.ServiceTime;
                        simCase.TimeInQueue = 0;

                        Program.MainSystem.Servers[0].FinishTime = simCase.EndTime;
                        CurrentSerIndex = 0;
                        simCase.AssignedServer = Program.MainSystem.Servers[0];


                    }
                    else if (Program.MainSystem.SelectionMethod == Enums.SelectionMethod.Random)
                    {
                        int ranSerIndex = rnd.Next(0, Program.MainSystem.NumberOfServers);

                        simCase.ServiceTime = getservicetime(simCase.RandomService, ranSerIndex);
                        simCase.StartTime = 0;
                        simCase.EndTime = simCase.StartTime + simCase.ServiceTime;
                        simCase.TimeInQueue = 0;

                        Program.MainSystem.Servers[0].FinishTime = simCase.EndTime;
                        CurrentSerIndex = ranSerIndex;
                        simCase.AssignedServer = Program.MainSystem.Servers[ranSerIndex];


                    }

                }
                else
                {


                    simCase.RandomInterArrival = rnd.Next(1, 101);
                    simCase.InterArrival = gettimearrives(simCase.RandomInterArrival); ;
                    simCase.ArrivalTime = simulationCases[i - 1].ArrivalTime +
                    simCase.InterArrival;

                    Server ExitSer = new Server();
                    Server MinSer = new Server();
                    int serIndex = 0;
                    int MinSerIndex = 0;
                    bool found = false;
                    if (Program.MainSystem.SelectionMethod == Enums.SelectionMethod.HighestPriority)
                    {
                        int minSerEndTime = 999999999;
                        for (int j = 0; j < Program.MainSystem.NumberOfServers; j++)
                        {

                            if (simCase.ArrivalTime >= Program.MainSystem.Servers[j].FinishTime)
                            {
                                ExitSer = Program.MainSystem.Servers[j];
                                serIndex = j;
                                found = true;
                                break;
                            }
                            if (Program.MainSystem.Servers[j].FinishTime < minSerEndTime)
                            {
                                MinSer = Program.MainSystem.Servers[j];
                                MinSerIndex = j;
                                minSerEndTime = Program.MainSystem.Servers[j].FinishTime;
                            }
                                            

                        }

                        if (found == true)
                        {
                            simCase.ServiceTime = getservicetime(simCase.RandomService, serIndex);
                            simCase.StartTime = simCase.ArrivalTime;
                            simCase.EndTime = simCase.StartTime + simCase.ServiceTime;
                            simCase.TimeInQueue = 0;
                            simCase.AssignedServer = ExitSer;

                            Program.MainSystem.Servers[serIndex].FinishTime = simCase.EndTime;

                            CurrentSerIndex = serIndex;
                        }
                        else
                        {
                            simCase.ServiceTime = getservicetime(simCase.RandomService, MinSerIndex);
                            simCase.StartTime = MinSer.FinishTime;
                            simCase.EndTime = simCase.StartTime + simCase.ServiceTime;
                            simCase.TimeInQueue = simCase.StartTime - simCase.ArrivalTime;
                            simCase.AssignedServer = MinSer;
                            Program.MainSystem.Servers[MinSerIndex].FinishTime = simCase.EndTime;
                            CurrentSerIndex = MinSerIndex;
                            waitingCutomer.Add(simCase);
                        }



                    }
                    else if (Program.MainSystem.SelectionMethod == Enums.SelectionMethod.Random)
                    {

                        List<int> AvalibaleSer = new List<int>();
                        int minSerEndTime = 999999999;
                        for (int j = 0; j < Program.MainSystem.NumberOfServers; j++)
                        {

                            if (simCase.ArrivalTime >= Program.MainSystem.Servers[j].FinishTime)
                            {
                                AvalibaleSer.Add(j);
                            }

                            if (Program.MainSystem.Servers[j].FinishTime < minSerEndTime)
                            {
                                MinSer = Program.MainSystem.Servers[j];
                                MinSerIndex = j;
                                minSerEndTime = Program.MainSystem.Servers[j].FinishTime;
                            }

                        }
                        if (AvalibaleSer.Count > 0)
                        {
                            int index = rnd.Next(AvalibaleSer.Count);
                            CurrentSerIndex = AvalibaleSer[index];
                            Server ser = Program.MainSystem.Servers[CurrentSerIndex];
                            simCase.ServiceTime = getservicetime(simCase.RandomService, CurrentSerIndex);
                            simCase.StartTime = simCase.ArrivalTime;
                            simCase.EndTime = simCase.StartTime + simCase.ServiceTime;
                            simCase.TimeInQueue = simCase.StartTime - simCase.ArrivalTime;
                            simCase.AssignedServer = ser;
                            Program.MainSystem.Servers[CurrentSerIndex].FinishTime = simCase.EndTime;

                        }
                        else
                        {
                            simCase.ServiceTime = getservicetime(simCase.RandomService, MinSerIndex);
                            simCase.StartTime = MinSer.FinishTime;
                            simCase.EndTime = simCase.StartTime + simCase.ServiceTime;
                            simCase.TimeInQueue = simCase.StartTime - simCase.ArrivalTime;
                            simCase.AssignedServer = MinSer;
                            Program.MainSystem.Servers[MinSerIndex].FinishTime = simCase.EndTime;
                            CurrentSerIndex = MinSerIndex;

                        }


                    }
                }


                if (end_time && simCase.EndTime > model_input.finish_time)
                     break;
                
                simulationCases.Add(simCase);
                dataTable.Rows.Add(i + 1, simCase.RandomInterArrival, simCase.InterArrival, simCase.ArrivalTime,
                    simCase.RandomService, simCase.StartTime, simCase.ServiceTime, simCase.EndTime, simCase.TimeInQueue, CurrentSerIndex);

            }

            Program.MainSystem.SimulationTable = simulationCases;

            dataGridView1.DataSource = dataTable;






        }
        public int gettimearrives(int n)
        {


            for (int i = 0; i < Program.MainSystem.InterarrivalDistribution.Count; i++)
            {

                if (n <= Program.MainSystem.InterarrivalDistribution[i].MaxRange
                    && n >= Program.MainSystem.InterarrivalDistribution[i].MinRange)

                    return Program.MainSystem.InterarrivalDistribution[i].Time;

            }
            return 0;
        }

        public int getservicetime(int n, int index)
        {
            
            for (int i = 0; i < Program.MainSystem.Servers[index].TimeDistribution.Count; i++)
            {

                if (n <= Program.MainSystem.Servers[index].TimeDistribution[i].MaxRange
                    && n >= Program.MainSystem.Servers[index].TimeDistribution[i].MinRange)

                    return Program.MainSystem.Servers[index].TimeDistribution[i].Time;

            }
            return 0;
        }

        public void setALLSerEndTimeTo0()
        {
            foreach (var v in Program.MainSystem.Servers)
            {
                v.FinishTime = -1;
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            int MaxQueueLen = getMaxQueueLen();
            PerformanceMeasure p = new PerformanceMeasure(MaxQueueLen);
            p.Show();
        }

        public  int getMaxQueueLen()
        {
            int Max = -1;
            if (waitingCutomer.Count < 1)
            {
                Max = 0;
            }
            else
            {

                for (int i = 0; i < waitingCutomer.Count; i++)
                {
                    int tempMax = 1;
                    for (int j = i + 1; j < waitingCutomer.Count; j++)
                    {
                        if (waitingCutomer[i].StartTime > waitingCutomer[j].ArrivalTime)
                        {
                            tempMax++;
                        }
                    }

                    if (tempMax > Max) Max = tempMax;

                }
            }

            return Max;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Gragh gragh = new Gragh();
            gragh.Show();
        }
    }
}
