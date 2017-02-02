using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ChessTD2.console;

namespace ChessTD2.Tests
{
    [TestFixture]
    class CreatePairingsShould
    {
        [TestFixture]
        class ForSimpleFirstRound
        {
            Standings standings;
            List<Pairing> pairings;

            public int HigherStandingPlayerId(int firstID, int secondID)
            {
                var firstPlayer = standings.SectionPlayers.Where(p => p.PlayerID == firstID).First();
                var firstPlayerIndexInStandings = standings.SectionPlayers.IndexOf(firstPlayer);

                var secondPlayer = standings.SectionPlayers.Where(p => p.PlayerID == secondID).First();
                var secondPlayerIndexInStandings = standings.SectionPlayers.IndexOf(secondPlayer);

                if (firstPlayerIndexInStandings < secondPlayerIndexInStandings)
                {
                    return firstID;
                }
                else
                {
                    return secondID;
                }
            }

            [SetUp]
            public void Setup()
            {
                standings = new Standings { SectionPlayers = new List<SectionPlayer>() };
                for (int i = 1; i <= 16; i++)
                {
                    standings.SectionPlayers.Add(new SectionPlayer { PlayerID = i, Rating = i * 100, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
                }

                standings.SectionPlayers = standings.SectionPlayers
                    .OrderByDescending(p => p.RoundResults.Sum())
                    .ThenByDescending(p => p.Rating)
                    .ThenByDescending(p => p.PlayerID)
                    .ToList();
                
                pairings = standings.CreatePairings();
            }

            [Test]
            public void ReturnTheCorrectNumberOfPairings()
            {
                Assert.AreEqual(standings.SectionPlayers.Count() / 2, pairings.Count());
            }

            [Test]
            public void AssignCorrectRoundNumber()
            {
                Assert.AreEqual(1, pairings[0].RoundNumber);
                Assert.AreEqual(1, pairings[1].RoundNumber);
                Assert.AreEqual(1, pairings[2].RoundNumber);
                Assert.AreEqual(1, pairings[3].RoundNumber);
                Assert.AreEqual(1, pairings[4].RoundNumber);
                Assert.AreEqual(1, pairings[5].RoundNumber);
                Assert.AreEqual(1, pairings[6].RoundNumber);
                Assert.AreEqual(1, pairings[7].RoundNumber);
            }

            [Test]
            public void NotOverlapPairings()
            {
                var pairedPlayers = new List<int> { };
                foreach (var pairing in pairings)
                {
                    pairedPlayers.Add(pairing.WhitePlayerID);
                    pairedPlayers.Add(pairing.BlackPlayerID);
                }
                Assert.AreEqual(pairedPlayers.Count(), pairedPlayers.Distinct().Count());
            }


            [Test]
            public void OrderPairingsByStandings()
            {
                for (int i = 0; i < pairings.Count() - 1; i++)
                {
                    var highestInUpperPairing = HigherStandingPlayerId(pairings.ElementAt(i).WhitePlayerID, pairings.ElementAt(i).BlackPlayerID);
                    var highestInLowerPairing = HigherStandingPlayerId(pairings.ElementAt(i + 1).WhitePlayerID, pairings.ElementAt(i + 1).BlackPlayerID);
                    // check that the highest standing player in this pairing
                    // is higher standing than the highest in the next pairing
                    Assert.AreEqual(highestInUpperPairing, HigherStandingPlayerId(highestInUpperPairing, highestInLowerPairing));
                }
            }

            [Test]
            public void GenerateCorrectPairings()
            {
                Assert.That(pairings[0].WhitePlayerID == 16 || pairings[0].BlackPlayerID == 16);
                Assert.That(pairings[0].WhitePlayerID == 8 || pairings[0].BlackPlayerID == 8);

                Assert.That(pairings[1].WhitePlayerID == 15 || pairings[1].BlackPlayerID == 15);
                Assert.That(pairings[1].WhitePlayerID == 7 || pairings[1].BlackPlayerID == 7);

                Assert.That(pairings[2].WhitePlayerID == 14 || pairings[2].BlackPlayerID == 14);
                Assert.That(pairings[2].WhitePlayerID == 6 || pairings[2].BlackPlayerID == 6);

                Assert.That(pairings[3].WhitePlayerID == 13 || pairings[3].BlackPlayerID == 13);
                Assert.That(pairings[3].WhitePlayerID == 5 || pairings[3].BlackPlayerID == 5);

                Assert.That(pairings[4].WhitePlayerID == 12 || pairings[4].BlackPlayerID == 12);
                Assert.That(pairings[4].WhitePlayerID == 4 || pairings[4].BlackPlayerID == 4);

                Assert.That(pairings[5].WhitePlayerID == 11 || pairings[5].BlackPlayerID == 11);
                Assert.That(pairings[5].WhitePlayerID == 3 || pairings[5].BlackPlayerID == 3);

                Assert.That(pairings[6].WhitePlayerID == 10 || pairings[6].BlackPlayerID == 10);
                Assert.That(pairings[6].WhitePlayerID == 2 || pairings[6].BlackPlayerID == 2);

                Assert.That(pairings[7].WhitePlayerID == 9 || pairings[7].BlackPlayerID == 9);
                Assert.That(pairings[7].WhitePlayerID == 1 || pairings[7].BlackPlayerID == 1);
            }

        }

        [TestFixture]
        class ForOddFirstRound
        {
            Standings standings;
            List<Pairing> pairings;

            // takes two IDs, returns ID of player with higher standing
            public int HigherStandingPlayerId(int firstID, int secondID)
            {
                var firstPlayer = standings.SectionPlayers.Where(p => p.PlayerID == firstID).First();
                var firstPlayerIndexInStandings = standings.SectionPlayers.IndexOf(firstPlayer);

                var secondPlayer = standings.SectionPlayers.Where(p => p.PlayerID == secondID).First();
                var secondPlayerIndexInStandings = standings.SectionPlayers.IndexOf(secondPlayer);

                if (firstPlayerIndexInStandings < secondPlayerIndexInStandings)
                {
                    return firstID;
                }
                else
                {
                    return secondID;
                }
            }

            [SetUp]
            public void Setup()
            {
                // creates SectionPlayers, puts them into standings, then performs initial sort
                standings = new Standings { SectionPlayers = new List<SectionPlayer>() };
                for (int i = 1; i <= 17; i++)
                {
                    standings.SectionPlayers.Add(new SectionPlayer { PlayerID = i, Rating = i * 100, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
                }

                standings.SectionPlayers = standings.SectionPlayers
                    .OrderByDescending(p => p.RoundResults.Sum())
                    .ThenByDescending(p => p.Rating)
                    .ThenByDescending(p => p.PlayerID)
                    .ToList();

                // creates initial pairings
                pairings = standings.CreatePairings();
            }

            [Test]
            public void ReturnTheCorrectNumberOfPairings()
            {
                Assert.AreEqual(standings.SectionPlayers.Count() / 2, pairings.Count());
            }

            [Test]
            public void AssignCorrectRoundNumber()
            {
                Assert.AreEqual(1, pairings[0].RoundNumber);
                Assert.AreEqual(1, pairings[1].RoundNumber);
                Assert.AreEqual(1, pairings[2].RoundNumber);
                Assert.AreEqual(1, pairings[3].RoundNumber);
                Assert.AreEqual(1, pairings[4].RoundNumber);
                Assert.AreEqual(1, pairings[5].RoundNumber);
                Assert.AreEqual(1, pairings[6].RoundNumber);
                Assert.AreEqual(1, pairings[7].RoundNumber);
            }

            [Test]
            // checks that each player ID appears only once in the pairings
            public void NotOverlapPairings()
            {
                var pairedPlayers = new List<int> { };
                foreach (var pairing in pairings)
                {
                    pairedPlayers.Add(pairing.WhitePlayerID);
                    pairedPlayers.Add(pairing.BlackPlayerID);
                }
                Assert.AreEqual(pairedPlayers.Count(), pairedPlayers.Distinct().Count());
            }


            [Test]
            public void OrderPairingsByStandings()
            {
                for (int i = 0; i < pairings.Count() - 1; i++)
                {
                    var highestInUpperPairing = HigherStandingPlayerId(pairings.ElementAt(i).WhitePlayerID, pairings.ElementAt(i).BlackPlayerID);
                    var highestInLowerPairing = HigherStandingPlayerId(pairings.ElementAt(i + 1).WhitePlayerID, pairings.ElementAt(i + 1).BlackPlayerID);
                    // check that the highest standing player in this pairing
                    // is higher standing than the highest in the next pairing
                    Assert.AreEqual(highestInUpperPairing, HigherStandingPlayerId(highestInUpperPairing, highestInLowerPairing));
                }
            }

            [Test]
            public void GenerateCorrectPairings()
            {
                Assert.That(pairings[0].WhitePlayerID == 17 || pairings[0].BlackPlayerID == 17);
                Assert.That(pairings[0].WhitePlayerID == 9 || pairings[0].BlackPlayerID == 9);

                Assert.That(pairings[1].WhitePlayerID == 16 || pairings[1].BlackPlayerID == 16);
                Assert.That(pairings[1].WhitePlayerID == 8 || pairings[1].BlackPlayerID == 8);

                Assert.That(pairings[2].WhitePlayerID == 15 || pairings[2].BlackPlayerID == 15);
                Assert.That(pairings[2].WhitePlayerID == 7 || pairings[2].BlackPlayerID == 7);

                Assert.That(pairings[3].WhitePlayerID == 14 || pairings[3].BlackPlayerID == 14);
                Assert.That(pairings[3].WhitePlayerID == 6 || pairings[3].BlackPlayerID == 6);

                Assert.That(pairings[4].WhitePlayerID == 13 || pairings[4].BlackPlayerID == 13);
                Assert.That(pairings[4].WhitePlayerID == 5 || pairings[4].BlackPlayerID == 5);

                Assert.That(pairings[5].WhitePlayerID == 12 || pairings[5].BlackPlayerID == 12);
                Assert.That(pairings[5].WhitePlayerID == 4 || pairings[5].BlackPlayerID == 4);

                Assert.That(pairings[6].WhitePlayerID == 11 || pairings[6].BlackPlayerID == 11);
                Assert.That(pairings[6].WhitePlayerID == 3 || pairings[6].BlackPlayerID == 3);

                Assert.That(pairings[7].WhitePlayerID == 10 || pairings[7].BlackPlayerID == 10);
                Assert.That(pairings[7].WhitePlayerID == 2 || pairings[7].BlackPlayerID == 2);
            }

        }
    }
}
