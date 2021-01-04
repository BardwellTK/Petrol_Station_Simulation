using Petrol_Somewhat_Unlimited_Ltd.Vehicles;
using Petrol_Somewhat_Unlimited_Ltd.Custom_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Petrol_Somewhat_Unlimited_Ltd
{

    public partial class Home : Form
    {
        //Custom Data Types ------------------------------
        private CounterHandler _counterHandler;
        private VehicleHandler _vehicleHandler;
        public CustomButton DetailsButton, ExitButton;
        public List<Lane> Lane;
        public Details Details;
        //Basic Data Types --------------------------------
        public List<Size> ConstSizes = new List<Size> { new Size(163, 120), new Size(150, 50), new Size(160, 40), new Size(901, 40), new Size(3, 3), new Size(185, 120), new Size(20, 20) };
        
        public Home()
        {
            InitializeComponent();
            InitCounters(); //creates Counters  {Dependencies: 0}
            InitVehicles(); //creates Vehicles  {Dependencies: 1 [Counter._counterHandler]}
            InitPumps(); //creates Pumps        {Dependencies: 2 [Counter._counterHandler], [Vehicle._vehicleHandler]}

            _vehicleHandler.VehicleCreated += Loop_Tick; //Add method Loop_Tick() to event _vehicleHandler.VehicleCreated

            //Init Panels
            var panelWidth = Lane[0].Pump[2].Location.X + Lane[0].Pump[2].Width + 3;
            panel2.Visible = false;
            panel1.Size = new Size(panelWidth, (Lane[0].Pump[0].Height * 3) + (4 * 3)); //501//panel1.Height);
            AutoSize = true; //Autosize enabled
            panel2.Size = new Size((int)(panel1.Width * 1.4), panel1.Height);
            AutoSizeMode = AutoSizeMode.GrowAndShrink; //Enables the form to grow and shrink
            panel1.Location = new Point((Width / 2) - (panelWidth / 2), 3);
            panel2.Location = new Point(6, panel1.Location.Y);

            //Init Details
            Details = new Details(panel2.Size);
            panel2.Controls.Add(Details);
            Details.Paint += Details.Details_Paint;
            _vehicleHandler.VehicleServiced += Details.UpdateServiced;
            _vehicleHandler.VehicleServiceFailed += Details.UpdateFailed;
            
            
            //Init CustomButtons
            var counter = _counterHandler.Counter[3]; //this is actually object #4, and will be the last counter in a line.
            DetailsButton = new CustomButton(1, "Details", (new Point(counter.Location.X + counter.Width + 3, counter.Location.Y)));
            DetailsButton.CustomButtonClicked += OnDetailsButtonClicked;
            ExitButton = new CustomButton(1, "Exit", (new Point(counter.Location.X + counter.Width + 3, counter.Location.Y + counter.Height + 3)));
            ExitButton.SetSpecificColors(new Color[]{Color.Firebrick,Color.IndianRed,Color.Brown});
            ExitButton.CustomButtonClicked += OnExitButtonClicked;
            Controls.Add(DetailsButton);
            Controls.Add(ExitButton);

        }

        /// <summary>
        /// Main methods -----------------------------------------------------------------
        /// </summary>


        private void Loop_Tick(object sender, EventArgs e)
        {
            var randomSelectPump = 0;
            var rand = new Random();
            var toRemove = new List<Vehicle>();
            var availablePump = new List<Pump>();
            var lane = new Lane();
            //auto assigns cars to free pumps
            if (_vehicleHandler.QueueCount > 0) //Only execute if vehicles are in queue
            {
                foreach (var v in _vehicleHandler.QueueBox.List)
                {
                    foreach (var l in Lane)
                    {
                        foreach (var p in l.Pump) // for all vehicles
                        {

                            if (p.Available) //if pump available and fuel compatible
                            {
                                if (p.FuelCompatible(v.FType))
                                {
                                    availablePump.Add(p);
                                    //lane = l;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (availablePump.Count > 0)
                    {
                        randomSelectPump = rand.Next(1, availablePump.Count);
                        foreach (var l in Lane)
                        {
                            foreach (var p in l.Pump)
                            {
                                if (p.PumpId == availablePump[randomSelectPump - 1].PumpId)
                                {
                                    v.Serviced();
                                    p.AddVehicle(v); //add vehicle to pump
                                    toRemove.Add(v); //add vehcles to list of waiting to be removed
                                }
                            }
                        }
                    }
                }
            }

            foreach (Vehicle v in toRemove)
            {
                _vehicleHandler.QueueBox.Remove(v);
            }
            toRemove.Clear();
        }
        
        /// <summary>
        /// Initialising methods -------------------------------------------------------
        /// </summary>
        private void InitVehicles()
        {
            //Initialise Vehicle Handler
            _vehicleHandler = new VehicleHandler();
            _vehicleHandler.VehicleCreated += _counterHandler.NewControlValue;
            _vehicleHandler.VehicleServiced += _counterHandler.NewControlValue;
            _vehicleHandler.VehicleServiceFailed += _counterHandler.NewControlValue;
        }

        private void InitPumps()
        {
            int xdef = 3, y = 3, x = xdef; //xdef (x default, the first point and kept as temp static value to reffer to)
            Lane = new List<Lane> {new Lane(), new Lane(), new Lane()}; //Create lanes (and therefore all pumps)
            foreach (Lane l in Lane)
            {
                foreach (Pump p in l.Pump)
                {
                    panel1.Controls.Add(p); //add pump to panel
                    p.Location = new Point(x, y); //set pump location
                    x += p.Width + 3; //adjust x coord
                    p.PumpFinished += _counterHandler.UpdateValues;
                    p.PumpFinished += _vehicleHandler.UpdateServiced;
                }
                x = xdef;
                y += l.Pump[0].Height + 3; //+3 for a gap between objects
            }

        }
        

        public void InitCounters()
        {
            //Create the counter handler
            _counterHandler = new CounterHandler(3, panel1.Height + 3 + 3);
            //Add each control to to the form and update it
            foreach (Counter c in _counterHandler.Counter)
            {
                Controls.Add(c);
            }

        }

        public void OnDetailsButtonClicked(object source, EventArgs e)
        {
            if (panel1.Visible)
            {
                panel1.Visible = false;
                panel2.Visible = true;
                DetailsButton.MyText = "Visuals";
                Details.Refresh();
            }
            else
            {
                panel1.Visible = true;
                panel2.Visible = false;
                DetailsButton.MyText = "Details";
            }
            DetailsButton.Refresh();

        }

        public void OnExitButtonClicked(object source, EventArgs e)
        {
            Application.Exit();
        }
        


    }

    

}

    


    

