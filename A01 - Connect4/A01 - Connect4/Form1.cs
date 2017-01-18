using System;
using System.Drawing;
using System.Windows.Forms;

namespace A01___Connect4
{
    public partial class Form1 : Form
    {
        int x;
        int height;
        int width;
        float cellWidth;
        float cellHeight;
        Boolean tooWide;
        float margin;

        Connect4 game;
        int gameWinner;

        public Form1()
        {
            game = new Connect4();
            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Refresh();
        }

        protected void DrawSpaces(PaintEventArgs e)
        {
            int[,] board = game.Board;

            if (tooWide)
            {
                for (int i = 0; i < board.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < board.GetUpperBound(1) + 1; j++)
                    {
                        if (board[i, j] == 0)
                        e.Graphics.FillEllipse(Brushes.White, margin + cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                        else if (board[i, j] == 1)
                            e.Graphics.FillEllipse(Brushes.Blue, margin + cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                        else if (board[i, j] == 2)
                            e.Graphics.FillEllipse(Brushes.DarkRed, margin + cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                    }
                }
            }
            else
            {
                
                for (int i = 0; i < board.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < board.GetUpperBound(1) + 1; j++)
                    {
                        if (board[i, j] == 0)
                            e.Graphics.FillEllipse(Brushes.White, cellWidth * j, margin + cellHeight * i, cellWidth, cellHeight);
                        else if (board[i, j] == 1)
                            e.Graphics.FillEllipse(Brushes.Blue, cellWidth * j, margin + cellHeight * i, cellWidth, cellHeight);
                        else if (board[i, j] == 2)
                            e.Graphics.FillEllipse(Brushes.DarkRed, cellWidth * j, margin + cellHeight * i, cellWidth, cellHeight);
                    }
                }

            }

            

        }

        //Just drawing state - not changing state
        protected override void OnPaint(PaintEventArgs e)
        {
            height = e.ClipRectangle.Height;
            width = e.ClipRectangle.Width;

            tooWide = (width * (6.0 / 7) > height);
            if (tooWide)
            {
                margin = (float)(Math.Abs(height * 7.0 / 6 - width) / 2);
                cellWidth = (width - 2 * margin) / 7;
                cellHeight = (float)(height / 6.0);
            }
            else
            {
                margin = (float)(Math.Abs(height - (width * 6.0 / 7)) / 2);
                cellWidth = (float)(width / 7.0);
                cellHeight = (height - 2 * margin) / 6;
            }


            if (gameWinner>0)
            {
                e.Graphics.FillRectangle(Brushes.Gray, 0, 0, width, height);
                Font drawFont = new Font("Ubuntu", cellHeight*3/4);
                e.Graphics.DrawString("Player " + gameWinner + " Wins", drawFont, Brushes.Black, 0,height/4+cellHeight);

            }

            else if (gameWinner<0)
            {
                e.Graphics.FillRectangle(Brushes.Gray, 0, 0, width, height);
                Font drawFont = new Font("Ubuntu", cellHeight * 3 / 4);
                e.Graphics.DrawString("Draw", drawFont, Brushes.Black, 0, height / 4 + cellHeight);

            }

            else if (tooWide)
            {
                e.Graphics.FillRectangle(Brushes.Chartreuse, margin, 0, (float)(height * 7.0 / 6), height);
                DrawSpaces(e);
            }

            else
            {
                e.Graphics.FillRectangle(Brushes.Chartreuse, 0, margin, width, (float)(width * 6.0 / 7));
                DrawSpaces(e);
            }
                

        }

        //Good for changing state
        protected override void OnMouseDown(MouseEventArgs e)
        {
            x = e.X;

            int col;
            
            if (gameWinner != 0)
            {
                gameWinner=0;
                game = new Connect4();
                Refresh();
                base.OnMouseDown(e);
                return;
            }

            if (tooWide)
            {
                col = (int)Math.Floor((x - margin) / cellWidth);
                if (col>=0 && col < 7)
                    game.MakeMove(col);
            }
            else
            {
                col = (int)(x/cellWidth);
                if (col >= 0 && col < 7)
                    game.MakeMove(col);
            }

            gameWinner = game.CheckWin();

            Refresh();
            base.OnMouseDown(e);
        }

    }
}
