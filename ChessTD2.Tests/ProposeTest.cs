using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ChessTD2.console;

namespace ChessTD2.Tests
{
    [TestFixture]
    public class ProposeShould
    {
        Standings standings;
        Dictionary<int, PreferenceList> prefLists;

        [SetUp]
        public void Setup()
        {
            standings = new Standings { SectionPlayers = new List<SectionPlayer>() };
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 002, Rating = 1100, RoundResults = new List<double> { .5 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 003, Rating = 1200, RoundResults = new List<double> { 0 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 004, Rating = 1300, RoundResults = new List<double> { 0 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 005, Rating = 1400, RoundResults = new List<double> { .5 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 006, Rating = 1500, RoundResults = new List<double> { 0 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 007, Rating = 1600, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 008, Rating = 1700, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { } });

            prefLists = new Dictionary<int, PreferenceList>();
            for (int i = 0; i < standings.SectionPlayers.Count(); i++)
            {
                int id = standings.SectionPlayers.ElementAt(i).PlayerID;
                prefLists.Add(id, standings.GenerateIndividualPreferenceList(id));
            }
        }

        [Test]
        public void UseADictionaryOfAllPreferenceLists()
        {
            Assert.AreEqual(standings.SectionPlayers.Count(), prefLists.Count());
            //standings.Propose(prefLists[001].PlayerID, prefLists[001].PreferenceListIDs.First(), prefLists);
        }

        [Test]
        public void CorrectlyModifyRecipientListAfterFirstProposal()
        {
            var proposerID = 008;
            var recipientID = prefLists[proposerID].PreferenceListIDs.First();

            var listBefore = prefLists[recipientID].PreferenceListIDs;

            standings.Propose(proposerID, recipientID, prefLists);

            var listAfter = prefLists[recipientID].PreferenceListIDs;


            Assert.AreNotEqual(listBefore, listAfter);
            Assert.AreEqual(proposerID, listAfter.Last());
        }
    }
}
