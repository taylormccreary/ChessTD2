using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessTD2.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChessTD2.Tests
{
    [TestClass]
    public class SectionTest
    {
        [TestMethod]
        public void calculateScoreAndOpponents()
        {

            // Arrange

            var section = GetSection();


            // Act
            // players with score and opponents
            var players2 =
                from p in section.Players
                select new SectionPlayerForPairing
                {
                    Player = p,
                    Score =
                        (
                        from ro in section.Rounds
                        from pr in ro.Pairings
                        where pr.Black == p || pr.White == p
                        select
                        ((pr.Black == p && pr.Result == PairingResult.BlackWins) || (pr.White == p && pr.Result == PairingResult.WhiteWins)) ? 1 :
                        pr.Result == PairingResult.Draw && (pr.Black == p || pr.White == p) ? 0.5 : 0
                        ).Sum(),
                    Opponents =
                        (
                        from ro in section.Rounds
                        from pr in ro.Pairings
                        where pr.Black == p || pr.White == p
                        select pr.Black == p ? pr.White.PlayerID : pr.Black.PlayerID
                        ) //.Distinct()
                };

            var playerCount = players2.Count();

            // Assert

            // checking scores
            Assert.AreEqual(2.5, players2.First().Score);
            Assert.AreEqual(1.5, players2.ElementAt(1).Score);
            Assert.AreEqual(4, players2.ElementAt(2).Score);
            Assert.AreEqual(0, players2.ElementAt(3).Score);

            // checking opponents of player1
            Assert.AreEqual(101, players2.First().Opponents.First());
            Assert.AreEqual(103, players2.First().Opponents.ElementAt(1));
            Assert.AreEqual(102, players2.First().Opponents.ElementAt(2));
            Assert.AreEqual(101, players2.First().Opponents.ElementAt(3));
        }


        public Section GetSection()
        {
            var player1 = new Player { PlayerID = 100, FirstName = "Taylor", LastName = "McCreary", Rating = 2000 };
            var player2 = new Player { PlayerID = 101, FirstName = "Michael", LastName = "McCreary", Rating = 900 };
            var player3 = new Player { PlayerID = 102, FirstName = "Debbie", LastName = "McCreary", Rating = 600 };
            var player4 = new Player { PlayerID = 103, FirstName = "Shannyn", LastName = "McCreary", Rating = 800 };
            var player5 = new Player { PlayerID = 104, FirstName = "Lily", Rating = 200 };

            return
            new Section
            {
                Name = "championship",
                SectionID = 1,
                Players = new List<Player> { player1, player2, player3, player4, player5 },
                Rounds = new List<Round>
                {
                    new Round
                    {
                     Number = 1,
                     Pairings = new List<Pairing>
                     {
                      new Pairing { White = player1, Black = player2, Result = PairingResult.Draw },
                      new Pairing { White = player3, Black = player4, Result = PairingResult.WhiteWins }
                     }
                    },

                    new Round
                    {
                     Number = 2,
                     Pairings = new List<Pairing>
                     {
                      new Pairing { White = player3, Black = player2, Result = PairingResult.WhiteWins },
                      new Pairing { White = player4, Black = player1, Result = PairingResult.BlackWins }
                     }
                    },

                    new Round
                    {
                     Number = 3,
                     Pairings = new List<Pairing>
                     {
                      new Pairing { White = player1, Black = player3, Result = PairingResult.BlackWins },
                      new Pairing { White = player2, Black = player4, Result = PairingResult.WhiteWins }
                     }
                    },

                    new Round
                    {
                     Number = 4,
                     Pairings = new List<Pairing>
                     {
                      new Pairing { White = player4, Black = player3, Result = PairingResult.BlackWins },
                      new Pairing { White = player2, Black = player1, Result = PairingResult.BlackWins }
                     }
                    }
                }
            };
        }
    }

    public class SectionPlayerForPairing
    {
        public Player Player { get; set; }
        public double Score { get; set; }
        //public int[] Opponents { get; set; }
        public IEnumerable<int> Opponents { get; set; }
        public ICollection<Player> Preferences { get; set; }
    }
}

