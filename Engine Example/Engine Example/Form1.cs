using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine_Example
{
    public partial class Form1 : Form
    {
        Sprite main = new Sprite();


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            //UpdateSize();
            Box b = new Box();
            b.X = 100;
            b.Y = 100;
            main.Add(b);
            Box b2 = new Box();
            b2.X = 5;
            b2.Y = 5;
            b2.Scale = .2;
            b.Add(b2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.DrawRectangle(Pens.Black, 0, 0, 100, 100);
            main.Render(e.Graphics);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Refresh();
        }
    }
}
