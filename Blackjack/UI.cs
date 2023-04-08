using Blackjack.Library;
using static Blackjack.InputValidation;

namespace Blackjack
{
    public class UI
    {
        private static Game game = new Game();
        private static GamePrinter _gamePrinter = new GamePrinter();

        public static void StartGame()
        {
           _gamePrinter.PrintWelcomeMessage();

            string message = "Welcome to the casino! Ready to play Blackjack?\nType (Y)es to continue.";
            bool playGame = GetYesOrNoAsBool(message);

            if (!playGame)
            {
                GameOver();
            }
            else
            {
                StartRound();
            }
        }

        public static void StartRound()
        {
            _gamePrinter.PrintRoundStart();

            PlaceBet(game.Player.Hands[0], game.Player.Balance);
            game.DealFirstRound();

            if (game.Player.Hands[0].Value > 21 || game.Player.Hands[0].IsBlackjack())
            {
                GetResult();
            }
            else
            {
                StartGameplayLoop();
            }
        }

        private static void StartGameplayLoop()
        {
            int numberOfHands = game.Player.Hands.Count;
            for (int index = 0; index < numberOfHands; index++)
            {
                Hand playerHand = game.Player.Hands[index];
                int handNumber = game.Player.Hands.IndexOf(playerHand) + 1;

                while (!playerHand.PlayCompleted)
                {
                    _gamePrinter.PrintPlayerChoiceStart();

                    _gamePrinter.ShowHands(playerHand, game.Dealer.Hand);

                    Task.Delay(700).Wait();

                    if (game.Player.HasSplit)
                    {
                        Console.WriteLine($"Playing hand {handNumber}");
                    }

                    if (playerHand.Value >= 21)
                    {
                        Console.WriteLine("Busted!");
                        playerHand.PlayCompleted = true;
                    }
                    else
                    {
                        char choice = PlayerChoice(playerHand);
                        NextMove(choice, playerHand);
                    }
                }
            }

            Task.Delay(700).Wait();

            if (game.Player.Hands.All(hand => hand.PlayCompleted))
            {
                GetResult();
            }
            else
            {
                StartGameplayLoop();
            }
        }


        private static char PlayerChoice(Hand playerHand)
        {
            char choice;
            char[] choices;
            string message;

            if (playerHand.CanSplit(game.Player.Balance) && playerHand.CanDoubleDown(game.Player.Balance))
            {
                choices = new char[] { 'H', 'S', 'D', 'P' };
                message = "(H)it, (S)tand, (D)ouble Down or Split (P)airs?";
            }
            else if (playerHand.CanDoubleDown(game.Player.Balance))
            {
                choices = new char[] { 'H', 'S', 'D' };
                message = "(H)it, (S)tand, or (D)ouble down?";
            }
            else
            {
                choices = new char[] { 'H', 'S' };
                message = "(H)it or (S)tand?";
            }
            choice = GetChoiceFromUser(message, choices);
            return choice;
        }

        private static void NextMove(char choice, Hand playerHand)
        {
            if (choice == 'H')
            {
                PlayerHits(playerHand);
            }
            else if (choice == 'S')
            {
                playerHand.PlayCompleted = true;
            }
            else if (choice == 'D')
            {
                PlayerDoublesDown(playerHand);
            }
            else if (choice == 'P')
            {
                PlayerSplits(playerHand);
            }
        }

        private static void PlayerHits(Hand playerHand)
        {
            game.DealNext(playerHand);
            if (playerHand.Value > 21 || playerHand.Value == 21)
            {
                playerHand.PlayCompleted = true;
            }
        }

        private static void PlayerSplits(Hand playerHand)
        {
            Console.WriteLine("Splitting pair.\n");

            game.Player.Split(playerHand);
            foreach (Hand hand in game.Player.Hands)
            {
                game.DealNext(hand);
            }
        }

        private static void PlayerDoublesDown(Hand playerHand)
        {
            playerHand.DoubleDown();

            Console.WriteLine($"Doubling down. New bet: {playerHand.Bet:#,0.#}.\n");

            game.DealNext(playerHand);
            playerHand.PlayCompleted = true;
        }


        private static void GetResult()
        {
            _gamePrinter.PrintRoundResultStart();

            Player player = game.Player;
            Dealer dealer = game.Dealer;

            dealer.TurnCardFaceUp();

            if (player.HasSplit)
            {
                _gamePrinter.ShowHands(player.Hands, dealer.Hand);
            }
            else
            {
                _gamePrinter.ShowHands(player.Hands.FirstOrDefault()!, dealer.Hand);
            }

            Task.Delay(700).Wait();

            if (!player.Hands.Any(hand => hand.IsBusted) && !dealer.Hand.IsBlackjack())
            {
                DealerPlays(dealer.Hand);
                dealer.Hand.PlayCompleted = true;
            }

            foreach (Hand playerHand in game.Player.Hands)
            {
                playerHand.Result = game.CalculateResult(playerHand, game.Dealer.Hand, game.Player.HasSplit);
                game.Player.UpdateBalance(playerHand.Result, playerHand.Bet);
            }

            ShowResult();
        }

        private static void ShowResult()
        {
            foreach (Hand playerHand in game.Player.Hands)
            {
                Result result = playerHand.Result;

                if (game.Player.HasSplit)
                {
                    int handNumber = game.Player.Hands.IndexOf(playerHand) + 1;
                    Console.WriteLine($"Hand {handNumber} result:");
                }

                _gamePrinter.PrintResult(result, playerHand.Bet);
                
                Task.Delay(700).Wait();
            }

            EndRound();
        }

        private static void DealerPlays(Hand dealerHand)
        {
            while (dealerHand.Value < 17 && !dealerHand.IsBlackjack())
            {
                _gamePrinter.PrintDealerPlaysStart();

                game.DealNext(game.Dealer.Hand);

                if (game.Player.HasSplit)
                {
                    _gamePrinter.ShowHands(game.Player.Hands, dealerHand);
                }
                else
                {
                    _gamePrinter.ShowHands(game.Player.Hands.First(), dealerHand);
                }

                Task.Delay(700).Wait();
            }
        }

        private static void PlaceBet(Hand hand, decimal balance)
        {
            decimal bet = GetBetFromUser(balance);
            game.Player.DeductBetFromBalance(bet);
            hand.SetBet(bet);
            Console.WriteLine();
        }

        private static void EndRound()
        {
            if (game.Player.Balance <= 0)
            {
                GameOver();
            }
            else
            {
                Console.WriteLine($"Your balance: {game.Player.Balance:#,0.#}");
                Console.WriteLine();

                string message = "Would you like to play again? (Y)es/ (N)o";

                bool playAgain = GetYesOrNoAsBool(message);

                if (playAgain)
                {
                    Console.WriteLine();
                    game.Reset();
                    StartRound();
                }
                else
                {
                    GameOver();
                }
            }
        }

        private static void GameOver()
        {
            _gamePrinter.PrintGameOverStart();

            decimal balance = game.Player.Balance;
            decimal change = balance - 20;

            _gamePrinter.PrintExitMessage(balance, change);

            Environment.Exit(0);
        }
    }
}