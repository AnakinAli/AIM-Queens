using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIM_Queens.GameLogic;

namespace AIM_Queens {
internal class Program {
  static void Main(string[] args) {
    Game game = new Game();

    game.WelcomeScreen();
    game.GameScreen();
  }
}
}
