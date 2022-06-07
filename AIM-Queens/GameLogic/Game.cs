﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIM_Queens.GameLogic
{
    internal class Game
    {
        private Animation animation = new Animation();

        public void WelcomeScreen()
        {
            animation.TitleQueens();
            animation.SelectMode();
            animation.SelectMapSize();
            animation.SelectPlayers();
        }

        public void GameScreen(int turnIndex = 1)
        {
            // Get current Player
            Player currentPlayer = Players.AllPlayers.Where(p => p.Id == turnIndex).First();

            //Generate table for his turn
            animation.GameTableAnimationOnTurnGenerator();


            //Select Winner
            if (Map.AllCheckedOut())
            {
                Player winner = Players.NextPlayer(currentPlayer);
                animation.TextAnimation("\nThe Winner is...: ");

                Console.ForegroundColor = ConsoleColor.Green;
                animation.TextAnimation($"ID: {winner.Id} {winner.Name} ", 250);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                Environment.Exit(0);
            }


            Console.WriteLine($"\n[Player: {currentPlayer.Name}]");
            Console.WriteLine($"Choose coordinates (1,5|1x5)...: ");

            // Validate input
            var validatedInput = Validation.ValidateCoordinates(Console.ReadLine());

            //Has Error
            if (validatedInput == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                animation.TextAnimation($"{animation.Messages["coordinates_error"]}\nWait...");
                Console.ForegroundColor = ConsoleColor.White;

                Thread.Sleep(animation.ErrorTimeSuspense);
                GameScreen(currentPlayer.Id);
            }

            // Player's coordinates
            int r = validatedInput[0];
            int c = validatedInput[1];


            Validation.ValidateQueenMovement(r, c, currentPlayer);


            var nextPlayer = Players.NextPlayer(currentPlayer);
            Console.WriteLine(nextPlayer);
            GameScreen(nextPlayer.Id);
        }
    }
}