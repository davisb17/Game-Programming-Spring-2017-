using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public partial class Engine : Form
    {
        public static Form form;
        public static Thread renderThread;
        public static Thread updateThread;
        public static Sprite canvas = new Sprite();
        public static int target_fps = 30;

        public Engine()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;

            renderThread = new Thread(new ThreadStart(RenderLoop));
            renderThread.Start();

            updateThread = new Thread(new ThreadStart(UpdateLoop));
            updateThread.Start();
        }

        public static void RenderLoop()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / target_fps);
            Thread.Sleep(500);
            while (true)
            {
                DateTime temp = DateTime.Now;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;

                form.Invoke(new MethodInvoker(form.Refresh));
            }
        }

        public static void UpdateLoop()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / target_fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;
                
                canvas.Update();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            renderThread.Abort();
            updateThread.Abort();
            base.OnClosed(e);
        }

        protected override void OnResize(EventArgs e)
        {
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            canvas.Render(e.Graphics);
        }
    }
}