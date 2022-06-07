using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIM_Queens.GameLogic
{
    internal static class Players
    {
        public static List<Player> AllPlayers = new List<Player>();

        public static void AddPlayer(Player p)
        {
            AllPlayers.Add(p);
        }

        public static Player NextPlayer(Player currentPlayer)
        {
            if (currentPlayer.Id == AllPlayers.Count)
            {
                return AllPlayers.First();
            }
            return AllPlayers.Where(p=>currentPlayer.Id+1==p.Id).FirstOrDefault();
        }

    }
}
