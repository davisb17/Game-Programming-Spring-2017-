using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug
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
            for (int row = board.GetUpperBound(0); row >=0; row--)
            {
                //Console.WriteLine(board[i, column]);
                if (board[row,col] == 0)
                {
                    //Console.WriteLine(column + " " + i);
                    board[row,col] = turn;
                    turn = turn == 1 ? turn + 1 : turn - 1;
                    //Console.WriteLine(turn);
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
            for (int i=0;i<4;i++)
            {
                if (board[row, col+i] != token)
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

        private int CheckDiagWin(int row, int col)
        {
            int token = board[row, col];
            for (int i = 0; i < 4; i++)
            {
                if (board[row + i, col + i] != token)
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
                        
                    //Checks for diag win if on top 3rowx4col sector
                    if (horiz && vert && CheckDiagWin(i, j) != 0)
                        return CheckDiagWin(i, j);
                        
                }
            }
            if (openSpace)
                return 0;
            else
                return -1;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running...");

            Connect4 game1 = new Connect4();

            Console.WriteLine(game1.ToString());

            Random rnd = new Random();
            while (game1.CheckWin()==0)
            {
                int col = rnd.Next(0, 7);
                game1.MakeMove(col);
            }

            Console.WriteLine(game1.CheckWin());
            Console.WriteLine(game1);

            Console.ReadKey();
        }
    }
}
