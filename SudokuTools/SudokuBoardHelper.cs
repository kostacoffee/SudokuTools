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
    /// Method container to access the generation and solving of sudoku boards/puzzles
    /// </summary>
    public class SudokuBoardHelper
    {

        private delegate bool SkipCondition(int[,] board, int row, int col);

        /// <summary>
        /// Generates a 2D array of size (9,9)
        /// with values meeting all rules of a standard Sudoku game.
        /// Uses LINQ statments for optimisation.
        /// </summary>
        /// <returns></returns>
        public static int[,] generatePuzzle()
        {
            int[,] board = new int[9, 9];
            makeBoard(board, 0, 0, generatorCondition);
            return board;
        }

        /// <summary>
        /// Fills the incomplete 2D array
        /// with values meeting all rules of a standard Sudoku game.
        /// Uses LINQ statments for optimisation.
        /// </summary>
        /// <returns></returns>
        public static int[,] solvePuzzle(int[,] board)
        {
            makeBoard(board, 0, 0, solverCondition);
            return board;
        }

        private static bool makeBoard(int[,] board, int row, int col, SkipCondition skipCondition)
        {
            //adjusting rows and columns to stay within board
            if (row == 9)
            {
                row = 0;
                col++;
            }

            //base case - column out of bounds
            if (col == 9) return true;

            //recursive case
            else
            {
                if (skipCondition(board, row, col)) return makeBoard(board, row + 1, col, skipCondition); // if the cell is skippable, ignore it and move onto the next cell
                int[] availableNums = SudokuMethods.getValidMoves(board, row, col);
                for (int i = 0; i < availableNums.Length; i++)
                {

                    board[row, col] = availableNums[i]; // fill cell with a valid value
                    if (makeBoard(board, row + 1, col, skipCondition)) return true; // if the rest of the puzzle is correct, then this cell is correct
                    board[row, col] = 0; // otherwise reset cell and try the next value 
                }
                return false;
            }
        }

        private static bool generatorCondition(int[,] board, int row, int col) { return false; }  // no cell can be skipped in the generation of a new puzzle

        private static bool solverCondition(int[,] board, int row, int col) { return board[row, col] != 0; } // cells already with a number can be skipped (assuming every number is legal)
    }
}
