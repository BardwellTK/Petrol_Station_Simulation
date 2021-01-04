using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using Petrol_Somewhat_Unlimited_Ltd.Vehicles;

namespace Petrol_Somewhat_Unlimited_Ltd.Custom_Controls
{
    public partial class ListingBox : UserControl
    {
        public List<Vehicle> List;

        public ListingBox(string title, int height)
        {
            List = new List<Vehicle>();
            InitializeComponent();
            label1.Text = title;
            Height = height;
            listBox1.Height = height - listBox1.Location.Y - 3;
        }

        public void Add(Vehicle v)
        {
            listBox1.Items.Add(v.Id);
            List.Add(v);
        }

        public Vehicle GetVehicle(int i)
        {
            return List[i];
        }

        public int Count()
        {
            return List.Count;
        }

        public void Remove(int i)
        {
            listBox1.Items.RemoveAt(i);
            List.RemoveAt(i);
        }

        public void Remove(Vehicle v)
        {
            listBox1.Items.Remove(v.Id);
            List.Remove(v);
        }

    }
}
