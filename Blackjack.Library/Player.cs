using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Library
{
    public class Player
    {
        public decimal Balance { get; private set; }

        public List<Hand> Hands { get; internal set; }

        public bool HasSplit 
        { 
            get
            {
                return Hands.Count >= 2;
            } 
        }

        public Player()
        {
            Hands = new List<Hand> { new Hand() };
            Balance = 20m;
        }

        public void UpdateBalance(Result result, decimal bet)
        {
            Balance += result switch
            {
                Result.BlackjackWin => bet * 2.5m,
                Result.BlackjackTie or Result.Tie => bet,
                Result.Win => bet * 2,
                _ => 0
            };
        }

        public void Split(Hand hand)
        {
            Hand splitHand = new();
            Card temp = hand.Cards.Last();
            splitHand.Cards.Add(temp);
            splitHand.SetBet(hand.Bet);
            hand.Cards.Remove(hand.Cards.Last());
            Hands.Add(splitHand);
        }

        public void DeductBetFromBalance(decimal bet)
        {
            Balance -= bet;
        }
    }
}
