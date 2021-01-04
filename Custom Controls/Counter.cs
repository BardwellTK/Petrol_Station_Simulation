using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petrol_Somewhat_Unlimited_Ltd.Custom_Controls
{
    public partial class Counter : UserControl
    {
        private readonly string _title;

        public Counter(string inputTitle, Point loc)
        {
            InitializeComponent();
            _title = inputTitle;
            GetCounter = 0; //initialise counter!
            Location = loc;
        }

        public int GetCounter { get; set; }

        public void UpdateString()
        {
            Label.Text = ($@"{_title}: {GetCounter}"); //set new text for label!
        }

        public void Update(int inCount)
        {
            GetCounter = inCount; //set new value for count
        }
    }
    } 
