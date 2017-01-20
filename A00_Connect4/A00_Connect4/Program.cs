using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A00_Connect4
{
    class Connect4
    {
        private int[,] board;
        private int turn;

        public Connect4()
        {
            this.board = new int[6, 7];
            this.turn = 1;
        }

        public int[,] Board
        {
            get { return board; }
        }

        public void MakeMove(int col)
        {
            for (int row = board.GetUpperBound(0); row >= 0; row--)
            {
                if (board[row, col] == 0)
                {
                    board[row, col] = turn;
                    turn = turn == 1 ? turn + 1 : turn - 1;
                    return;
                }
            }
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < board.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < board.GetUpperBound(1) + 1; j++)
                {
                    if (board[i, j] == 0)
                        output += "O";
                    else if (board[i, j] == 1)
                        output += "A";
                    else if (board[i, j] == 2)
                        output += "B";
                }
                output += '\n';
            }
            return output;
        }

        private int CheckHorizWin(int row, int col)
        {
            int token = board[row, col];
            for (int i = 0; i < 4; i++)
            {
                if (board[row, col + i] != token)
                    return 0;
            }
            return token;
        }

        private int CheckVertWin(int row, int col)
        {
            int token = board[row, col];
            for (int i = 0; i < 4; i++)
            {
                if (board[row + i, col] != token)
                    return 0;
            }
            return token;
        }

        private int CheckBackDiagWin(int row, int col)
        {
            int token = board[row, col];
            for (int i = 0; i < 4; i++)
            {
                if (board[row + i, col + i] != token)
                    return 0;
            }
            return token;
        }

        private int CheckFrontDiagWin(int row, int col)
        {
            int token = board[row, col];
            for (int i = 0; i < 4; i++)
            {
                if (board[row - i, col + i] != token)
                    return 0;
            }
            return token;
        }

        //0 if no win, 1 if player1, 2 if player 2, -1 if draw
        public int CheckWin()
        {
            Boolean openSpace = false;
            //Loop through points on the board
            for (int i = 0; i < board.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < board.GetUpperBound(1) + 1; j++)
                {
                    //Checks to see if there's still another space to play
                    if (board[i, j] == 0)
                        openSpace = true;


                    //Checks for win starting from top left piece

                    //Checks for horiz win if on top 3 rows
                    Boolean horiz = (j < 4);
                    //Checks for vert win if on left 4 cols
                    Boolean vert = (i < 3);

                    if (horiz && CheckHorizWin(i, j) != 0)
                        return CheckHorizWin(i, j);

                    if (vert && CheckVertWin(i, j) != 0)
                        return CheckVertWin(i, j);

                    //Checks for diag (top left to bottom right) win if on top 3rowx4col sector
                    if (horiz && vert && CheckBackDiagWin(i, j) != 0)
                        return CheckBackDiagWin(i, j);

                    //Checks for diag (bottom left to top right) win if on botom 3rowx4col sector
                    if (horiz && !vert && CheckFrontDiagWin(i, j) != 0)
                        return CheckFrontDiagWin(i, j);

                }
            }
            if (openSpace)
                return 0;
            else
                return -1;
        }

    }


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
