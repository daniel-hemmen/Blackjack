using Blackjack.Library;
using NuGet.Frameworks;

namespace Blackjack.Test
{
    [TestClass]
    public class ShoeTest
    {
        Shoe sut;
        [TestInitialize] public void Init() 
        {
            Game game = new Game();
            sut = game.Shoe;
        }

        //[TestMethod]
        //public void ShoeContains312Cards()
        //{
        //    Assert.AreEqual(sut.Cards.Count, 312);
        //}

        [TestMethod]
        public void Shuffle_NewDeckNotEqualToOldDeck()
        {
            Shoe newShoe = new Shoe();

            Assert.AreNotEqual(newShoe, sut);
        }

        [TestMethod]
        public void Shuffle_NewDeckDoesNotContainDuplicates()
        {
            var sut = new Shoe();

            Assert.AreEqual(52, sut.Cards.DistinctBy(card => card.ToString()).Count());
        }

        [TestMethod]
        public void GetCard_DealsFirstCard()
        {
            // Arrange
            Card firstCardBeforeDealing = sut.Cards.First();
            
            // Act
            Card dealtCard = sut.GetCard();

            // Assert
            Assert.AreEqual(dealtCard, firstCardBeforeDealing);
        }

        [TestMethod]
        public void GetCard_RemovesDealtCardFromShoe()
        {
            // Arrange
            Card firstCardBeforeDealing = sut.Cards.First();


            // Act
            Card dealtCard = sut.GetCard();
            Card firstCardAfterDealing = sut.Cards.First();


            // Assert
            Assert.AreNotEqual(firstCardAfterDealing, firstCardBeforeDealing);
        }
    }
}