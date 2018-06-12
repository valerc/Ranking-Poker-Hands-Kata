using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerHands.Tests
{
    [TestClass]
    public class PokerHandTests
    {
        [TestMethod]
        [DataRow("Highest straight flush wins", Result.Loss, "2H 3H 4H 5H 6H", "KS AS TS QS JS")]
        [DataRow("Straight flush wins of 4 of a kind", Result.Win, "2H 3H 4H 5H 6H", "AS AD AC AH JD")]
        [DataRow("Highest 4 of a kind wins", Result.Win, "AS AH 2H AD AC", "JS JD JC JH 3D")]
        [DataRow("4 Of a kind wins of full house", Result.Loss, "2S AH 2H AS AC", "JS JD JC JH AD")]
        [DataRow("Full house wins of flush", Result.Win, "2S AH 2H AS AC", "2H 3H 5H 6H 7H")]
        [DataRow("Highest flush wins", Result.Win, "AS 3S 4S 8S 2S", "2H 3H 5H 6H 7H")]
        [DataRow("Flush wins of straight", Result.Win, "2H 3H 5H 6H 7H", "2S 3H 4H 5S 6C")]
        [DataRow("Equal straight is tie", Result.Tie, "2S 3H 4H 5S 6C", "3D 4C 5H 6H 2S")]
        [DataRow("Straight wins of three of a kind", Result.Win, "2S 3H 4H 5S 6C", "AH AC 5H 6H AS")]
        [DataRow("3 Of a kind wins of two pair", Result.Loss, "2S 2H 4H 5S 4C", "AH AC 5H 6H AS")]
        [DataRow("2 Pair wins of pair", Result.Win, "2S 2H 4H 5S 4C", "AH AC 5H 6H 7S")]
        [DataRow("Highest pair wins", Result.Loss, "6S AD 7H 4S AS", "AH AC 5H 6H 7S")]
        [DataRow("Pair wins of nothing", Result.Loss, "2S AH 4H 5S KC", "AH AC 5H 6H 7S")]
        [DataRow("Highest card loses", Result.Loss, "2S 3H 6H 7S 9C", "7H 3C TH 6H 9S")]
        [DataRow("Highest card wins", Result.Win, "4S 5H 6H TS AC", "3S 5H 6H TS AC")]
        [DataRow("Equal cards is tie", Result.Tie, "2S AH 4H 5S 6C", "AD 4C 5H 6H 2C")]
        public void RankingPokerHandsTest(string description, Result expected, string hand, string opponentHand)
        {
            Assert.AreEqual(expected, new PokerHand(hand).CompareWith(new PokerHand(opponentHand)), description);
        }


        [TestMethod]
        public void PokerHandSortTest()
        {
            // Arrange
            var expected = new List<PokerHand> {
            new PokerHand("KS AS TS QS JS"),
            new PokerHand("2H 3H 4H 5H 6H"),
            new PokerHand("AS AD AC AH JD"),
            new PokerHand("JS JD JC JH 3D"),
            new PokerHand("2S AH 2H AS AC"),
            new PokerHand("AS 3S 4S 8S 2S"),
            new PokerHand("2H 3H 5H 6H 7H"),
            new PokerHand("2S 3H 4H 5S 6C"),
            new PokerHand("2D AC 3H 4H 5S"),
            new PokerHand("AH AC 5H 6H AS"),
            new PokerHand("2S 2H 4H 5S 4C"),
            new PokerHand("AH AC 5H 6H 7S"),
            new PokerHand("AH AC 4H 6H 7S"),
            new PokerHand("2S AH 4H 5S KC"),
            new PokerHand("2S 3H 6H 7S 9C")
        };
            var random = new Random((int)DateTime.Now.Ticks);
            var actual = expected.OrderBy(x => random.Next()).ToList();
            // Act
            actual.Sort();
            // Assert
            for (var i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i], actual[i], "Unexpected sorting order at index {0}", i);
        }
    }
}
