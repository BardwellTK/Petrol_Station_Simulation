using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Petrol_Somewhat_Unlimited_Ltd.Custom_Controls;

namespace Petrol_Somewhat_Unlimited_Ltd.Vehicles
{
    class VehicleHandler
    {

        //makes vehicles
        private readonly Timer _generator; //, _failCheck;
        private int _vehicleCount, _queueCount, _servicedCount, _failedCount;
        //public List<Vehicle> VehicleList, ServicedList, FailedList;

        //Listing Box
        public ListingBox QueueBox, ServicedBox, FailedBox;

        public event EventHandler VehicleCreated;
        public event EventHandler<VehicleQueueEventArgs> VehicleServiceFailed;
        public event EventHandler<VehicleServicedEventArgs> VehicleServiced;

        //Properties
        public int VehicleCount { get => _vehicleCount; set => _vehicleCount = value; }
        public int QueueCount { get => _queueCount; set => _queueCount = value; }
        public int ServicedCount { get => _servicedCount; set => _servicedCount = value; }
        public int FailedCount { get => _failedCount; set => _failedCount = value; }

        public VehicleHandler()
        {
            //Initialise Variables -------
            _vehicleCount = 0;
            _queueCount = 0;
            _servicedCount = 0;
            _failedCount = 0;

            //Create a timer
            var rand = new Random();
            _generator = new Timer {Interval = rand.Next(1500,2201)}; //Initial timer will be 1500 milliseconds
            _generator.Tick += Generator_Tick;
            _generator.Enabled = true;

            //Create Listing box's
            //Initialisation
            QueueBox = new ListingBox("Queue: ",120);
            ServicedBox = new ListingBox("Serviced: ",120);
            FailedBox = new ListingBox("Failed to serve: ",120);

            //Location
            var x = 3;
            var y = 3;
            QueueBox.Location = new Point(x, y);
            y += FailedBox.Height + 3;
            ServicedBox.Location = new Point(x,y);
            y += FailedBox.Height + 3;
            FailedBox.Location = new Point(x,y);
        }

        private void Generator_Tick(object sender, EventArgs e)
        {
            if (_queueCount >= 1000) return;
            //each timer tick creates a new car by adding it to the dynamic list
            var v = new Vehicle();
            QueueBox.Add(v);
            var rand = new Random();
            _generator.Interval = rand.Next(1500, 2201);
            _vehicleCount++;
            _queueCount = QueueBox.Count();
            QueueBox.GetVehicle(QueueBox.Count() - 1).ServiceFailed += ServiceFailed;
            OnVehicleCreated(v);
        }

        private void ServiceFailed(object sender, EventArgs e)
        {
            var v = (Vehicle)sender;
            v.Serviced();
            FailedBox.Add(v); //Add vehcile to failed list
            _failedCount = FailedBox.Count(); //increment failed count
            OnVehicleServiceFailed(v);
            Remove(v);

        }

        public void Remove(Vehicle v)
        {
            QueueBox.Remove(v);
            _queueCount = QueueBox.Count();
        }

        public void UpdateServiced(object source, EventArgs e)
        {
            var p = (Pump)source;
            var v = p.RemoveVehicle();

            //Extraxt the vehicle and increment!
            ServicedBox.Add(v);
            _servicedCount = ServicedBox.Count();
            OnVehicleServiced(p);
        }

        protected virtual void OnVehicleCreated(Vehicle vehicle)
        {
            VehicleCreated?.Invoke(this, new VehicleQueueEventArgs(){Vehicle = vehicle});
        }

        protected virtual void OnVehicleServiceFailed(Vehicle vehicle)
        {
            VehicleServiceFailed?.Invoke(this, new VehicleQueueEventArgs() { Vehicle = vehicle });
        }
        

        protected virtual void OnVehicleServiced(Pump pump)
        {
            VehicleServiced?.Invoke(this,new VehicleServicedEventArgs(){Pump = pump});
        }
    }
}
