using System;
using System.Collections.Generic;
using System.Drawing;

namespace Petrol_Somewhat_Unlimited_Ltd.Custom_Controls
{
    public class Column
    {
        public string Title;
        public Size Size;
        private static int _staticID = 0;
        private int _myID, index;
        public int SelectedCell;
        public List<Cell> SelectedCellList, CellList;

        public Column(string columnTitle)
        {
            Title = columnTitle;
            _myID = _staticID++;
            index = 0;
            CellList = new List<Cell>();
            SelectedCellList = new List<Cell>();
            SelectedCell = 0;
        }

        public void Add(string data)
        {
            CellList.Add(new Cell($"{_myID}{index++}",data));
            FillSelected();
        }

        public void FillSelected()
        {
            
                SelectedCellList.Clear();
            //i sets the literal index of _cellList to extract from our selected index, to the max (so no null referencing)
            //j sets the limit of SelectedCellList to 11, to nicely fit the screen
            //j could potentially be calculated, however, for a project of this size would take up..
            //an unecessary amount of disk space, code clutter, and processing time..
            //relative to what is already included ^ required, this type of extensive..
            //programming could be saved for a program of this size.
                for (int i = SelectedCell, j = 0; i < CellList.Count - 1 && j < 11; i++, j++)
                {
                    try
                    {
                        SelectedCellList.Add(CellList[i]);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        break;
                    }
                    catch (NullReferenceException)
                    {
                        break;
                    }
                }
            



        }

        public void Paint(Graphics g, Point p)
        {
                var drawFont = new Font("Corbel", 10);
                var txtBrush = new SolidBrush(Color.Black);
                var stringSize = g.MeasureString(Title, drawFont); //define string sizeRatio
                if (stringSize.Width < 50)
                {
                    stringSize.Width = 65;
                }
                Size = new Size((int) (stringSize.Width + 3), 25);
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(p, Size));
                g.DrawString(Title, drawFont, txtBrush, p.X, p.Y + 3, new StringFormat()); //draw string
                foreach (Cell cell in SelectedCellList)
                {
                    p = new Point(p.X, p.Y + 26);
                    cell.Paint(g, p, Size);
                }
        }
            
        }
    }

