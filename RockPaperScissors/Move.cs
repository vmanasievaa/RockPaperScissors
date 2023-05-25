using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public enum MoveType
    {
        ROCK,
        SCISSORS,
        PAPER,
        LIZARD,
        SPOCK
    }

    public class Move
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MoveType MoveType {get; set;}
        public List<Move> WinsOverMoves { get; set; }

        public Move(string name, MoveType moveType)
        {
            Name = name;
            MoveType = moveType;
            WinsOverMoves = new List<Move>();
        }

        public bool WinsOver(Move player)
        {
            return this.WinsOverMoves.Any(x => x.MoveType == player.MoveType);
        }

    }
}
