using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Engine
{
    public class Program : Engine
    {
        public static PhysicsSprite player;
        public static PhysicsSprite[] blocks;
        bool right = false, left = false, up = false;


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                right = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                //down = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                left = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (!up)
                {
                    player.VY = -10;
                    player.Y -= 1;
                }
                up = true;
            }

            UpdateVelocities();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                right = false;
            }
            else if (e.KeyCode == Keys.Down)
            {
                //down = false;
            }
            else if (e.KeyCode == Keys.Left)
            {
                left = false;
            }
            else if (e.KeyCode == Keys.Up)
            {
                up = false;
            }

            UpdateVelocities();
        }

        protected void UpdateVelocities()
        {
            if (left && right) player.VX = 0;
            else if (left) player.VX = -10;
            else if (right) player.VX = 10;
            else player.VX = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //FixScale();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Bitmap img = Properties.Resources.thing;
            player = new PhysicsSprite(img);
            player.X = 50;
            player.Y = 50;
            blocks = new PhysicsSprite[5];
            
            for (int i=0; i<blocks.Length;i++)
            {
                blocks[i] = new PhysicsSprite(img);
                blocks[i].X = i * 200 + 200;
                blocks[i].Y = 450;
                blocks[i].Model = MotionModel.Static;
                canvas.QueueAdd(blocks[i]);

            }




            canvas.QueueAdd(player);

            Application.Run(new Program());
        }
    }
}