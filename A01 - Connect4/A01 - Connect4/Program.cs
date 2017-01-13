using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A01___Connect4
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

        public int[,] Board {
            get { return board; }
        }

        public void MakeMove (int column)
        {
            for (int i = board[column].Length-1; i >0; i--)
            {
                if (board[column, i]!=0)
                {
                    board[column, i] = turn;
                    turn == 1 ? turn++ : turn--;
                    return;
                }
            }
        }

        public String toString();

        {
            string output = "";
            for (int i=0;i<board.Length;i++)
            {
                for (int j=0;j<board[0].Length;j++)
                {
                    if 
                    output+=
                }
            }
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
