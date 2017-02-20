using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace A02_Sprite
{
    public partial class Engine : Form
    {
        public static Form form;
        public static Thread renderThread;
        public static Thread updateThread;
        public static TextSprite fpsShow;
        public static ImageSprite monkey;
        public static int target_fps = 30;
        public static double fps = 30.0;
        public static List<Sprite> sprites = new List<Sprite>();

        public Engine()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;

            Image img = new Bitmap("C:\\Users\\Brock\\Google Drive\\CompSci\\Game Programming\\A02_Sprite\\A02_Sprite\\Resources\\monkey.png");
            monkey = new ImageSprite(img);

            fpsShow = new TextSprite("test");

            renderThread = new Thread(new ThreadStart(Run));
            renderThread.Start();

            updateThread = new Thread(new ThreadStart(Update));
            updateThread.Start();
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

        public static void Update()
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

                fpsShow.Text = fps+"";
                monkey.act();
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
            monkey.render(e.Graphics);
            fpsShow.render(e.Graphics);
        }

    }

}