using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A04_Sokoban
{
    public class TextBox:Sprite
    {
        String text;
        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        Font font = new Font("Ubuntu", 40);
        public Font Font
        {
            get { return font; }
            set { font = value; }
        }

        Brush fontColor = Brushes.Black;
        public Brush FontColor
        {
            get { return fontColor; }
            set { fontColor = value; }
        }

        Brush bgColor = Brushes.White;
        public Brush BGColor
        {
            get { return bgColor; }
            set { bgColor = value; }
        }

        public TextBox(string text)
        {
            this.text = text;
        }

       public override void Paint(Graphics g)
       {
            g.FillRectangle(bgColor, 0, 0, g.VisibleClipBounds.Width, g.VisibleClipBounds.Height);
            g.DrawString(text, font, fontColor, 0, 0);
       }
    }
}
