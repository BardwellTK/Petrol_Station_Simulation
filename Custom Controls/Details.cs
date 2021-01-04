using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Petrol_Somewhat_Unlimited_Ltd.Vehicles;

namespace Petrol_Somewhat_Unlimited_Ltd.Custom_Controls
{
    public partial class Details : UserControl
    {
        
        public Point MyPoint;
        public TabControl MyTabControl;
        public int SelectedTab;
        private Color[] _bgbuttonColors, _selectedButtonColors;
        private static int ServicedID = 0, FailedID = 0;

        public Details(Size s)
        {
            Size = s;
            MyPoint = new Point(0,0); //myPoint is 0,0 because its relative to it's own graphics .'. 0,0
            BackColor = Color.FromKnownColor(KnownColor.Control); //gives a transparent look witout
            _bgbuttonColors = new Color[3] {Color.FromArgb(111, 172, 119),Color.FromArgb(137, 191, 145),Color.FromArgb(81, 149, 90) };
            MyTabControl = new TabControl(Location, s, new List<string>{"Serviced", "Failed"});
            foreach (var b in MyTabControl.ButtonList)
            {
                Controls.Add(b);
                b.SetSpecificColors(_bgbuttonColors);
                b.CustomButtonClicked += CustomButtonClicked;
            }

            SelectedTab = 0;
            _selectedButtonColors = new Color[3] {Color.FromArgb(87, 159, 96), Color.FromArgb(123, 189, 131), Color.FromArgb(55, 130, 64)};
            MyTabControl.ButtonList[SelectedTab].SetSpecificColors(_selectedButtonColors);
            MyTabControl.TabList[SelectedTab].Visible = true; //show this tab first (Queue)
            foreach (var tab in MyTabControl.TabList)
            {
                tab.BackColor = Color.FromArgb(87, 159, 96);
                tab.Paint += tab.PaintSheet;
                Controls.Add(tab);
            }

            

        }

        public void Details_Paint(object sender, PaintEventArgs e)
        {
                var g = e.Graphics;
                g.FillRectangle(new SolidBrush(Color.FromArgb(87, 159, 96)),
                    new Rectangle(new Point(MyPoint.X, MyPoint.Y + MyTabControl.ButtonList[0].Height), Size));
            

        }

        public void UpdateFailed(object source, VehicleQueueEventArgs e)
        {
            var v = e.Vehicle;
            var lp = v.Id;
            var vt = v.VType;
            var ft = v.FType;
            var dateTime = DateTime.Now;
            string s = $@"'{FailedID++}','{lp}','{vt}','{ft}','{dateTime:d}','{dateTime:T}'";
            MyTabControl.TabList[1].InjectData(s);
        }

        public void UpdateServiced(object source, VehicleServicedEventArgs e)
        {
            var pump = e.Pump;
            var pumpID = pump.PumpId;
            var fuelUsed = pump.Dispensed;
            var vehicle = pump.RemoveVehicle();
            var vehicleFuelType = vehicle.FType;
            double chargeRate = 0;
            if (vehicleFuelType == Vehicle.FuelType.Unleaded)
            {
                chargeRate = 0.87; //Unleaded
            }
            else if (vehicleFuelType == Vehicle.FuelType.Diesel)
            {
                chargeRate = 1.11; //Diesel
            }
            else if (vehicleFuelType == Vehicle.FuelType.Lpg)
            {
                chargeRate = 1.36; //Lpg
            }
            var fuelCost = Math.Round(Convert.ToDecimal((chargeRate * fuelUsed)), 2); ;
            var vehicleType = vehicle.VType;
            var licensePlate = vehicle.Id;

            var dateTime = DateTime.Now;
            string s = $@"'{ServicedID++}','{licensePlate}', '{vehicleType}', '{vehicleFuelType}', '{pumpID}', '{(int)fuelUsed}', '{fuelCost}', '{dateTime:d}', '{dateTime:T}'";
            MyTabControl.TabList[0].InjectData(s);
             

        }

        public void CustomButtonClicked(object source, EventArgs e) //When button is clicked, selected tab changeds
        {
            var bSource = (CustomButton) source;
            MyTabControl.ButtonList[SelectedTab].SetSpecificColors(_bgbuttonColors); //Switch off old selected tab button
            MyTabControl.TabList[SelectedTab].Visible = false; //Switch off old selected tab
            SelectedTab = MyTabControl.ButtonList.IndexOf(bSource);
            MyTabControl.ButtonList[SelectedTab].SetSpecificColors(_selectedButtonColors); //Switch on new selected tab button
            MyTabControl.TabList[SelectedTab].Visible = true; //Switch on new selected tab
            MyTabControl.TabList[SelectedTab].Refresh(); //Redraw graphics
            Refresh();
        }

    }

    
}

