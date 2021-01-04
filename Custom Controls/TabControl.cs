using System.Collections.Generic;
using System.Drawing;

namespace Petrol_Somewhat_Unlimited_Ltd.Custom_Controls
{
    public class TabControl
    {
        public List<CustomButton> ButtonList;
        public List<TabSheet> TabList;

        public TabControl(Point point, Size s, List<string> titleList)
        {
            var buttonSizeRatio = 1;
            ButtonList = new List<CustomButton>();
            TabList = new List<TabSheet>();

            //get x, y
            //place button at x,y
            var p = point;
            foreach (var t in titleList)
            {
                ButtonList.Add(new CustomButton(buttonSizeRatio, t, p));
                TabList.Add(new TabSheet(new Point( point.X + 3, point.Y + ButtonList[0].Height + 6),new Size(s.Width - 6, s.Height - ButtonList[0].Height - 12),t));
                p.X += ButtonList[titleList.IndexOf(t)].Width + 3;
            }

        }
    }

    
}

