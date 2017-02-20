using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A01_StressTest
{
    public partial class Form1 : Form
    {
        public static readonly double set_FPS = 30.0;

        public static Form form;
        public static Thread timeThread;

        private static int s = 0;
        private static double calcFPS;

        private static List<int> xPos = new List<int>();
        private static List<int> yPos = new List<int>();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;

            timeThread = new Thread(new ThreadStart(Time));
            timeThread.Start();
            
        }
        
        public static void Time()
        {
            DateTime current = DateTime.Now;
            DateTime last = DateTime.Now;
            TimeSpan diff = current - last;
            TimeSpan frameSpan = new TimeSpan((long)(10000000 / set_FPS));

            long frameTime = frameSpan.Milliseconds;

            while (true)
            {
                current = DateTime.Now;

                //Do stuff here

                diff = current - last;
                if (calcFPS < 100)
                    calcFPS = calcFPS * .90 + (1000.0 / diff.TotalMilliseconds) * .10;
                else
                    calcFPS = 1000.0 / diff.TotalMilliseconds;

                

                //Stop doing stuff
                if (diff.Milliseconds < frameTime)
                    Thread.Sleep((int)(frameTime - diff.Milliseconds));

                form.Invoke(new MethodInvoker(form.Refresh));

                last = current;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DateTime now = DateTime.Now;
            Random rand = new Random();

            float shake = 5 * (float)Math.Sin(now.Millisecond / 100);

            Font drawFont = new Font("Ubuntu", 10);
            e.Graphics.DrawString(("FPS: "+calcFPS), drawFont, Brushes.Black, 0,0);
            for (int i = 0; i < Math.Pow(2,s); i++)
            {
                if (xPos.Count <= i)
                {
                    xPos.Insert(i, rand.Next(20, 1000));
                    yPos.Insert(i, rand.Next(20, 500));
                }
                e.Graphics.DrawRectangle(Pens.Black, xPos.ElementAt(i), yPos.ElementAt(i), 100 + shake, 100 + shake);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            timeThread.Abort();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                s++;
            else if (e.KeyCode == Keys.Down)
                s--;
            base.OnKeyDown(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Refresh();
        }
    }
}
