using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessTD2.Models;
using System.Collections.Generic;

namespace ChessTD2.Tests
{
    [TestClass]
    public class PairingTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            var player1 = new Player { FirstName = "Taylor", LastName = "McCreary", PlayerID = 1, Rating = 2000 };
            var player2 = new Player { FirstName = "Michael", LastName = "McCreary", PlayerID = 2, Rating = 900 };
            var round1 = new Round { Number = 1, RoundID = 1 };
            var pairing1 = new Pairing { White = player1, Black = player2, PairingID = 1, Result = PairingResult.WhiteWins };
            round1.Pairings.Add(pairing1);
            var round2 = new Round { Number = 2, RoundID = 2 };
            var pairing2 = new Pairing { White = player2, Black = player1, PairingID = 2, Result = PairingResult.Draw };
            var players = new List<Player> { player1, player2 };
            var section1 = new Section { Name = "championship", SectionID = 1, Players = players };
            // act
            var player1score = ;

            // assert
            Assert.AreEqual(expected: 2, actual: players.Count);
        }
    }
}
