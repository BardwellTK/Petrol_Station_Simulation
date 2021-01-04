using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petrol_Somewhat_Unlimited_Ltd.Vehicles
{
    //public class ServiceFailedEventArgs : EventArgs
    //{
    //    public Vehicle Vehicle { get; set; }
    //}

    public class Vehicle
    {

        public enum VehicleType
        {
            Car,
            Van,
            Hgv,
        }

        public enum FuelType
        {
            Unleaded,
            Diesel,
            Lpg,
        }

        protected static int GlobalId = 0;
        Timer _serviceFailureTimer = new Timer();
        public int X, Y;

        public event EventHandler ServiceFailed;

        public Vehicle()
        {
            GenerateID();
            RandVehicle(); //generates random vehicle & fuel type
            var rand = new Random();
            _serviceFailureTimer.Interval = rand.Next(1000,2001);
            _serviceFailureTimer.Tick += SFTimer_Tick;
            _serviceFailureTimer.Enabled = true;
            _serviceFailureTimer.Start();
        }

        public void GenerateID()
        {
            var rand = new Random();
            var characters = new List<string>();
            var numbers = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                var num = rand.Next(65, 91);
                characters.Add($"{(char)num}");
            }

            for (int i = 0; i < 2; i++)
            {
                var num = rand.Next(48, 58);
                numbers.Add($"{(char)num}");
            }

            var output = $"{characters[0]}{characters[1]}{numbers[0]}{numbers[1]} {characters[2]}{characters[3]}{characters[4]}";
            Id = output;
        }

        public void Serviced()
        {
            _serviceFailureTimer.Stop();
        }

        public void RandVehicle()
        {
            var rand = new Random(); //new random
            //generate random fuel type and vehicle type
            VType = (VehicleType)rand.Next(0,3); //even though values go from 0-2 for type
            FType = (FuelType)rand.Next(0,3);    //0,3 is needed because random.next does [min to (max-1)]

            if ((int)VType == 0)
                MaxFuel = 40;
            else if ((int)VType == 1)
            {
                MaxFuel = 80;
            }
            else if ((int)VType == 2)
            {
                MaxFuel = 120;
            }
            CurFuel = rand.Next(1,MaxFuel+1); //1 <= curFuel < maxFuel
        }

        protected virtual void SFTimer_Tick (object sender, EventArgs e)
        {
            ServiceFailed?.Invoke(this, EventArgs.Empty);
        }
        
        //Property declaration
        //Get only, to keep data secure
        public VehicleType VType { get; private set; }
        public FuelType FType { get; private set; }
        public string Id { get; private set; }
        public int MaxFuel { get; private set; }
        public int CurFuel { get; private set; }
    }
}
