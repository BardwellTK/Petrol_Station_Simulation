using System.Drawing;
public class Cell
    {
        public string ID;
        public string Data;
        public Cell(string givenID, string data)
        {
            ID = givenID;
            Data = data;
        }

        public void Paint(Graphics g, Point p, Size s)
        {
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(p, s));

            var drawFont = new Font("Corbel", 10);
            var txtBrush = new SolidBrush(Color.Black);
            var stringSize = g.MeasureString(Data, drawFont); //define string sizeRatio
            var Size = new Size((int)(stringSize.Width + 3), 25);
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(p, Size));
            g.DrawString(Data, drawFont, txtBrush, p.X, p.Y + 3, new StringFormat()); //draw string
        }
    }

