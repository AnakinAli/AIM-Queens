using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIM_Queens.GameLogic
{
    internal static class Map
    {
        public static int Rows { get; set; }
        public static int Cols { get; set; }
        public static int[,] Matrix { get; set; }

        public static bool AllCheckedOut()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Matrix[r,c]==0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static string Print()
        {
            string res = string.Empty;

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    res += $"{Matrix[i, j]} ";
                }
                res += "\n";
            }
            return res;
        }
    }
}
