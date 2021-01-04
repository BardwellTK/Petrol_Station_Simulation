using System;
using Petrol_Somewhat_Unlimited_Ltd.Custom_Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Petrol_Somewhat_Unlimited_Ltd.Vehicles;

namespace Petrol_Somewhat_Unlimited_Ltd
{
    public class CounterHandler
    {
        public List<Counter> Counter = new List<Counter>();
        readonly string[] _titles = { "Unleaded dispensed", "Diesel dispensed", "LPG dispensed", "Total earned", "Commission", "Vehicles serviced", "Vehicles in queue", "Failed to serve" };
        double _commission, _totalEarned;
        double[] _chargeRate = new double[3], _totalFuelDispensed = new double[3];

        public int Width;
        //public enum CounterEnum
        //{
        //    Unleaded,
        //    Diesel,
        //    Lpg,
        //    Earned,
        //    Comm,
        //    Served,
        //    Queue,
        //    Failed,
        //}

        public CounterHandler(int x, int y)
        {

            _totalEarned = 0;

            //totalDispensed
            _totalFuelDispensed[0] = 0; //Unleaded
            _totalFuelDispensed[1] = 0; //Diesel
            _totalFuelDispensed[2] = 0; //LPG

            //chargeRate
            _chargeRate[0] = 0.87; //Unleaded  (Order important, keep same as Vehicle.FuelType)
            _chargeRate[1] = 1.11; //Diesel
            _chargeRate[2] = 1.36; //LPG

            var xDef = x;
            for (int i = 0; i < _titles.Length; i++)
            {
                Counter.Add(new Counter(_titles[i], new Point(x,y)));
                Counter[i].Location = new Point(x, y);
                x += Counter[i].Width + 3;

                if (Counter.Count == 4)
                {
                    y += Counter[i].Height + 3;
                    x = xDef;
                }
            }

            Width = (Counter[0].Width * 4) + ( 5 * 3);

            var controlTimer = new Timer();
            controlTimer.Interval = 1000;
            controlTimer.Enabled = true;
            controlTimer.Tick += UpdateControls;
        }

        private void UpdateControls(object sender, EventArgs e)
        {
            foreach (Counter c in Counter)
            {
                c.UpdateString(); //Refresh the counter text every second
            }
        }

        public void NewControlValue(object source, EventArgs e)
        {
            var vh = (VehicleHandler) source;
            Counter[5].Update(vh.ServicedCount); //Served
            Counter[6].Update(vh.QueueCount); //Queue
            Counter[7].Update(vh.FailedCount); //Failed
        }
        private void NewControlValue(int i, int x)
        {
            Counter[i].Update(x); //Hand in new value of counter
        }
        private void NewControlValue()
        {
            Counter[3].Update((int)_totalEarned); //Earned
            Counter[4].Update((int)_commission); //Commision
        }


        public void UpdateValues(object source, EventArgs e)
        {
            /*The pump doesn't have to be referenced because
             we are only extracting vlaues from the pump, not
             managing the pump*/
            var p = (Pump) source; //Grab the pump
            var v = p.RemoveVehicle(); //Get the vehicle
            var i = 0;

            foreach (Vehicle.FuelType f in Enum.GetValues(typeof(Vehicle.FuelType)))
            {
                if (f == v.FType)
                {

                    _totalFuelDispensed[i] += p.Dispensed; //Update fuel dispensed
                    NewControlValue(i, (int) _totalFuelDispensed[i]); //Update counter for fuel dispensed
                    _totalEarned += _totalFuelDispensed[i] * _chargeRate[i]; //update total earned
                    break;
                }
                i++;
            }
            _commission = _totalEarned * 0.01;
            NewControlValue();
        }

    }
}
