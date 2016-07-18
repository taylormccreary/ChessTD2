using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ChessTD2.console;

namespace ChessTD2.Tests
{
    [TestFixture]
    public class GenerateIndividualPreferenceListShould
    {
        Standings standings = new Standings { SectionPlayers = new List<SectionPlayer>() };
        [SetUp]
        public void Setup()
        {
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<double> {1}, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 002, Rating = 1100, RoundResults = new List<double> {.5}, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 003, Rating = 1200, RoundResults = new List<double> {0}, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 004, Rating = 1300, RoundResults = new List<double> {0}, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 005, Rating = 1400, RoundResults = new List<double> {.5}, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 006, Rating = 1500, RoundResults = new List<double> {0}, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 007, Rating = 1600, RoundResults = new List<double> {1}, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 008, Rating = 1700, RoundResults = new List<double> {1}, OpponentPlayerIDs = new List<int> { } });
        }

        [Test]
        public void CreateListsWithAllPlayersExceptOne()
        {
            var list001 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 001);
            var list002 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 002);
            var list003 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 003);
            var list004 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 004);
            var list005 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 005);
            var list006 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 006);
            var list007 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 007);
            var list008 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 008);

            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list001.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list002.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list003.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list004.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list005.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list006.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list007.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list008.Count());
        }

        [Test]
        public void SortPlayersCorrectly()
        {
            var list007 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 007);

            Assert.AreEqual(008, list007.ElementAt(0).PlayerID);
            Assert.AreEqual(001, list007.ElementAt(1).PlayerID);
            Assert.AreEqual(005, list007.ElementAt(2).PlayerID);
            Assert.AreEqual(002, list007.ElementAt(3).PlayerID);
            Assert.AreEqual(006, list007.ElementAt(4).PlayerID);
            Assert.AreEqual(004, list007.ElementAt(5).PlayerID);
            Assert.AreEqual(003, list007.ElementAt(6).PlayerID);
        }
    }
}
