using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Game
    {
        public int Id { get; set; }
        public int GameTypeId { get; set; }
        public GameType GameType { get; set; }

        public List<Move> AvailableMoves { get; set; }
        public List<Move> UsedMoves { get; set; }

        private int Rounds { get; set; }

        private int PlayerScore { get; set; }
        private int MasterScore { get; set; }

        public Game(GameType gameType, int rounds)
        {
            GameType = gameType;
            Rounds = rounds;
            AvailableMoves = gameType.Name.ToLower().Equals("basic") ? ConfigureBasicGameMoves() : ConfigureAdvancedGameMoves();
            UsedMoves = new List<Move>();
        }

        public void DisplayGameDescriptionLine()
        {
            Console.WriteLine("\nYou have chosen " + GameType.Name.ToLower() + " ROCK, PAPER, SCISSORS game\n" + GameType.Description.ToLower()); 
        }

        public void DisplayGameMoveLine()
        {
            Console.WriteLine("\nEnter your chosen move as " + String.Join(", ", AvailableMoves.Select(x => x.MoveType.ToString()).ToList()) + ": ");
        }

        public void DisplayGameRoundsLine()
        {
            Console.WriteLine("You have "+ Rounds +" rounds to beat the MASTER");
        }

        public void DisplayGameResultOverview()
        {
            Console.WriteLine("\n RESULT OVERVIEW \n Player: " + PlayerScore + " Master: " + MasterScore);
        }

        public void DisplayWinner()
        {
            if (PlayerScore > MasterScore)
                Console.WriteLine("\n PLAYER WON!\n");
            else
                Console.WriteLine("\n MASTER WON!\n");
        }

        public void DisplayTotalTurnsToWin() {
            Console.WriteLine("Total turns to win " + UsedMoves.Count() / 2);
        }

        public void DisplayMostUsedMove()
        {
            Console.WriteLine("Most used move during the game " + UsedMoves.GroupBy(x => x.MoveType).Select(mostUsedMove => new { MoveTypeName = mostUsedMove.Select(x => x.MoveType.ToString()).FirstOrDefault(), Occurence = mostUsedMove.Count() }).OrderByDescending(x => x.Occurence).FirstOrDefault().MoveTypeName);
        }

        private List<Move> ConfigureBasicGameMoves() {
            Move Rock = new Move("ROCK", MoveType.ROCK);
            Move Paper = new Move("PAPER", MoveType.PAPER);
            Move Scissors = new Move("SCISSORS", MoveType.SCISSORS);

            Rock.WinsOverMoves = new List<Move> { Scissors };
            Paper.WinsOverMoves = new List<Move> { Rock };
            Scissors.WinsOverMoves = new List<Move> { Paper };

            return new List<Move> { Rock, Paper, Scissors };

        }
        private List<Move> ConfigureAdvancedGameMoves() {

            Move Rock = new Move("ROCK", MoveType.ROCK);
            Move Paper = new Move("PAPER", MoveType.PAPER);
            Move Scissors = new Move("SCISSORS", MoveType.SCISSORS);
            Move Lizard = new Move("LIZARD", MoveType.LIZARD);
            Move Spock = new Move("SPOCK", MoveType.SPOCK);

            Rock.WinsOverMoves = new List<Move> { Scissors, Lizard };
            Paper.WinsOverMoves = new List<Move> { Rock, Spock };
            Scissors.WinsOverMoves = new List<Move> { Paper, Lizard };
            Lizard.WinsOverMoves = new List<Move> { Paper, Spock };
            Spock.WinsOverMoves = new List<Move> { Scissors, Rock };

            return new List<Move> { Rock, Paper, Scissors, Lizard, Spock };
        }

        public bool IsValidInputMove(string move)
        {
            return AvailableMoves.Any(x => x.Name.Equals(move));
        }

        public Move GetAvailableMoveByName(string moveName)
        {
            return AvailableMoves.FirstOrDefault(x => x.Name.Equals(moveName));
        }

        public void UpdateUsedMoves(Move newMove)
        {
                UsedMoves.Add(newMove);
        }

        public Move GetRandomMasterMove()
        {
            Random rnd = new Random();
            return AvailableMoves.ElementAt(rnd.Next(0, AvailableMoves.Count()));
        }

        public void UpdatePlayerScore()
        {
            PlayerScore++;
        }

        public void UpdateMasterScore()
        {
            MasterScore++;
        }

        public bool IsGameOver()
        {
            return PlayerScore == Rounds || MasterScore == Rounds;
        }


    }

    public class GameType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeyPicker { get; set; }

    }
}
