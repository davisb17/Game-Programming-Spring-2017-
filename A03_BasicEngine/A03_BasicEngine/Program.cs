using System;
using System.Drawing;
using System.Windows.Forms;

namespace A03_BasicEngine
{
    public class Program : Engine
    {

        public static SlideSprite elephant;


        protected override void OnKeyDown(KeyEventArgs e)
        {
            //base.OnKeyDown(e);
            //Console.WriteLine("asdffasdf");
            if (e.KeyCode == Keys.Left) elephant.TargetX -= 30;
            if (e.KeyCode == Keys.Right) elephant.TargetX += 30;
            if (e.KeyCode == Keys.Up) elephant.TargetY -= 30;
            if (e.KeyCode == Keys.Down) elephant.TargetY += 30;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            elephant = new SlideSprite(Properties.Resources.elephant);
            elephant.TargetX = 100;
            elephant.TargetY = 100;
            elephant.Velocity = 5;
            Program.canvas.Add(elephant);
            Application.Run(new Program());
        }
    }
}