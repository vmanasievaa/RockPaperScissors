using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game;
            GameType gameType;

            List<GameType> gameTypes = new List<GameType> {
                new GameType { Name = "basic", KeyPicker = "b", Description = String.Empty},
                new GameType { Name = "advanced", KeyPicker = "a", Description = "Please follow these rules: .\n"}
            };

            bool playAgain = true;

            string gameTypeInput;
            string playerMove;


            Console.WriteLine("Welcome to ROCK, PAPER, SCISSORS!!\n");

            while (playAgain)
            {
                gameTypeInput = "";
                playerMove = "";

                Console.WriteLine("Choose between basic or advanced game (b/a)");
                while (!gameTypes.Any(x => x.KeyPicker == gameTypeInput.ToLower()))
                {
                    gameTypeInput = Console.ReadLine();
                    gameType = gameTypes.SingleOrDefault(x => x.KeyPicker == gameTypeInput.ToLower());

                    if (gameType == null)
                        break;

                    game = new Game(gameType, 3);

                    game.DisplayGameDescriptionLine();
                    game.DisplayGameRoundsLine();

                    while(!game.IsGameOver())
                    {
                        game.DisplayGameMoveLine();
                        playerMove = Console.ReadLine().ToUpper();
                        if (game.IsValidInputMove(playerMove))
                        {
                            Move playerMovePassed = game.GetAvailableMoveByName(playerMove);
                            game.UpdateUsedMoves(playerMovePassed);

                            Move masterMovePassed = game.GetRandomMasterMove();
                            game.UpdateUsedMoves(masterMovePassed);

                            Console.WriteLine("\nMaster move: " + masterMovePassed.Name);

                            if (playerMovePassed.WinsOver(masterMovePassed))
                            {
                                Console.WriteLine("\nPlayer SCORED!");
                                game.UpdatePlayerScore();
                            }
                            else if (masterMovePassed.WinsOver(playerMovePassed))
                            {
                                Console.WriteLine("\nMaster SCORED!");
                                game.UpdateMasterScore();
                            }
                            else
                            {
                                Console.WriteLine("\nDRAW");
                            }

                            game.DisplayGameResultOverview();
                        }
                    }

                    Console.WriteLine("\n================================ GAME OVERVIEW ================================");
                    game.DisplayWinner();
                    game.DisplayTotalTurnsToWin();
                    game.DisplayMostUsedMove();
                    Console.WriteLine("===============================================================================\n");
                }

                Console.WriteLine("Do you want to play again (y/n)?");
                if(Console.ReadLine() == "n")
                    playAgain = false;

                Console.Clear();
            }

            Environment.Exit(0);

        }
    }
}
