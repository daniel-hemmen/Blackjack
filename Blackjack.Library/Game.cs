using System.Security.Cryptography.X509Certificates;

namespace Blackjack.Library
{
    public class Game
    {
        public Dealer Dealer { get; }
        public Player Player { get; }
        public Shoe Shoe { get; private set; }

        public Game() 
        {
            Dealer = new Dealer();
            Player = new Player();
            Shoe = new Shoe();
        }

        public void DealFirstRound()
        {
            DealNext(Player.Hands.FirstOrDefault()!);
            DealNext(Dealer.Hand);
            DealNext(Player.Hands.FirstOrDefault()!);
            DealNext(Dealer.Hand);
            Dealer.Hand.Cards.Last().IsFaceDown = true;
        }

        public void DealNext(Hand hand)
        {
            hand.Cards.Add(Shoe.GetCard());
        }

        public void Reset()
        {
            if (Shoe.CutCardReached)
            {
                Console.WriteLine("Cut card reached. Replacing shoe...");
                Shoe = new Shoe();
                Task.Delay(700).Wait();
            }
            Dealer.Hand = new Hand();
            Player.Hands.Clear();
            Player.Hands.Add(new Hand());
        }

        public Result CalculateResult(Hand playerHand, Hand dealerHand, bool split)
        {
            if (playerHand.IsBlackjack(split) && dealerHand.IsBlackjack())
            {
                return Result.BlackjackTie;
            }
            else if (playerHand.IsBlackjack(split))
            {
                return Result.BlackjackWin;
            }
            else if (playerHand.Value > 21)
            {
                return Result.Bust;
            }
            else if (playerHand.Value > dealerHand.Value || dealerHand.Value > 21)
            {
                return Result.Win;
            }
            else if (playerHand.Value == dealerHand.Value)
            {
                return Result.Tie;
            }
            else
            {
                return Result.Loss;
            }
        }

    }
}