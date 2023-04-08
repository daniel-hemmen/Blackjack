using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Library
{
    public class Hand
    {
        public decimal Bet { get; private set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public bool PlayCompleted { get; set; } = false;
        public Result Result { get; set; }

        private int _value;
        public int Value
        {
            get
            {
                _value = Cards.Where(card => !card.IsFaceDown).Sum(card => card.Value);

                if (Cards.Any(card => card.Rank == Rank.Ace && !card.IsFaceDown) && _value + 10 <= 21)
                {
                    return _value + 10;
                }
                else
                {
                    return _value;
                }
            }
        }

        public bool IsBusted { get { return Value > 21;  } }

        public bool IsBlackjack (bool split = false)
        { 
            return Cards.Count == 2 && Value == 21 && !split; 
        }

        public bool CanSplit(decimal balance)
        {
            return Cards.Count == 2 && Cards[0].Rank == Cards[1].Rank && balance >= Bet;
        }

        public bool CanDoubleDown(decimal balance)
        {
            return Cards.Count == 2 && balance >= Bet;
        }


        public void DoubleDown()
        {
            Bet *= 2;
        }

        public void SetBet(decimal bet)
        {
            Bet = bet;
        }
    }
}
