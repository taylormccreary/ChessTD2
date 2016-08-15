using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ChessTD2.console;

namespace ChessTD2.Tests
{
    [TestFixture]
    public class ProposeShould
    {

        [TestFixture]
        public class ForFirstRound
        {

            Standings standings;
            Dictionary<int, PreferenceList> prefLists;

            [SetUp]
            public void Setup()
            {
                standings = new Standings { SectionPlayers = new List<SectionPlayer>() };
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 002, Rating = 1100, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 003, Rating = 1200, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 004, Rating = 1300, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 005, Rating = 1400, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 006, Rating = 1500, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 007, Rating = 1600, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 008, Rating = 1700, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });

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
                Assert.AreEqual(1, listAfter.Count());
            }

            [Test]
            public void CorrectlyModifyListsAfterFirstHalfOfProposals()
            {
                var recipientID8 = prefLists[008].PreferenceListIDs.First();
                standings.Propose(8, recipientID8, prefLists);

                var recipientID7 = prefLists[007].PreferenceListIDs.First();
                standings.Propose(7, recipientID7, prefLists);

                var recipientID6 = prefLists[006].PreferenceListIDs.First();
                standings.Propose(6, recipientID6, prefLists);

                var recipientID5 = prefLists[005].PreferenceListIDs.First();
                standings.Propose(5, recipientID5, prefLists);
                
                Assert.AreEqual(8, prefLists[recipientID8].PreferenceListIDs.Last());
                Assert.AreEqual(1, prefLists[recipientID8].PreferenceListIDs.Count());

                Assert.AreEqual(7, prefLists[recipientID7].PreferenceListIDs.Last());
                Assert.AreEqual(2, prefLists[recipientID7].PreferenceListIDs.Count());

                Assert.AreEqual(6, prefLists[recipientID6].PreferenceListIDs.Last());
                Assert.AreEqual(3, prefLists[recipientID6].PreferenceListIDs.Count());

                Assert.AreEqual(5, prefLists[recipientID5].PreferenceListIDs.Last());
                Assert.AreEqual(4, prefLists[recipientID5].PreferenceListIDs.Count());

            }
        }
    }

}

