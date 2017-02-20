using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace SpriteStuff
{
    public partial class Engine : Form
    {
        public static Form form;
        public static Thread thread;
        public static int target_fps = 30;
        public static double fps = 30.0;
        public static long start = DateTime.Now.Ticks;
        public static List<Sprite> sprites = new List<Sprite>();
        public static Sprite canvas;

        public Engine()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            canvas = new Sprite();
            
            Image monkeyImg = new Bitmap(Properties.Resources.monkey);
            ImageSprite monkey = new ImageSprite(monkeyImg);
            canvas.Add(monkey);



            thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        public static void Run()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / target_fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                fps = .9 * fps + .1 * (1000.0 / (temp - now).TotalMilliseconds);
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;
                
                form.Invoke(new MethodInvoker(form.Refresh));
            }
        }
        
        protected override void OnClosed(EventArgs e)
        {
            thread.Abort();
            base.OnClosed(e);
        }

        protected override void OnResize(EventArgs e)
        {
            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            canvas.Render(e.Graphics);
            Font drawFont = new Font("Ubuntu", 10);
            e.Graphics.DrawString(("FPS: " + fps), drawFont, Brushes.Black, 0, 0);
        }

    }

}