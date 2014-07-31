using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTools
{
    /* SudokuTools namespace used as toolset for Sobo no Sudoku game
    * Toolset created by Kosta Dunn
    * Game created by Jarrah Holt.
    */

    /// <summary>
    /// A set of generic methods useful for a functioning Sudoku game.
    /// </summary>
    public static class SudokuMethods
    {
        private static Random r = new Random();

        /// <summary>
        /// Determines whether a legal Sudoku move has been made for the given cell on the board.
        /// Note: Very efficient
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static bool legalMove(int[,] board, int row, int col, int move)
        {
            // Used to ignore all cells with 0's, since there are no cells with -1.
            if (move == 0) move = -1;

            // check "neighbours" for the current row and column - ignoring the current cell and any 0's
            for (int i = 0; i < 9; i++)
            {
                if ((i != col && board[row, i] == move) || (i != row && board[i, col] == move)) return false;
            }

            //determine the top left corner of the square the cell is in. Works due to C# int rounding towards 0
            int boxRowStart = (row / 3) * 3;
            int boxColStart = (col / 3) * 3;

            //check box neighbours
            for (int r = boxRowStart; r < boxRowStart + 3; r++)
                for (int c = boxColStart; c < boxColStart + 3; c++)
                    if (board[r, c] == move && board[r,c] != 0 && r != row && c != col) return false;
            return true;
        }


        /// <summary>
        /// Returns a list of all valid moves that could be made for the given cell on the board. 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static int[] getValidMoves(int[,] board, int row, int col){
            //get all "neighbours" - all the numbers that the current cell cannot have
            int[] boardRow = getRow(row, board);
            int[] boardCol = getCol(col, board);
            int[] boardBox = getBox(row, col, board);

            //eliminate "neighbours" from the list of choices and return
            return Enumerable.Range(1, 9).Where(num => !(boardRow.Contains(num) || boardCol.Contains(num) || boardBox.Contains(num)))
                                                        .OrderBy(index => r.Next()).ToArray();
        }

        //Functions for retrieving "neighbours"
        private static int[] getRow(int row, int[,] board)
        {
            int[] boardRow = new int[9];
            for (int i = 0; i < 9; i++)
                boardRow[i] = board[row, i];
            return boardRow;
        }

        private static int[] getCol(int col, int[,] board)
        {
            int[] boardCol = new int[9];
            for (int i = 0; i < 9; i++)
                boardCol[i] = board[i, col];
            return boardCol;
        }

        private static int[] getBox(int row, int col, int[,] board)
        {
            int[] boxValues = new int[9];
            int boxRowStart = (row / 3) * 3; // works due to C# int rounding towards 0
            int boxColStart = (col / 3) * 3;
            int i = 0;
            for (int r = boxRowStart; r <= boxRowStart + 2; r++)
                for (int c = boxColStart; c <= boxColStart + 2; c++)
                {
                    boxValues[i] = board[r, c];
                    i++;
                }
            return boxValues;
        }
    }
}
