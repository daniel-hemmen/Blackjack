using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Library
{
    public class Shoe
    {
        private const string Name = "Harry";

        public List<Card> Cards = new List<Card>();
        public bool CutCardReached { get { return Cards.Count <= 78; } }

        public Shoe() 
        {
            for (int i = 0; i < 6; i++)
            {
                foreach(Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    {
                        Cards.Add(new Card(suit, rank));
                    }
                }
            }
            Shuffle();
        }

        public void Shuffle()
        {
            Card tempCard;
            var rnd = new Random();

            for (int index = Cards.Count - 1; index >= 0; index--)
            {
                int random = rnd.Next(0, index + 1);
                if (random != index)
                {
                    tempCard = Cards[random];
                    Cards[random] = Cards[index];
                    Cards[index] = tempCard;
                }
            }
        }

        public Card GetCard()
        {
            Card firstCard = Cards.First();
            Cards.Remove(Cards.First());
            return firstCard;
        }
    }
}
