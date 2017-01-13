using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormExample
{
    public partial class Form1 : Form
    {
        int x;
        int y;

        //Keep drawing and changing states separate

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Refresh();
        }

        //Just drawing state - not changing state
        protected override void OnPaint(PaintEventArgs e)
        {

            //e.Graphics.DrawRectangle(Pens.Chocolate, new Rectangle(10, 10, e.ClipRectangle.Width - 20, e.ClipRectangle.Height - 20));

            e.Graphics.DrawRectangle(Pens.Chocolate, new Rectangle(x, y, ClientSize.Width - 2*x, ClientSize.Height - 2*y));
        }

        //Good for changing state
        protected override void OnMouseDown(MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            Refresh();
            base.OnMouseDown(e);
        }

    }
}
