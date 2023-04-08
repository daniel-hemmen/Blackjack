using Blackjack.Library;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;

namespace Blackjack.Test
{
    [TestClass]
    public class HandTest
    {
        Hand sut;
        [TestInitialize]
        public void Init()
        {
            Game game = new Game();
            sut = game.Player.Hands.FirstOrDefault()!;
        }

        [TestMethod]
        public void ValueGetter_Set10_Return10()
        {
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Five));
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Five));

            Assert.AreEqual(10, sut.Value);
        }

        [TestMethod]
        public void ValueGetter_Set10andAce_Returns21()
        {
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Ten));
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Ace));

            Assert.AreEqual(21, sut.Value);
        }


        [TestMethod]
        public void ValueGetter_TwoAces_Return12()
        {
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Ace));
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Ace));

            Assert.AreEqual(12, sut.Value);
        }

        [TestMethod]
        public void ValueGetter_KingSeven_Returns17()
        {
            sut.Cards.Add(new Card(Suit.Clubs, Rank.King));
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Seven));

            Assert.AreEqual(17, sut.Value);
        }

        [TestMethod]
        public void IsBusted_TwoFive_ReturnsFalse()
        {
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Two));
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Five));

            Assert.IsFalse(sut.IsBusted);
        }

        [TestMethod]
        public void IsBusted_TenFiveSeven_ReturnsTrue()
        {
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Ten));
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Five));
            sut.Cards.Add(new Card(Suit.Spades, Rank.Seven));

            Assert.IsTrue(sut.IsBusted);
        }

        [TestMethod]
        public void CanSplit_ThreeCards_ReturnsFalse()
        {
            // Arrange
            decimal balance = 20m;
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Three));
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Three));
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Three));

            // Assert
            Assert.IsFalse(sut.CanSplit(balance));
        }

        [TestMethod]
        public void CanSplit_SameRankDifferentSuit_ReturnsTrue()
        {
            // Arrange
            decimal balance = 20m;
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Three));
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Three));

            // Assert
            Assert.IsTrue(sut.CanSplit(balance));
        }

        [TestMethod]
        public void CanSplit_SameSuitDifferentRank_ReturnsFalse()
        {
            // Arrange
            decimal balance = 20m;
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Three));
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Four));

            // Assert
            Assert.IsFalse(sut.CanSplit(balance));
        }

        [TestMethod]
        public void CanSplit_BalanceLowerThanBet_ReturnsFalse()
        {
            // Arrange
            sut.SetBet(10);

            sut.Cards.Add(new Card(Suit.Clubs, Rank.Jack));
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Jack));

            // Assert
            Assert.IsTrue(sut.CanSplit(11m));
            Assert.IsTrue(sut.CanSplit(10m));
            Assert.IsFalse(sut.CanSplit(9m));
        }


        [TestMethod]
        public void IsBlackjack_Blackjack_ReturnsTrue()
        {
            // Arrange
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Ten));
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Ace));

            // Assert
            Assert.IsTrue(sut.IsBlackjack());
        }

        [TestMethod]
        public void IsBlackjack_21OnSplitHand_ReturnsFalse()
        {
            // Arrange
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Ten));
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Ace));

            // Assert
            Assert.IsFalse(sut.IsBlackjack(split: true));
        }

        [TestMethod]
        public void IsBlackjack_21WithThreeCards_ReturnsFalse()
        {
            // Arrange
            sut.Cards.Add(new Card(Suit.Clubs, Rank.Five));
            sut.Cards.Add(new Card(Suit.Hearts, Rank.Six));
            sut.Cards.Add(new Card(Suit.Spades, Rank.Ten));

            // Assert
            Assert.IsFalse(sut.IsBlackjack());
        }
    }
}