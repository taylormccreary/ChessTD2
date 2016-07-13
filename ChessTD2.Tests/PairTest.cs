using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ChessTD2.console;

namespace ChessTD2.Tests
{
    [TestFixture]
    public class PairMethodShould
    {
        List<SectionPlayer> sectionPlayers;
        [SetUp]
        public void Setup()
        {
            sectionPlayers = new List<SectionPlayer> ();
            sectionPlayers.Add(new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 002, Rating = 1100, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 003, Rating = 1200, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 004, Rating = 1300, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 005, Rating = 1400, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 006, Rating = 1500, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 007, Rating = 1600, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 008, Rating = 1700, RoundResults = new List<double> { } });
        }

        [Test]
        public void ReturnListOfPairings()
        {
            var result = Program.Pair(sectionPlayers);

            Assert.IsInstanceOf<List<Pairing>>(result);
        }

        [Test]
        public void Return4PairingsFor8Players()
        {
            var result = Program.Pair(sectionPlayers);

            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void Return4PairingsFor9Players()
        {
            sectionPlayers.Add(new SectionPlayer { PlayerID = 009, Rating = 1800, RoundResults = new List<double> { } });
            var result = Program.Pair(sectionPlayers);

            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void Return5PairingsFor10Players()
        {
            sectionPlayers.Add(new SectionPlayer { PlayerID = 009, Rating = 1800, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 010, Rating = 1900, RoundResults = new List<double> { } });
            var result = Program.Pair(sectionPlayers);

            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void CorrectlyPairFirstRoundFor8Players()
        {
            var result = Program.Pair(sectionPlayers);

            Assert.AreEqual(008, result.ElementAt(0).WhitePlayerID);
            Assert.AreEqual(004, result.ElementAt(0).BlackPlayerID);

            Assert.AreEqual(007, result.ElementAt(1).WhitePlayerID);
            Assert.AreEqual(003, result.ElementAt(1).BlackPlayerID);
            
            Assert.AreEqual(006, result.ElementAt(2).WhitePlayerID);
            Assert.AreEqual(002, result.ElementAt(2).BlackPlayerID);

            Assert.AreEqual(005, result.ElementAt(3).WhitePlayerID);
            Assert.AreEqual(001, result.ElementAt(3).BlackPlayerID);
        }

        [Test]
        public void CorrectlyPairFirstRoundFor9Players()
        {
            sectionPlayers.Add(new SectionPlayer { PlayerID = 009, Rating = 1800, RoundResults = new List<double> { } });
            var result = Program.Pair(sectionPlayers);

            Assert.AreEqual(009, result.ElementAt(0).WhitePlayerID);
            Assert.AreEqual(005, result.ElementAt(0).BlackPlayerID);

            Assert.AreEqual(008, result.ElementAt(1).WhitePlayerID);
            Assert.AreEqual(004, result.ElementAt(1).BlackPlayerID);

            Assert.AreEqual(007, result.ElementAt(2).WhitePlayerID);
            Assert.AreEqual(003, result.ElementAt(2).BlackPlayerID);

            Assert.AreEqual(006, result.ElementAt(3).WhitePlayerID);
            Assert.AreEqual(002, result.ElementAt(3).BlackPlayerID);
        }
        
    }
    
}

