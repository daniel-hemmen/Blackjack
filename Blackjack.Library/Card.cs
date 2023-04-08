using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Library
{
    public record Card
    {
        public Suit Suit { get; internal set; }
        public Rank Rank { get; internal set; }
        public bool IsFaceDown;
        public int Value
        {
            get
            {
                int value = Rank switch
                {
                    Rank.Ace => 1,
                    Rank.Two => 2,
                    Rank.Three => 3,
                    Rank.Four => 4,
                    Rank.Five => 5,
                    Rank.Six => 6,
                    Rank.Seven => 7,
                    Rank.Eight => 8,
                    Rank.Nine => 9,
                    _ => 10
                };
                return value;
            }
        }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
            IsFaceDown = false;
        }

        public override string ToString()
        {
            return $"Rank: {Rank}. Suit: {Suit}.";
        }
    }
}
