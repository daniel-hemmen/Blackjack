using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Library.Enums
{
    public static class RankExtensions
    {
        public static string ToShortString(this Rank rank)
        {
            return rank switch
            {
                Rank.Ace => "A",
                Rank.King => "K",
                Rank.Queen => "Q",
                Rank.Jack => "J",
                Rank.Ten => "10",
                Rank.Nine => "9",
                Rank.Eight => "8",
                Rank.Seven => "7",
                Rank.Six => "6",
                Rank.Five => "5",
                Rank.Four => "4",
                Rank.Three => "3",
                Rank.Two => "2",
                _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, "Invalid rank"),
            };
        }
    }
}
