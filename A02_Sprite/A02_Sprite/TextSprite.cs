using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A02_Sprite
{
    public class TextSprite : Sprite
    {
        string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        Font font = new Font("Ubuntu", 10);

        public Font Font
        {
            get { return font; }
            set { font = value; }
        }

        Brush brush = Brushes.Black;

        public Brush Brush
        {
            get { return brush; }
            set { brush = value; }
        }

        public TextSprite(string text)
        {
            this.text = text;
        }

        public override void paint(Graphics g)
        {
            g.DrawString(text, font, brush, X, Y);
        }

        public override void act()
        {
            base.act();
        }
    }
}