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
            Assert.AreEqual(standings.SectionPlayers.Count() / 2, standings.CreatePairings(prefLists).Count());
        }

        [Test]
        public void NotOverlapPairings()
        {
            var pairings = standings.CreatePairings(prefLists);
            var pairedPlayers = new List<int> { };
            foreach (var pairing in pairings)
            {
                pairedPlayers.Add(pairing.WhitePlayerID);
                pairedPlayers.Add(pairing.BlackPlayerID);
            }
            Assert.AreEqual(pairedPlayers.Count(), pairedPlayers.Distinct().Count());
        }

        public int HigherStandingPlayerId(Pairing pairing)
        {
            var whitePlayer = standings.SectionPlayers.Where(p => p.PlayerID == pairing.WhitePlayerID).First();
            var whiteIndexInStandings = standings.SectionPlayers.IndexOf(whitePlayer);

            var blackPlayer = standings.SectionPlayers.Where(p => p.PlayerID == pairing.BlackPlayerID).First();
            var blackIndexInStandings = standings.SectionPlayers.IndexOf(blackPlayer);

            if(whiteIndexInStandings < blackIndexInStandings)
            {
                return pairing.WhitePlayerID;
            }
            else
            {
                return pairing.BlackPlayerID;
            }
        }


        [Test]
        public void OrderPairingsByStandings()
        {
            var pairings = standings.CreatePairings(prefLists);
            for (int i = 0; i < pairings.Count() - 1; i++)
            {
                // check that the highest standing player in this pairing
                // is higher standing than the highest in the next pairing
                Assert.Greater(HigherStandingPlayerId(pairings.ElementAt(i)), HigherStandingPlayerId(pairings.ElementAt(i + 1)));
            }
        }

        [TestFixture]
        class GenerateCorrectPairings
        {

        }
    }
}
