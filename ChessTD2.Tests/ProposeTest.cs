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
        public class ForRound1
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
            public void GeneratesCorrectPreferenceLists()
            {
                for (int i = 0; i < prefLists.Count(); i++)
                {
                    var proposerID = prefLists.ElementAt(i).Key;
                    standings.Propose(proposerID, prefLists[proposerID].PreferenceListIDs.First(), prefLists);
                }
                Assert.AreEqual(new List<int> { 5 }, prefLists[1].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 6 }, prefLists[2].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 7 }, prefLists[3].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 8 }, prefLists[4].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 1 }, prefLists[5].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 2 }, prefLists[6].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 3 }, prefLists[7].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 4 }, prefLists[8].PreferenceListIDs);
            }
        }

        [TestFixture]
        public class ForRound2
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


        [TestFixture]
        public class ForRound3
        {
            Standings standings;
            Dictionary<int, PreferenceList> prefLists;

            [SetUp]
            public void Setup()
            {
                standings = new Standings { SectionPlayers = new List<SectionPlayer>() };
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<double> { 0 }, OpponentPlayerIDs = new List<int> { 5, 3 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 002, Rating = 1100, RoundResults = new List<double> { 0 }, OpponentPlayerIDs = new List<int> { 6, 4 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 003, Rating = 1200, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { 7, 1 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 004, Rating = 1300, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { 8, 2 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 005, Rating = 1400, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { 1, 7 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 006, Rating = 1500, RoundResults = new List<double> { 2 }, OpponentPlayerIDs = new List<int> { 2, 8 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 007, Rating = 1600, RoundResults = new List<double> { 2 }, OpponentPlayerIDs = new List<int> { 3, 5 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 008, Rating = 1700, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { 4, 6 } });

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
                Assert.AreEqual(new List<int> { 2 }, prefLists[1].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 1 }, prefLists[2].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 8 }, prefLists[3].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 5 }, prefLists[4].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 4 }, prefLists[5].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 7 }, prefLists[6].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 6 }, prefLists[7].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 3 }, prefLists[8].PreferenceListIDs);
            }
        }


        [TestFixture]
        public class ForRound4
        {
            Standings standings;
            Dictionary<int, PreferenceList> prefLists;

            [SetUp]
            public void Setup()
            {
                standings = new Standings { SectionPlayers = new List<SectionPlayer>() };
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<double> { 0 }, OpponentPlayerIDs = new List<int> { 5, 3, 2 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 002, Rating = 1100, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { 6, 4, 1 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 003, Rating = 1200, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { 7, 1, 8 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 004, Rating = 1300, RoundResults = new List<double> { 2 }, OpponentPlayerIDs = new List<int> { 8, 2, 5 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 005, Rating = 1400, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { 1, 7, 4 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 006, Rating = 1500, RoundResults = new List<double> { 3 }, OpponentPlayerIDs = new List<int> { 2, 8, 7 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 007, Rating = 1600, RoundResults = new List<double> { 2 }, OpponentPlayerIDs = new List<int> { 3, 5, 6 } });
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 008, Rating = 1700, RoundResults = new List<double> { 2 }, OpponentPlayerIDs = new List<int> { 4, 6, 3 } });

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
                Assert.AreEqual(new List<int> { 2 }, prefLists[1].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 1 }, prefLists[2].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 5 }, prefLists[3].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 6 }, prefLists[4].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 3 }, prefLists[5].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 4 }, prefLists[6].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 8 }, prefLists[7].PreferenceListIDs);
                Assert.AreEqual(new List<int> { 7 }, prefLists[8].PreferenceListIDs);
            }
        }
    }

}

