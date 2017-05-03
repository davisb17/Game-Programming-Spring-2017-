using System;
using System.Drawing;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Program : Engine
    {
        public static readonly int BLOCK_MASK = 3;
        public static readonly int PCHAR_MASK = 1;
        public static readonly int ECHAR_MASK = 2;
        public static readonly int PBULL_MASK = 2;
        public static readonly int EBULL_MASK = 1;

        public static readonly int PLAYING = 0;
        public static readonly int LOSS = -1;
        public static readonly int WIN = 1;
        public static readonly int WAIT = 2;
        public static readonly int RESTART = -2;

        public static int height, width; //h and w of game field
        public static Character player_char;
        static List<Enemy> enemies;

        public static int level = 0;
        public static int gameState = LOSS;

        public static Thread winStateThread;

        bool right = false, left = false;

        public Program() : base()
        {
            MakeLevel();
            UpdateWindow();
            winStateThread = new Thread(new ThreadStart(WinStateLoop));
            winStateThread.Start();
        }
        
        public static void WinStateLoop()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / 2); //doesn't need to be fast
            while (true)
            {
                DateTime temp = DateTime.Now;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;

                UpdateGameState();
                if (gameState == PLAYING || gameState == WAIT) continue;
                if (gameState == WIN) level++;
                if (gameState == WIN || gameState == LOSS || gameState == RESTART)
                {
                    MakeStartScreen();
                    gameState = WAIT;
                }
            }

        }
        
        public static void ClearCanvas()
        {
            for (int i = 0; i < canvas.Children.Count; i++)
            {
                Sprite s = canvas.Children.ElementAt(i);
                canvas.QueueKill(s);
                try
                {
                    PhysicsSprite ps = (PhysicsSprite)s;
                    PhysicsSprite.sprites.Remove(ps);
                }
                catch (Exception) { }
            }
        }

        public static void UpdateGameState()
        {
            if (gameState == WAIT || gameState == RESTART) return;
            else if (!canvas.Children.Contains(player_char))
            {
                gameState = LOSS;
            }
            else
            {
                bool flag = false;
                foreach (Enemy e in enemies)
                    if (canvas.Children.Contains(e))
                    {
                        flag = true;
                        break;
                    }
                if (flag) gameState = PLAYING;
                else
                {
                    gameState = WIN;
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (gameState == WAIT)
            {
                MakeLevel();
                UpdateWindow();
                return;
            }
            else if (gameState == WIN || gameState == LOSS)
                return;

            if (e.KeyCode == Keys.R)
            {
                gameState = RESTART;
            }
            else if (e.KeyCode == Keys.D)
            {
                right = true;
            }
            else if (e.KeyCode == Keys.A)
            {
                left = true;
            }
            else if (e.KeyCode == Keys.W)
            {
                if (!player_char.InAir)
                {
                    player_char.VY = -25;
                    player_char.Y -= 5;
                }
            }

            UpdatePlayerMovement();
        }

        //Shoot
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (gameState == WAIT)
            {
                MakeLevel();
                UpdateWindow();
                return;
            }
            else if (gameState == WIN || gameState == LOSS)
                return;


            Bullet b = player_char.Shoot(Properties.Resources.PBullet, e.X / canvas.Scale, e.Y / canvas.Scale, 40f);
            canvas.QueueAdd(b);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.D)
            {
                right = false;
            }
            else if (e.KeyCode == Keys.A)
            {
                left = false;
            }

            UpdatePlayerMovement();
        }

        protected void UpdatePlayerMovement()
        {
            if (left && right) player_char.VX = 0;
            else if (left) player_char.VX = -10;
            else if (right) player_char.VX = 10;
            else player_char.VX = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateWindow();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateWindow();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            winStateThread.Abort();
        }
        
        //TODO: offset
        protected void UpdateWindow()
        {
            //Calculate window area ratio
            float aspect_ratio = height * 1.0f / width;
            float window_ratio = ClientSize.Height * 1.0f / ClientSize.Width;

            if (aspect_ratio > window_ratio) //Player area is skinnier than the window
            {
                //X will have an offset
                canvas.Scale = ClientSize.Height * 1.0f / height;
            }
            else
            {
                // Y will have an offset
                canvas.Scale = ClientSize.Width * 1.0f / width;
            }
        }

        protected static void MakeLevel()
        {
            ClearCanvas();

            string levelData;
            enemies = new List<Enemy>();

            if (level == 0)
                levelData = Properties.Resources.lvl0;
            else if (level == 1)
                levelData = Properties.Resources.lvl1;
            else if (level == 2)
                levelData = Properties.Resources.lvl2;
            else if (level == 3)
                levelData = Properties.Resources.lvl3;
            else if (level == 4)
                levelData = Properties.Resources.lvl4;
            else if (level == 5)
                levelData = Properties.Resources.lvl5;
            else if (level == 6)
                levelData = Properties.Resources.lvl6;
            else if (level == 7)
                levelData = Properties.Resources.lvl7;
            else
                levelData = Properties.Resources.lvl8;

            string[] lines = levelData.Split('\n');
            foreach (string line in lines)
                line.Trim();
            int h = Int32.Parse(lines[0].Split(' ')[0]);
            int w = Int32.Parse(lines[0].Split(' ')[1]);

            height = h * 120 - 20;
            width = w * 120 - 20;

            //blocks = new PhysicsSprite[h, w];
            for (int i = 0; i < h; i++)
            {
                string line = lines[i + 1];
                for (int j = 0; j < w; j++)
                {
                    int y = i * 120 + 50;
                    int x = j * 120 + 50;

                    if (line[j] == 'X')
                    {
                        PhysicsSprite block = new PhysicsSprite(Properties.Resources.Block, x, y)
                        {
                            Mask = BLOCK_MASK,
                            Model = MotionModel.Static
                        };
                        canvas.QueueAdd(block);
                    }

                    else if (line[j] == 'S')
                    {
                        Enemy slime = new Enemy(Properties.Resources.Slime, x, y)
                        {
                            Mask = ECHAR_MASK,
                            BulletMask = EBULL_MASK,
                            BulletSpeed = 15,
                            BulletImage = Properties.Resources.SlimeBullet,
                            ShootTicks = 45
                        };
                        enemies.Add(slime);
                    }

                    else if (line[j] == 'L')
                    {
                        Enemy lavaSlime = new Enemy(Properties.Resources.LavaSlime, x, y)
                        {
                            Mask = ECHAR_MASK,
                            BulletMask = EBULL_MASK,
                            BulletSpeed = 25,
                            BulletImage = Properties.Resources.LavaBullet,
                            ShootTicks = 30
                        };
                        enemies.Add(lavaSlime);
                    }

                    else if (line[j] == 'P')
                    {
                        player_char = new Character(Properties.Resources.PChar, x, y)
                        {
                            Mask = PCHAR_MASK,
                            BulletMask = PBULL_MASK,
                            Player = true
                        };
                        canvas.QueueAdd(player_char);
                    }
                }
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Target = player_char;
                canvas.QueueAdd(enemy);
            }

            gameState = PLAYING;
        }
        
        protected static void MakeStartScreen()
        {
            ClearCanvas();
            
            TextBox endScreen = new TextBox("Press Any Key to Continue");
            canvas.QueueAdd(endScreen);
        }
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        
            Application.Run(new Program());
        }
    }
}