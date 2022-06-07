using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIM_Queens.GameLogic
{
    internal class Validation
    {
        /// <summary>
        /// Checks if the Matrix is between 0 and 20
        /// </summary>
        /// <returns></returns>
        public static bool ValidateMatrixRowsAndCols(bool isTheMatrix = true, params int[] coords)
        {
            int rows = Map.Rows;
            int cols = Map.Cols;

            if (!isTheMatrix)
            {
                rows = coords[0];
                cols = coords[1];
                return (rows<=Map.Rows && cols<=Map.Cols);
            }

            return ((rows > 0 && rows <= 20) && (cols > 0 && cols <= 20));
        }

        public static int[] ValidateCoordinates(string coords)
        {
            try
            {
                var pattern = coords.Split(new char[] { 'x', ',' });
                if ((int.TryParse(pattern[0], out int r) && int.TryParse(pattern[1], out int c)) && ValidateMatrixRowsAndCols(false, r, c))
                {
                    return new int[2] { r, c };
                }
            }
            catch (Exception)
            {
                return null;

            }
            return null;
        }

        public static void ValidateQueenMovement(int r, int c, Player currentPlayer)
        {
            r--;
            c--;

            //Mark Player's posizion
            if (Map.Matrix[r, c] == 0)
            {
                Map.Matrix[r, c] = currentPlayer.Id;

                //Horizontals
                for (int col = 0; col < Map.Cols; col++)
                {
                    if (col != c)
                    {
                        Map.Matrix[r, col] = 3;
                    }
                }
                //Verticals
                for (int row = 0; row < Map.Rows; row++)
                {
                    if (row != r)
                    {
                        Map.Matrix[row, c] = 3;
                    }
                }

                /*
                 * Diagonals
                 */

                int queenLeftRow = r;
                int queenLeftCol = c;
                int maxRows = Map.Matrix.GetLength(0);
                int maxCols = Map.Matrix.GetLength(1);


                while (true)
                {

                    if (queenLeftRow == 0 || queenLeftCol == 0)
                    {
                        break;

                    }
                    queenLeftCol--;
                    queenLeftRow--;
                }

                // goes down from left to right
                while (true)
                {
                    if (queenLeftRow == maxRows || queenLeftCol == maxCols)
                    {
                        break;
                    }

                    if (Map.Matrix[queenLeftRow, queenLeftCol] == 0)
                        Map.Matrix[queenLeftRow, queenLeftCol] = 3;
                    queenLeftCol++;
                    queenLeftRow++;
                }

                int queenRightRow = r;
                int queenRightCol = c;
                while (true)
                {

                    if (queenRightRow == 0 || queenRightCol == maxCols - 1)
                    {
                        break;
                    }

                    queenRightCol++;
                    queenRightRow--;
                }
                // goes down from right to left
                while (true)
                {
                    if (queenRightRow > maxRows - 1 || queenRightCol < 0)
                    {
                        break;
                    }
                    if (Map.Matrix[queenRightRow, queenRightCol] == 0)
                        Map.Matrix[queenRightRow, queenRightCol] = 3;
                    queenRightCol--;
                    queenRightRow++;
                }

            }
            else
            {
                Console.WriteLine("You cannot place your queen there");
                Thread.Sleep(100);

                //Same Player
                var game = new Game();
                game.GameScreen(currentPlayer.Id);
            }
        }

        
    }
}
