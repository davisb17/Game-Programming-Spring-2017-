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

namespace Engine_Example
{
    public partial class Form1 : Form
    {
        Sprite main = new Sprite();

        public static Form form;
        public static Thread thread;

        public static int fps = 30;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            thread = new Thread(new ThreadStart(run));

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            thread.Abort();
        }

        public static void run()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                now = DateTime.Now;
                TimeSpan diff = now - last;

                double calcFPS = 1000.0 / diff.TotalMilliseconds;

                if (diff.Ticks<frameTime.Ticks)
                    Thread.Sleep((frameTime-diff).Milliseconds);
                
                form.Invoke(new MethodInvoker(form.Refresh));
                last = now;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            main.Render(e.Graphics);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Refresh();
        }
    }
}
