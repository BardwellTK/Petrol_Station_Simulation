using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;

namespace Petrol_Somewhat_Unlimited_Ltd.Custom_Controls
{
    public class Sheet
    {
        public List<Column> Column;
        public int ColumnWidth;
        private Size _size;
        private Point _point;
        public List<CustomButton> CustomButtonList;
        
        public Sheet(List<string> columnNameList, Size size)
        {
            _size = size;
            _point = new Point(0,0);
            Column = new List<Column>();
            ColumnWidth = 0;
            foreach (string s in columnNameList)
            {
                Column.Add(new Column(s));
            }
            InitialiseCustomButtons();


        }

        private void InitialiseCustomButtons()
        {
            //Initialise buttons for table control
            CustomButtonList = new List<CustomButton>();
            CustomButtonList.Add(new CustomButton(1, "⬆", new Point(_size.Width - 100 - 6, _size.Height / 2))); //100 gives a nice distance relative to right border //⬆⬇
            CustomButtonList.Add(new CustomButton(1, "⬇", new Point(CustomButtonList[0].Location.X, (CustomButtonList[0].Location.Y + 50 + 3))));//50 is button height at ratio 1
            CustomButtonList.Add(new CustomButton(1, "Export Sheet", new Point(CustomButtonList[1].Location.X, CustomButtonList[1].Location.Y + 50 + 9)));
            //set colours of the buttons
            var selectedButtonColors = new Color[3] { Color.FromArgb(87, 159, 96), Color.FromArgb(123, 189, 131), Color.FromArgb(55, 130, 64) };

            foreach (CustomButton button in CustomButtonList)
            {
                button.SetBackUpColour(selectedButtonColors);
                button.CustomButtonClicked += OnCustomButtonClicked;
                button.TurnOff();
            }
            CustomButtonList[2].TurnOn();
            OnDataChange();
        }

        public event EventHandler DataChange; //Used to alert loosely connected code about object events
        protected virtual void OnDataChange()
        {
            DataChange?.Invoke(this, EventArgs.Empty);
        }
        private void OnScrollUpClicked()
        {
            foreach (Column c in Column)
            {
                c.SelectedCell--;
                c.FillSelected();
            }
            CustomButtonStateCheck();
            OnDataChange();
            
        }

        private void OnCustomButtonClicked(object source, EventArgs e)
        {
            var button = (CustomButton) source;
            switch (CustomButtonList.IndexOf(button))
            {
                case 0:
                    OnScrollUpClicked();
                    break;
                case 1:
                    OnScrollDownClicked();
                    break;
                case 2:
                    OnSaveTabeClicked();
                    break;
            }
        }

        private void OnScrollDownClicked()
        {
            foreach (Column c in Column)
            {
                c.SelectedCell++;
                c.FillSelected();
            }
            CustomButtonStateCheck();
            OnDataChange();

        }

        public event EventHandler SaveTable; //Used to alert loosely connected code about object events
        protected virtual void OnSaveTable()
        {
            SaveTable?.Invoke(this, EventArgs.Empty);
        }
        private void OnSaveTabeClicked()
        {
            //save table to file
            OnSaveTable();
        }

        private void CustomButtonStateCheck()
        {
            //check each button to determine whether or not it should be enabled

            //Move up
            //SelectedCell
            //CellList
            if (Column[0].SelectedCell == 0)
            {
                CustomButtonList[0].TurnOff(); //Disable button, no more room to move up
            }
            else
            {
                CustomButtonList[0].TurnOn();
            }
            //Move Down
            //if selected cell == count-1 then turn off
            if (Column[0].SelectedCell == Column[0].CellList.Count - 1 || Column[0].CellList.Count <= 11)
            {
                CustomButtonList[1].TurnOff();
            }
            else
            {
                CustomButtonList[1].TurnOn();
            }
            //Save Table
            //always on (CustomButtonList[2])
            OnDataChange();
        }

        public void Paint(Graphics g)
        {
            //Paint the sheet
            var blackBrush = new SolidBrush(Color.FromArgb(63, 63, 63));
            g.FillRectangle(blackBrush, new Rectangle( _point, _size));
            //Paint the Column
            var variablePoint = new Point(_point.X + 6, _point.Y + 6);
            foreach (var column in Column)
            {
                column.Paint(g, variablePoint);
                variablePoint = new Point(variablePoint.X + column.Size.Width + 1, variablePoint.Y);
            }
        }

        public void InjectData(DataTable dataTable)
        {
            foreach (Column c in Column)
            {
                    c.Add(dataTable.Rows[dataTable.Rows.Count-1][c.Title].ToString());
                    //Console.WriteLine(row[c.ToString()]); is the basis of how to extract data from a row row[column].Tostring
            }
            CustomButtonStateCheck();
        }
    }
    }

