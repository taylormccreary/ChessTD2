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

            [Test]
            public void CorrectlyModifyListsAfterSecondHalfOfProposals()
            {
                var recipientID8 = prefLists[008].PreferenceListIDs.First();
                standings.Propose(8, recipientID8, prefLists);

                var recipientID7 = prefLists[007].PreferenceListIDs.First();
                standings.Propose(7, recipientID7, prefLists);

                var recipientID6 = prefLists[006].PreferenceListIDs.First();
                standings.Propose(6, recipientID6, prefLists);

                var recipientID5 = prefLists[005].PreferenceListIDs.First();
                standings.Propose(5, recipientID5, prefLists);

                var recipientID4 = prefLists[004].PreferenceListIDs.First();
                standings.Propose(4, recipientID8, prefLists);

                var recipientID3 = prefLists[003].PreferenceListIDs.First();
                standings.Propose(3, recipientID7, prefLists);

                var recipientID2 = prefLists[002].PreferenceListIDs.First();
                standings.Propose(2, recipientID6, prefLists);

                var recipientID1 = prefLists[001].PreferenceListIDs.First();
                standings.Propose(1, recipientID5, prefLists);

                Assert.AreEqual(8, prefLists[recipientID8].PreferenceListIDs.Last());
                Assert.AreEqual(recipientID8, prefLists[8].PreferenceListIDs.Last());

                Assert.AreEqual(7, prefLists[recipientID7].PreferenceListIDs.Last());
                Assert.AreEqual(recipientID7, prefLists[7].PreferenceListIDs.Last());

                Assert.AreEqual(6, prefLists[recipientID6].PreferenceListIDs.Last());
                Assert.AreEqual(recipientID6, prefLists[6].PreferenceListIDs.Last());

                Assert.AreEqual(5, prefLists[recipientID5].PreferenceListIDs.Last());
                Assert.AreEqual(recipientID5, prefLists[5].PreferenceListIDs.Last());

            }
        }

        [TestFixture]
        public class ForSecondRound
        {
            Standings standings;
            Dictionary<int, PreferenceList> prefLists;

            [SetUp]
            public void Setup()
            {
                standings = new Standings { SectionPlayers = new List<SectionPlayer>() };
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<double> { 0 }, OpponentPlayerIDs = new List<int> { 5 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 002, Rating = 1100, RoundResults = new List<double> { 0 }, OpponentPlayerIDs = new List<int> { 6 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 003, Rating = 1200, RoundResults = new List<double> { 0 }, OpponentPlayerIDs = new List<int> { 7 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 004, Rating = 1300, RoundResults = new List<double> { 0 }, OpponentPlayerIDs = new List<int> { 8 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 005, Rating = 1400, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { 1 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 006, Rating = 1500, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { 2 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 007, Rating = 1600, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { 3 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 008, Rating = 1700, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { 4 } });

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
            public void GeneratesCorrectPreferenceLists()
            {
                for (int i = 0; i < prefLists.Count(); i++)
                {
                    var proposerID = prefLists.ElementAt(i).Key;
                    standings.Propose(proposerID, prefLists[proposerID].PreferenceListIDs.First(), prefLists);
                }
                Assert.AreEqual(new List<int> { 3 }, prefLists[1].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 4 }, prefLists[2].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 1 }, prefLists[3].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 2 }, prefLists[4].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 7 }, prefLists[5].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 8 }, prefLists[6].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 5 }, prefLists[7].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 6 }, prefLists[8].PreferenceListIDs);
            }
        } 
    }

}

