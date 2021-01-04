using System;
using Petrol_Somewhat_Unlimited_Ltd.Vehicles;
using Petrol_Somewhat_Unlimited_Ltd.Custom_Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static Petrol_Somewhat_Unlimited_Ltd.Vehicles.Vehicle;

namespace Petrol_Somewhat_Unlimited_Ltd
{
    
    public partial class Pump : UserControl
    {

        //Pumps private variables
        ////Pumps property variables
        private int _timePassed;

        private string _vehicleId;
        private bool _available;
        private Timer _fuelTimer = new Timer(), _tickTimer = new Timer();
        private Vehicle _activeVehicle;
        private static int _iD = 1;
        private readonly double _rate;
        private double _percentage;
        private FuelType _vehFuelType;

        public event EventHandler PumpFinished;

        //Pumps constructor
        public Pump()
        {
            //Prepares the display
            InitializeComponent();

            //Changes some form data
            SetAvailable();
            PumpId = _iD++;
            l_Title.Text = $@"Pump {PumpId}:";
            //SetStatusFree();

            //Initialises Variables
            _vehicleId = "";
            _available = true;
            Finished = false;
            _rate = 1.5;
            _percentage = 0;
            Dispensed = 0;
            _fuelTimer.Interval = 18000; //was 18000
            _fuelTimer.Enabled = true;
            _fuelTimer.Tick += StopFuelPump;
            _fuelTimer.Stop(); //VERY IMPORTANT TO START UP!!!
            _tickTimer.Interval = 1000;
            _tickTimer.Stop();
            _tickTimer.Tick += TickTimer;
            _timePassed = 0;
        }

        //Pumps methods
        public bool FuelCompatible(FuelType vehFuel)
        {
            foreach (FuelType f in FType)
            {
                if (vehFuel == f)
                {
                    return true; //Fuel types compatible
                }
            }
            return false; //Fuel types not compatible
        }

        public void TickTimer(object myObject, EventArgs e)
        {
                _timePassed++;
                l_Timer.Text = $@"{_timePassed}s";
                Dispensed += _rate;
                PBAr.Refresh();
        }

        public void StartFuelPump()
        {
            var rand = new Random();
            _fuelTimer.Interval = rand.Next(17000,19000);
            _fuelTimer.Start();
            l_Status.Text = @"Fueling...";
            Dispensed = 0;
            _tickTimer.Start();
            
        }

        public void StopFuelPump(object myObject, EventArgs e)
        {
            _fuelTimer.Stop();
            _tickTimer.Stop();
            _timePassed = 0;
            _percentage = 0;
            Finished = true;
            OnPumpFinished();

        }

        public void AddVehicle( Vehicle vehicle)
        {
            if (_available)
            {
                _activeVehicle = vehicle;
                switch ((int)vehicle.VType)
                {
                    case 0:
                        pictureBox1.Image = Properties.Resources.carSymbol;
                        break;
                    case 1:
                        pictureBox1.Image = Properties.Resources.vanSymbol;
                        break;
                    case 2:
                        pictureBox1.Image = Properties.Resources.hgvSymbol;
                        break;
                }
                _vehicleId = _activeVehicle.Id;
                _vehFuelType = vehicle.FType;
                BoxLP.Text = $@"{vehicle.Id}";
                StartFuelPump();
                SetNotAvailable();
            }

        }

        public Vehicle RemoveVehicle()
        {
            Finished = false;
            SetAvailable();
            pictureBox1.Image = Properties.Resources.empty1;
            return _activeVehicle;//return the active vehicle
        }


        public void SetAvailable()
        {
            _available = true;
            UiAvailable();
        }

        public void SetNotAvailable()
        {
            _available = false;
            UiBusy();
        }

        public void UiBusy()
        {
            l_available.Text = "Not Available";
            l_available.ForeColor = Color.Firebrick;
            l_available.Location = new Point(115, 4);

            //Turn off button panel
            //freePanel.Visible = false;
        }

        private void UiAvailable()
        {
            l_available.Text = "Available";
            l_available.ForeColor = Color.RoyalBlue;
            l_available.Location = new Point(132, 4);
            l_Timer.Text = "0s";
            l_Status.Text = "Free";
            l_Percentage.Text = "0%";
            PBAr.Refresh();
            BoxLP.Clear();
            //Turn on button panel
            //freePanel.Visible = true;
        }

        /// <summary>
        /// -----------------------------------------------------------------
        /// </summary>
        //Pumps properties
        


        //C#7.0
        public bool Available { get => _available; }
        public string VehicleId{ get => _vehicleId; }
       

        private void ProgressPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush myBrush = new SolidBrush(Color.RoyalBlue);
            _percentage = 100 * ((double)(_timePassed * 1000) / (_fuelTimer.Interval));
            if (_percentage > 100)
            {
                _percentage = 100;
            }
            g.FillRectangle(myBrush, 0, 0, (int)(PBAr.Width*(_percentage/100)), PBAr.Height);
            l_Percentage.Text = Convert.ToString((int)_percentage) + "%";
        }

        public bool Finished { get; private set; }
        public double Dispensed { get; private set; }
        public int PumpId { get; }

        public List<FuelType> FType { get; } = new List<FuelType>{ FuelType.Diesel, FuelType.Lpg, FuelType.Unleaded };

        protected virtual void OnPumpFinished()
        {
            PumpFinished?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Lane
    {
        public List<Pump> Pump;
        public Lane()
        {
            Pump = new List<Pump> { new Pump(), new Pump(), new Pump() };
        }
    }

}
