using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIM_Queens.GameLogic
{
    internal class Bot
    {
        public string GetRandomCoordinate(Random rand)
        {
            List<string> locations = GetAllEmptyCoords(); 
            return locations[rand.Next(0, locations.Count)];
        }

        private List<string> GetAllEmptyCoords()
        {
            List<string> locations = new List<string>();

            for (int r = 0; r < Map.Rows; r++)
            {
                for (int c = 0; c < Map.Cols; c++)
                {
                    if (Map.Matrix[r, c] == 0)
                    {
                        locations.Add($"{r+1}x{c+1}");
                    }
                }
            }
            return locations;
        }
    }
}
