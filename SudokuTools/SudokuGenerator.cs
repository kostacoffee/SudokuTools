using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTools
{

    internal class SudokuGenerator
    {
        private static Random r = new Random();

        static void Main(string[] args)
        {
            int[,] board = generatePuzzle();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    Console.Write(board[i, j].ToString() + " ");
                Console.Write("\n");
            }
            Console.Read();
        }

        /// <summary>
        /// Generates a random 9x9 2D array of integers between 1 and 9 that meets all criteria for a solved Sudoku board
        /// </summary>
        /// <returns></returns>
        public static int[,] generatePuzzle()
        {
            int[,] board = new int[9, 9];
            randomizePuzzle(ref board, 0, 0);
            return board;
        }

        private static bool randomizePuzzle(ref int[,] board, int row, int col)
        {
            if (row == 9)
                return true;
            else
            {
                int[] availableNums = Enumerable.Range(1, 9).OrderBy(index => r.Next()).ToArray();
                for (int i = 0; i < 9; i++)
                {
                    if (SudokuMethods.legalMove(board, row, col, availableNums[i]))
                    {
                        board[row, col] = availableNums[i];
                        int nextRow = row;
                        int nextCol = col + 1;
                        if (nextCol == 9)
                        {
                            nextCol = 0;
                            nextRow++;
                        }
                        if (randomizePuzzle(ref board, nextRow, nextCol)) return true;
                        else board[row, col] = 0;
                    }
                }
                return false;
            }
        }
    }
}
