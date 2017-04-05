using System;
using System.Threading;
using System.Windows.Forms;

namespace A04_Sokoban
{
    public class Program : Engine
    {
        
        public static SlideSprite player;
        public static SlideSprite[,] goals;
        public static SlideSprite[,] walls;
        public static SlideSprite[,] blocks;
        public static TextBox endScreen;
        public static int width;
        public static int height;
        public static int x;
        public static int y;
        public static int level = 0;
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (CanMoveTo(x + 1, y, 1, 0)) x++;
                if (blocks[x, y] != null)
                {
                    //First check to see if player is next to box
                    if (player.X != player.TargetX || player.Y != player.TargetY)
                    {
                        x--;
                        return;
                    }
                    MoveBlock(x, y, 1, 0);
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                if (CanMoveTo(x - 1, y, -1, 0)) x--;
                if (blocks[x, y] != null)
                {
                    if (player.X != player.TargetX || player.Y != player.TargetY)
                    {
                        x++;
                        return;
                    }
                    MoveBlock(x, y, -1, 0);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (CanMoveTo(x, y - 1, 0, -1)) y--;
                if (blocks[x, y] != null)
                {
                    if (player.X != player.TargetX || player.Y != player.TargetY)
                    {
                        y++;
                        return;
                    }
                    MoveBlock(x, y, 0, -1);
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (CanMoveTo(x, y + 1, 0, 1)) y++;
                if (blocks[x, y] != null)
                {
                    if (player.X != player.TargetX || player.Y != player.TargetY)
                    {
                        y--;
                        return;
                    }
                    MoveBlock(x, y, 0, 1);
                }
            }
            player.TargetX = x * 100;
            player.TargetY = y * 100;

            if (CheckWin() && endScreen==null)
            {
                endScreen = new TextBox("You beat the level!");
                canvas.Add(endScreen);
            }
            else if (endScreen!=null)
            {
                canvas.KillChildren();
                endScreen = null;
                level++;
                if (level == 1)
                    InitLevel(Properties.Resources.lvl1);
                else if (level == 2)
                    InitLevel(Properties.Resources.lvl2);
                else if (level == 3)
                    InitLevel(Properties.Resources.lvl3);
                else if (level == 4)
                    InitLevel(Properties.Resources.lvl4);
                else if (level == 5)
                    InitLevel(Properties.Resources.lvl5);
                else if (level == 6)
                    InitLevel(Properties.Resources.lvl6);
                else if (level == 7)
                    InitLevel(Properties.Resources.lvl7);
                else if (level == 8)
                    InitLevel(Properties.Resources.lvl8);
                else if (level == 9)
                    InitLevel(Properties.Resources.lvl9);
                else if (level == 10)
                    InitLevel(Properties.Resources.lvl10);
                else if (level == 11)
                    InitLevel(Properties.Resources.lvl11);
                else if (level == 12)
                    InitLevel(Properties.Resources.lvl12);
                else
                    InitLevel(Properties.Resources.lvl13);
            }
        }
        
        public void MoveBlock(int i, int j, int dx, int dy)
        {
            
            blocks[i + dx, j + dy] = blocks[i, j];
            blocks[i, j] = null;

            blocks[i + dx, j + dy].TargetX = (i + dx) * 100;
            blocks[i + dx, j + dy].TargetY = (j + dy) * 100;
            if (goals[i + dx, j + dy] != null) blocks[i + dx, j + dy].Image = Properties.Resources.final;
            else blocks[i + dx, j + dy].Image = Properties.Resources.box;
            
        }

        public Boolean CanMoveTo(int i, int j, int dx, int dy)
        {

            if (walls[i, j] == null && blocks[i, j] == null) return true;
            if (walls[i, j] != null) return false;
            if (blocks[i, j] != null && blocks[i + dx, j + dy] == null && walls[i + dx, j + dy] == null) return true;
            return false;

        }

        //returns true if won, false otherwise
        public Boolean CheckWin()
        {
            for (int i=0; i <height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (goals[j, i] != null && blocks[j, i] == null) return false;
                }
            }
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FixScale();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            FixScale();
        }
        
        private void FixScale()
        {
            float heightScale = (float)(ClientSize.Height / 100.0 / height);
            float widthScale = (float)(ClientSize.Width / 100.0 / width);

            
            canvas.Scale = Math.Min(heightScale, widthScale);
            canvas.X = (float)Math.Max(0, (ClientSize.Width - width * 100.0 * heightScale) / 2f);
            canvas.Y = (float)Math.Max(0, (ClientSize.Height - height * 100.0 * widthScale) / 2f);
        }

        public static void InitLevel(String level)
        {
            String[] lines = level.Split('\n');

            height = lines.Length-1;
            width = lines[0].Length-1; //because newline char adds to length
            
            goals = new SlideSprite[width, height];
            walls = new SlideSprite[width, height];
            blocks = new SlideSprite[width, height];

            //Add sprites to arrays and canvas
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (lines[j][i] == 'g' || lines[j][i] == 'B')
                    {
                        goals[i, j] = new SlideSprite(Properties.Resources.goal, i * 100, j * 100);
                        Program.canvas.Add(goals[i, j]);
                    }
                    if (lines[j][i] == 'w')
                    {
                        walls[i, j] = new SlideSprite(Properties.Resources.wall, i * 100, j * 100);
                        Program.canvas.Add(walls[i, j]);
                    }
                    if (lines[j][i] == 'b' || lines[j][i] == 'B')
                    {
                        blocks[i, j] = new SlideSprite(Properties.Resources.box, i * 100, j * 100);
                        if (lines[j][i] == 'B') blocks[i, j].Image = Properties.Resources.final;
                    }
                    if (lines[j][i] == 'c')
                    {
                        player = new SlideSprite(Properties.Resources.elephant, i * 100, j * 100);

                        x = i;
                        y = j;
                    }

                }

            }
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                    if (blocks[i, j] != null) Program.canvas.Add(blocks[i, j]);
            Program.canvas.Add(player);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            String map = Properties.Resources.lvl0;

            InitLevel(map);
            
            Application.Run(new Program());
        }
    }
}