using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ChessTD2.console;

namespace ChessTD2.Tests
{
    [TestFixture]
    public class AddRoundResultsShould
    {
        Standings standings = new Standings { SectionPlayers = new List<SectionPlayer> () };
        List<Pairing> round1Pairings;
        [SetUp]
        public void Setup()
        {
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 002, Rating = 1100, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 003, Rating = 1200, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 004, Rating = 1300, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 005, Rating = 1400, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 006, Rating = 1500, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 007, Rating = 1600, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 008, Rating = 1700, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
            round1Pairings = Program.Pair(standings.SectionPlayers);
            foreach (var game in round1Pairings)
            {
                game.Result = PairingResult.WhiteWins;
            }
            standings.AddRoundResults(round1Pairings);
        }

        [Test]
        public void UpdateOpponentIDs()
        {

            Assert.Contains(004, standings.SectionPlayers.Where(p => p.PlayerID == 008).First().OpponentPlayerIDs.ToArray());
            Assert.Contains(008, standings.SectionPlayers.Where(p => p.PlayerID == 004).First().OpponentPlayerIDs.ToArray());

            Assert.Contains(003, standings.SectionPlayers.Where(p => p.PlayerID == 007).First().OpponentPlayerIDs.ToArray());
            Assert.Contains(007, standings.SectionPlayers.Where(p => p.PlayerID == 003).First().OpponentPlayerIDs.ToArray());

            Assert.Contains(002, standings.SectionPlayers.Where(p => p.PlayerID == 006).First().OpponentPlayerIDs.ToArray());
            Assert.Contains(006, standings.SectionPlayers.Where(p => p.PlayerID == 002).First().OpponentPlayerIDs.ToArray());

            Assert.Contains(001, standings.SectionPlayers.Where(p => p.PlayerID == 005).First().OpponentPlayerIDs.ToArray());
            Assert.Contains(005, standings.SectionPlayers.Where(p => p.PlayerID == 001).First().OpponentPlayerIDs.ToArray());
        }

        [Test]
        public void UpdateRoundResults()
        {
            Assert.AreEqual(1, standings.SectionPlayers.Where(p => p.PlayerID == 008).First().RoundResults.First());
            Assert.AreEqual(0, standings.SectionPlayers.Where(p => p.PlayerID == 004).First().RoundResults.First());

            Assert.AreEqual(1, standings.SectionPlayers.Where(p => p.PlayerID == 007).First().RoundResults.First());
            Assert.AreEqual(0, standings.SectionPlayers.Where(p => p.PlayerID == 003).First().RoundResults.First());

            Assert.AreEqual(1, standings.SectionPlayers.Where(p => p.PlayerID == 006).First().RoundResults.First());
            Assert.AreEqual(0, standings.SectionPlayers.Where(p => p.PlayerID == 002).First().RoundResults.First());

            Assert.AreEqual(1, standings.SectionPlayers.Where(p => p.PlayerID == 005).First().RoundResults.First());
            Assert.AreEqual(0, standings.SectionPlayers.Where(p => p.PlayerID == 001).First().RoundResults.First());
        }
    }
}
