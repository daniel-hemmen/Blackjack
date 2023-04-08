using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Library.Enums
{
    public static class SuitExtensions
    {
        public static string ToShortString(this Suit suit)
        {
            return suit switch
            {
                Suit.Spades => "♠",
                Suit.Diamonds => "♦",
                Suit.Hearts => "♥",
                Suit.Clubs => "♣",
                _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, "Invalid suit")
            };
        }
    }
}
