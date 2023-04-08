using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Library
{
    public class Dealer
    {
        public Hand Hand { get; internal set; } = new Hand();

        public void TurnCardFaceUp()
        {
            Hand.Cards.Last().IsFaceDown = false;
            Hand.PlayCompleted = (Hand.IsBlackjack() || Hand.IsBusted || Hand.Value > 17);
        }
    }
}
