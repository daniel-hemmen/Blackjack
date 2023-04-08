using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Blackjack
{
    internal class InputValidation
    {
        public static bool GetYesOrNoAsBool(string message)
        {
            while (true)
            {
                Console.WriteLine(message);

                char choice = char.ToUpper(Console.ReadLine()!.FirstOrDefault());
                if (choice == 'Y')
                {
                    return true;
                }
                else if (choice == 'N')
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        public static decimal GetBetFromUser(decimal balance)
        {
            Console.WriteLine($"Your balance is {balance}.");
            decimal maxBet = balance >= 10 ? 10 : balance;
            decimal bet;

            while (true)
            {
                Console.Write($"Place your bet (1-{maxBet}): ");
                if (decimal.TryParse(Console.ReadLine(), out bet))
                {
                    if (bet > balance)
                    {
                        Console.WriteLine("Balance too low. Please try again.");
                    }
                    else if (bet <= 0 || bet > 10)
                    {
                        Console.WriteLine("Only bets between 1-10 are allowed");
                    }
                    else
                    {
                        return bet;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        public static char GetChoiceFromUser(string message, char[] acceptedCharacters)
        {
            char choice;
            while (true)
            {
                Console.WriteLine(message);
                choice = char.ToUpper(Console.ReadLine()!.FirstOrDefault());
                if (acceptedCharacters.Contains(choice))
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
    }
}
