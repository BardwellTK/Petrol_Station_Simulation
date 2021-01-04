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
    public partial class CustomButton : UserControl
    {
        
        //Graphics
        private float _fontSize; //font sizeRatio
        private Graphics _g;
        private SizeF _stringSize;
        private SolidBrush _bgBrush; //background will host all background colours
        private SolidBrush _txtBrush; //text brush
        private Font _drawFont; //used for drawing text
        private Color[] _myColor, _myBackUpColor;


        private bool _graphicsAndClickState; //Graphics and functions enabled

        public string MyText { get; set; }

        public event EventHandler CustomButtonClicked; //Used to alert loosely connected code about object events

        public CustomButton(int sizeRatio, string text, Point p)
        {
            InitializeComponent();
            Init(sizeRatio, text, p); //Initialise variables
            _myColor = new Color[3] { Color.RoyalBlue, Color.CornflowerBlue, Color.MidnightBlue };
            _bgBrush = new SolidBrush(_myColor[0]);
            
        } //standard blue button constructor

        public CustomButton(int sizeRatio, string text, Point p, Color c) //secondary constructor for more available options
        {
            InitializeComponent();
            Init(sizeRatio, text, p); //Initialise variables

            //fetch the list of known colours
            //Organise them like in VS
            //Reason being, x is selected colour, x-1 is brighter, x+1 is brighter generally speaking...
            //PickNewColors(c);
            _bgBrush = new SolidBrush(_myColor[0]);
            Refresh();
        }

        private void Init(int sizeRatio, string text, Point p)
        {
            //set ratio of UI
            Width = sizeRatio * 100; //define object width
            Height = Width / 3; //define object height
            _fontSize = (float)(sizeRatio * 9.75); //define font sizeRatio
            Location = p;

            //Graphics assign settings
            MyText = text; //Assign text
            _stringSize = new SizeF();
            
            _txtBrush = new SolidBrush(Color.White);
            _drawFont = new Font("Corbel", (int)(_fontSize));

            //Optimal double buffer, causes notable delay between counter count and table count, but not by much.
            //Basically allows for rendering to be smoother by pre-calculating the graphics in a buffer before printing to screen
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            _myBackUpColor = new Color[3];
            _graphicsAndClickState = true;
        }

        protected virtual void OnCustomButtonClicked()
        {
            CustomButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        public void SetBackUpColour(Color[] color)
        {
            _myBackUpColor = color;
        }

        public void TurnOff()
        {
            //Grey out Colours
            var selectedButtonColors = new Color[3] { Color.FromArgb(117, 117, 117), Color.FromArgb(117, 117, 117), Color.FromArgb(117, 117, 117) };
            SetSpecificColors(selectedButtonColors);
            //
            _graphicsAndClickState = false;
            DefaultColour();
        }

        public void TurnOn()
        {
            //Grey out Colours
            SetSpecificColors(_myBackUpColor);
            //
            _graphicsAndClickState = true;
            DefaultColour();
        }

        public void SetSpecificColors(Color[] color)
        {
            //Takes 3 colors and sets them specifically
            _myColor[0] = color[0];
            _myColor[1] = color[1];
            _myColor[2] = color[2];
            DefaultColour();
        }

        private void CustomButton_MouseDown(object sender, MouseEventArgs e)
        {
            //set click colour
            ClickColour();
        }

        private void DefaultColour()
        {
            _bgBrush = new SolidBrush(_myColor[0]); //set new bg colour
            Refresh(); //re-draw graphics
        }

        private void HoverColour()
        {
            _bgBrush = new SolidBrush(_myColor[1]); //set new bg colour
            Refresh(); //re-draw graphics
        }

        private void ClickColour()
        {
            _bgBrush = new SolidBrush(_myColor[2]); //set new bg colour
            Refresh(); //re-draw graphics
        }

        private void CustomButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (_graphicsAndClickState)
            {
                DefaultColour(); //set default colour
                OnCustomButtonClicked();
            }
        }

        private void CustomButton_MouseEnter(object sender, EventArgs e)
        {
            HoverColour(); //set to hover colour
        }

        private void CustomButton_MouseLeave(object sender, EventArgs e)
        {
            DefaultColour(); //set back to default colour
        }

        private void CustomButton_Paint(object sender, PaintEventArgs e)
        {
            _g = e.Graphics; //deifne graphics
            _stringSize = _g.MeasureString(MyText, _drawFont); //define string sizeRatio
            if (_stringSize.Width > Width)
            {
                Width = (int) _stringSize.Width + 8;
            }
            _g.FillRectangle(_bgBrush, new Rectangle(0, 0, Width, Height)); //draw rectangle
            _g.DrawString(MyText, _drawFont, _txtBrush, 0 + (Width / 2) - (_stringSize.Width / 2), 0 + (Height / 2) - (_stringSize.Height / 2), new StringFormat()); //draw string
        }
    }
}
