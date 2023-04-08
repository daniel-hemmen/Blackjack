using Blackjack.Library;
using Blackjack.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class GamePrinter : TextWriter
    {
        public GamePrinter()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public void PrintWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("Start Game");
            Console.WriteLine("=================");
        }

        public void PrintRoundStart()
        {
            Console.Clear();
            Console.WriteLine("New Round");
            Console.WriteLine("=================");
        }

        public void PrintPlayerChoiceStart()
        {
            Console.Clear();
            Console.WriteLine("Player Choice");
            Console.WriteLine("=================");
        }

        public void PrintRoundResultStart()
        {
            Console.Clear();
            Console.WriteLine("Results");
            Console.WriteLine("=================");
        }

        public void PrintDealerPlaysStart()
        {
            Console.Clear();
            Console.WriteLine("Dealer draws");
            Console.WriteLine("=================");
        }

        public void PrintGameOverStart()
        {
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.WriteLine("=================");
        }
        internal void PrintExitMessage(decimal balance, decimal change)
        {
            Console.WriteLine($"Game over. Final balance: {balance:#,0.#}");
            Console.WriteLine(change < 0 ? $"Lost: {change * -1:#,0.#}" : $"Won: {change:#,0.#} ");
        }

        private void PrettyPrint(List<Card> cards)
        {
            string[] ranks;
            string[] suits;
            ranks = GetPrintableRanksAndSuits(cards, out suits);

            string[] lines = AsciiLines(ranks, suits);

            foreach (string line in lines)
            {
                WriteLine(line);
            }

            Console.WriteLine();
        }

        private string[] GetPrintableRanksAndSuits(List<Card> cards, out string[] suits)
        {
            int numberOfCards = cards.Count;
            string[] ranks = new string[numberOfCards];
            suits = new string[numberOfCards];

            string rank;
            string suit;

            for (int index = 0; index < numberOfCards; index++)
            {
                rank = cards[index].IsFaceDown ? "?" : cards[index].Rank.ToShortString();
                suit = cards[index].IsFaceDown ? "?" : cards[index].Suit.ToShortString();

                ranks[index] = rank;
                suits[index] = suit;
            }
            return ranks;
        }

        private string[] AsciiLines(string[] ranks, string[] suits)
        {
            int numberOfCards = ranks.Length;

            string[] lines = new string[7]
            {
                ".-------.",
                "| --    |",
                "|       |",
                "|   -   |",
                "|       |",
                "|    -- |",
                "`-------'"
            };

            string[] output = new string[7];

            for (int index = 0; index < numberOfCards; index++)
            {
                output[0] += lines[0] + " ";
                output[1] += lines[1].Replace("--", ranks[index].PadRight(2)) + " ";
                output[2] += lines[2] + " ";
                output[3] += lines[3].Replace("-", suits[index]) + " ";
                output[4] += lines[4] + " ";
                output[5] += lines[5].Replace("--", ranks[index].PadLeft(2)) + " ";
                output[6] += lines[6] + " ";
            };

            return output;
        }

        public override void WriteLine()
        {
            Console.WriteLine();
        }

        public override void Write(char value)
        {
            if (value == '♥' || value == '♦')
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(value);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public void PrintResult(Result result, decimal bet)
        {
            string message = result switch
            {
                Result.BlackjackTie => $"Unlucky! Tied on blackjack.\n{bet:#,0.#} returned to balance.",
                Result.BlackjackWin => $"Congratulations! Blackjack!\nWon {bet * 1.5m:#,0.#}.",
                Result.Bust => $"Busted!\nLost {bet:#,0.#}",
                Result.Win => $"You win!\nWon {bet:#,0.#}.",
                Result.Tie => $"Tie.\n{bet:#,0.#} returned to balance.",
                Result.Loss => $"You lose.\nLost {bet:#,0.#}.",
                _ => throw new ResultCannotBeShownException($"Unable to show result.")
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void ShowHands(Hand playerHand, Hand dealerHand)
        {
            Console.WriteLine("Dealer has:");
            PrettyPrint(dealerHand.Cards);
            Console.WriteLine();

            Console.WriteLine("You have: ");
            PrettyPrint(playerHand.Cards);

            Console.WriteLine($"Dealer hand's value: {dealerHand.Value}");
            Console.WriteLine($"Your hand's value: {playerHand.Value}");
            Console.WriteLine($"(Bet: {playerHand.Bet:#,0.#})");

            Console.WriteLine();
        }

        public void ShowHands(List<Hand> playerHands, Hand dealerHand)
        {
            Console.WriteLine("Dealer has:");
            PrettyPrint(dealerHand.Cards);

            Console.WriteLine();

            Console.WriteLine("You have: ");
            Console.WriteLine();
            int index = 0;
            foreach (Hand hand in playerHands)
            {
                index++;
                Console.WriteLine($"Hand number {index}:");
                PrettyPrint(hand.Cards);
                Console.WriteLine($"Dealer hand value: {dealerHand.Value}");
                Console.WriteLine($"Hand {index}'s value: {hand.Value}");
                Console.WriteLine($"(Bet: {hand.Bet:#,0.#})");
                Console.WriteLine();
            }
        }

        public override Encoding Encoding { get { return Encoding.Default; } }

    }
}
