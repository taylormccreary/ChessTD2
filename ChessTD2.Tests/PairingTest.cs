using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessTD2.Models;
using System.Collections.Generic;
using System.Linq;

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
            var round1 = new Round { Number = 1, RoundID = 1, Pairings = new List<Pairing>() };
            var pairing1 = new Pairing { White = player1, Black = player2, PairingID = 1, Result = PairingResult.WhiteWins };
            round1.Pairings.Add(pairing1);
            var round2 = new Round { Number = 2, RoundID = 2 };
            var pairing2 = new Pairing { White = player2, Black = player1, PairingID = 2, Result = PairingResult.Draw };
            var players = new List<Player> { player1, player2 };
            var section1 = new Section { Name = "championship", SectionID = 1, Players = players, Rounds = new List<Round> { round1, round2 } };

            // act

            var allRounds = section1.Rounds;

            // assert
            Assert.AreEqual(expected: 2, actual: players.Count);
            Assert.AreEqual(2, section1.Rounds.Count);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var champ = new SectionPlayers
            {
                Players = new List<SectionPlayer>
                {
                    new SectionPlayer
                    {
                        Player=new Player { FirstName = "Taylor", LastName = "McCreary", Rating = 2000, PlayerID = 1 },
                        Score=0
                    },
                    new SectionPlayer
                    {
                        Player=new Player { FirstName = "Michael", LastName = "McCreary", Rating = 1000, PlayerID = 2 },
                        Score=0
                    },
                    new SectionPlayer
                    {
                        Player=new Player { FirstName = "Ethan", LastName = "McSwain", Rating = 1000, PlayerID = 3 },
                        Score=0
                    },
                    new SectionPlayer
                    {
                        Player=new Player { FirstName = "Tanner", LastName = "Begin", Rating = 800, PlayerID = 4 },
                        Score=0
                    },
                    new SectionPlayer
                    {
                        Player=new Player { FirstName = "Mark", LastName = "Keller", Rating = 1600, PlayerID = 5 },
                        Score=0
                    },
                    new SectionPlayer
                    {
                        Player=new Player { FirstName = "Magnus", LastName = "Carlsen", Rating = 2850, PlayerID = 6 },
                        Score=0
                    }
                }
            };

            Assert.Fail();
        }
    }

    public class SectionPlayer
    {
        public Player Player { get; set; }
        public int PlayerID { get; set; }
        public double Score { get; set; }
        public ICollection<int> OpponentIDs { get; set; }
    }

    public class SectionPlayers
    {
        public ICollection<SectionPlayer> Players { get; set; }
    }
}
