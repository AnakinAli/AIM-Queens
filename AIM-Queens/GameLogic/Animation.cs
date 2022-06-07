using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIM_Queens.GameLogic
{
    internal class Animation
    {
        private string copyright = "Copyright AIM | AZ-MOGA.BG | Anakin's Fix";

        private int gameMode = 0; // singleplayer

        public int ErrorTimeSuspense = 80;
        public int LineLoadingSpeed = 1;
        private string[] logo = {"  .g8\"\"8q.                                                   ",
        ".dP'    `YM.                                                 ",
        "dM'      `MM `7MM  `7MM  .gP\"Ya   .gP\"Ya `7MMpMMMb.  ,pP\"Ybd ",
        "MM        MM   MM    MM ,M'   Yb ,M'   Yb  MM    MM  8I   `\" ",
        "MM.      ,MP   MM    MM 8M\"\"\"\"\"\" 8M\"\"\"\"\"\"  MM    MM  `YMMMa. ",
        "`Mb.    ,dP'   MM    MM YM.    , YM.    ,  MM    MM  L.   I8 ",
        "  `\"bmmd\"'     `Mbod\"YML.`Mbmmd'  `Mbmmd'.JMML  JMML.M9mmmP' ",
        "      MMb                                                    ",
        "       `bood'                                                "
        };

        private string lineSymbol = "██";

        public Dictionary<string, string> Messages;

        public Animation()
        {
            Messages = new Dictionary<string, string>();

            Messages.Add("mode", "Game Modes: SinglePlayer | Multiplayer");
            Messages.Add("mode_error", "[!] Error: Plese write SinglePlayer or Multiplayer!");
            Messages.Add("mode_selected", "[■] Game Mode was selected!");

            Messages.Add("player_one", "Player One Nickname: ");
            Messages.Add("player_two", "Player Two Nickname: ");

            Messages.Add("map", "Write Map Size (example 5x6): ");
            Messages.Add("map_added", "[■] Map Size was added!");
            Messages.Add("map_error", "[!] Map Size is not written correctly!");

            Messages.Add("start", "[■] Players were added to the game! Write \"Start\" to load the game GLHF!");
            Messages.Add("start_write", "[!] Write \"Start\" to start the game!");

            Messages.Add("coordinates_error", "[!] Enter valid coordinates (1x5|1,5)!");
        }

        /// <summary>
        /// Main Method For The Animation Class
        /// </summary>
        public void TitleQueens(bool needSuspense = true)
        {
            if (!needSuspense)
            {
                ErrorTimeSuspense = 0;
                LineLoadingSpeed = 0;
            }
            LineSeperator();
            LogoAnimation();
            LineSeperator();
            Console.WriteLine(
                String.Format(
                    "\n" +
                    "{0," + ((Console.WindowWidth / 2) + (copyright.Length / 2)) + "}" +
                    "\n",
                    copyright
                    ));
            LineSeperator();
        }

        /// <summary>
        /// Specifc Lines
        /// </summary>
        private void LineSeperator()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < Console.WindowWidth / 2; i++)
            {
                Console.Write(lineSymbol);
                Thread.Sleep(LineLoadingSpeed);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Animates The Logo
        /// </summary>
        private void LogoAnimation()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < logo.Length; i++)
            {
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (logo[i].Length / 2)) + "}", logo[i]));
                Thread.Sleep(ErrorTimeSuspense);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Select mode singleplayer|multiplayer
        /// </summary>
        public void SelectMode()
        {
            // Single and Multi player

            TextAnimation(Messages["mode"]);
            Console.WriteLine("");
            while (true)
            {
                Console.Write("Write your Mode: ");
                string gameMode = Console.ReadLine().Trim().ToLower();
                if (gameMode == "singleplayer" || gameMode == "multiplayer")
                {
                    if (gameMode == "multiplayer")
                    {
                        this.gameMode = 1;
                    }
                    break;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            TextAnimation(Messages["mode_selected"]);

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Add Map specifics
        /// </summary>
        public void SelectMapSize()
        {
            bool isCorrect = false;
            while (!isCorrect)
            {
                TextAnimation(Messages["map"]);

                string[] values = Console.ReadLine().Split('x');

                if (int.TryParse(values[0].Trim(), out int rows) && int.TryParse(values[1].Trim(), out int cols))
                {
                    Map.Rows = rows;
                    Map.Cols = cols;
                    Map.Matrix = GenerateMatrix(rows, cols);

                    isCorrect = Validation.ValidateMatrixRowsAndCols();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    TextAnimation(Messages["map_error"]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(ErrorTimeSuspense);
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Add the Players For the Fame
        /// </summary>
        public void SelectPlayers()
        {
            TextAnimation(Messages["player_one"]);

            Players.AddPlayer(new Player() { Id = 1, Name = Console.ReadLine() });

            if (gameMode == 1)
            {
                TextAnimation(Messages["player_two"]);
                Players.AddPlayer(new Player() { Id = 2, Name = Console.ReadLine() });
            }
            else
            {
                Players.AddPlayer(new Player() { Id = 2, Name = "AIM-Bot" });
            }

            Console.ForegroundColor = ConsoleColor.Green;
            TextAnimation(Messages["start"]);
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            while (true)
            {
                string answer = Console.ReadLine().Trim().ToLower();
                if (answer == "start")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    TextAnimation(Messages["start_write"]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Generates The Initial Matrix
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        private int[,] GenerateMatrix(int rows, int cols)
        {
            int[,] matrix = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Animates the Text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sleepTime"></param>
        public void TextAnimation(string text, int sleepTime = 10)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                Thread.Sleep(sleepTime);
            }
        }

        /// <summary>
        /// Generate the same screen on every turn
        /// </summary>
        public void GameTableAnimationOnTurnGenerator()
        {
            Console.Clear();

            TitleQueens(false);
            GameInfo();
            GameTableAnimation();
        }

        /// <summary>
        /// Show Game Info
        /// </summary>
        private void GameInfo()
        {
            Console.WriteLine("Info:");
            Console.Write("[   ] - Free space ");
            Console.Write("[ ■ ] - Used space ");
            Console.Write("[ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("A");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ] - Player One ");
            Console.Write("[ ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("B");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ] - Player Two");
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Animate the Game Table
        /// </summary>
        private void GameTableAnimation()
        {
            int[,] map = Map.Matrix;

            int rows = Map.Matrix.GetLength(0);
            int columns = Map.Matrix.GetLength(1);

            Console.WriteLine("[MAP]");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("- |");
            for (int i = 1; i < columns + 1; i++)
            {
                Console.Write("| " + i + " |");

            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            for (int iRows = 0; iRows < rows; iRows++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write((iRows + 1) + " |");
                Console.ForegroundColor = ConsoleColor.White;

                for (int iColumns = 0; iColumns < columns; iColumns++)
                {
                    if (map[iRows, iColumns] == 0)
                        Console.Write("[   ]");
                    else if (map[iRows, iColumns] == 1)
                    {
                        Console.Write("[ ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("A");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ]");
                    }
                    else if (map[iRows, iColumns] == 2)
                    {
                        Console.Write("[ ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("B");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ]");
                    }
                    else
                    {
                        Console.Write("[ ■ ]");
                    }
                }

                Console.WriteLine();
            }

        }
    }
}
