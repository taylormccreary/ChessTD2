using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ChessTD2.console;

namespace ChessTD2.Tests
{
    [TestFixture]
    class CreatePairingsShould
    {
        Standings standings;
        Dictionary<int, PreferenceList> prefLists;

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

            prefLists = new Dictionary<int, PreferenceList>();
            for (int i = 0; i < standings.SectionPlayers.Count(); i++)
            {
                int id = standings.SectionPlayers.ElementAt(i).PlayerID;
                prefLists.Add(id, standings.GenerateIndividualPreferenceList(id));
            }
        }

        [Test]
        public void ReturnTheCorrectNumberOfPairings()
        {
            Assert.AreEqual(standings.SectionPlayers.Count() / 2, standings.CreatePairings().Count());
        }

        [Test]
        public void NotOverlapPairings()
        {
            var pairings = standings.CreatePairings();
            var pairedPlayers = new List<int> { };
            foreach (var pairing in pairings)
            {
                pairedPlayers.Add(pairing.WhitePlayerID);
                pairedPlayers.Add(pairing.BlackPlayerID);
            }
            Assert.AreEqual(pairedPlayers.Count(), pairedPlayers.Distinct().Count());
        }

        public int HigherStandingPlayerId(int firstID, int secondID)
        {
            var firstPlayer = standings.SectionPlayers.Where(p => p.PlayerID == firstID).First();
            var firstPlayerIndexInStandings = standings.SectionPlayers.IndexOf(firstPlayer);

            var secondPlayer = standings.SectionPlayers.Where(p => p.PlayerID == secondID).First();
            var secondPlayerIndexInStandings = standings.SectionPlayers.IndexOf(secondPlayer);

            if(firstPlayerIndexInStandings < secondPlayerIndexInStandings)
            {
                return firstID;
            }
            else
            {
                return secondID;
            }
        }


        [Test]
        public void OrderPairingsByStandings()
        {
            var pairings = standings.CreatePairings();
            for (int i = 0; i < pairings.Count() - 1; i++)
            {
                var highestInUpperPairing = HigherStandingPlayerId(pairings.ElementAt(i).WhitePlayerID, pairings.ElementAt(i).BlackPlayerID);
                var highestInLowerPairing = HigherStandingPlayerId(pairings.ElementAt(i+1).WhitePlayerID, pairings.ElementAt(i+1).BlackPlayerID);
                // check that the highest standing player in this pairing
                // is higher standing than the highest in the next pairing
                Assert.AreEqual(highestInUpperPairing, HigherStandingPlayerId(highestInUpperPairing, highestInLowerPairing));
            }
        }

        [TestFixture]
        class GenerateCorrectPairings
        {

        }
    }
}
