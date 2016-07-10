using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ChessTD2.console;

namespace ChessTD2.Tests
{
    [TestFixture]
    public class PairMethodShould
    {
        List<SectionPlayer> standings;
        [SetUp]
        public void Setup()
        {
            standings = new List<SectionPlayer> ();
            standings.Add(new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<PairingResult> { } });
            standings.Add(new SectionPlayer { PlayerID = 002, Rating = 1100, RoundResults = new List<PairingResult> { } });
            standings.Add(new SectionPlayer { PlayerID = 003, Rating = 1200, RoundResults = new List<PairingResult> { } });
            standings.Add(new SectionPlayer { PlayerID = 004, Rating = 1300, RoundResults = new List<PairingResult> { } });
            standings.Add(new SectionPlayer { PlayerID = 005, Rating = 1400, RoundResults = new List<PairingResult> { } });
            standings.Add(new SectionPlayer { PlayerID = 006, Rating = 1500, RoundResults = new List<PairingResult> { } });
            standings.Add(new SectionPlayer { PlayerID = 007, Rating = 1600, RoundResults = new List<PairingResult> { } });
            standings.Add(new SectionPlayer { PlayerID = 008, Rating = 1700, RoundResults = new List<PairingResult> { } });
        }

        [Test]
        public void ReturnListOfPairings()
        {
            var result = Program.Pair(standings);

            Assert.IsInstanceOf<List<Pairing>>(result);
        }

        [Test]
        public void Return4PairingsFor8Players()
        {
            var result = Program.Pair(standings);

            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void Return4PairingsFor9Players()
        {
            standings.Add(new SectionPlayer { PlayerID = 009, Rating = 1800, RoundResults = new List<PairingResult> { } });
            var result = Program.Pair(standings);

            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void Return5PairingsFor10Players()
        {
            standings.Add(new SectionPlayer { PlayerID = 009, Rating = 1800, RoundResults = new List<PairingResult> { } });
            standings.Add(new SectionPlayer { PlayerID = 010, Rating = 1900, RoundResults = new List<PairingResult> { } });
            var result = Program.Pair(standings);

            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void CorrectlyPairFirstRoundFor8Players()
        {
            var result = Program.Pair(standings);

            Assert.AreEqual(008, result.ElementAt(0).White.PlayerID);
            Assert.AreEqual(004, result.ElementAt(0).Black.PlayerID);

            Assert.AreEqual(007, result.ElementAt(1).White.PlayerID);
            Assert.AreEqual(003, result.ElementAt(1).Black.PlayerID);
            
            Assert.AreEqual(006, result.ElementAt(2).White.PlayerID);
            Assert.AreEqual(002, result.ElementAt(2).Black.PlayerID);

            Assert.AreEqual(005, result.ElementAt(3).White.PlayerID);
            Assert.AreEqual(001, result.ElementAt(3).Black.PlayerID);
        }

        [Test]
        public void CorrectlyPairFirstRoundFor9Players()
        {
            standings.Add(new SectionPlayer { PlayerID = 009, Rating = 1800, RoundResults = new List<PairingResult> { } });
            var result = Program.Pair(standings);

            Assert.AreEqual(009, result.ElementAt(0).White.PlayerID);
            Assert.AreEqual(005, result.ElementAt(0).Black.PlayerID);

            Assert.AreEqual(008, result.ElementAt(1).White.PlayerID);
            Assert.AreEqual(004, result.ElementAt(1).Black.PlayerID);

            Assert.AreEqual(007, result.ElementAt(2).White.PlayerID);
            Assert.AreEqual(003, result.ElementAt(2).Black.PlayerID);

            Assert.AreEqual(006, result.ElementAt(3).White.PlayerID);
            Assert.AreEqual(002, result.ElementAt(3).Black.PlayerID);
        }
    }
    
}

