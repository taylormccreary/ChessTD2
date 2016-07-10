using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ChessTD2.console;

namespace ChessTD2.Tests
{
    [TestFixture]
    public class PairMethodShould
    {
        List<SectionPlayer> standings = new List<SectionPlayer> { };
        [SetUp]
        public void Setup()
        {
            standings.Add(new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<PairingResult> { } });
            standings.Add(new SectionPlayer { PlayerID = 002, Rating = 1100, RoundResults = new List<PairingResult> { } });
            standings.Add(new SectionPlayer { PlayerID = 003, Rating = 1200, RoundResults = new List<PairingResult> { } });
            standings.Add(new SectionPlayer { PlayerID = 004, Rating = 13000, RoundResults = new List<PairingResult> { } });
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
    }
    
}

