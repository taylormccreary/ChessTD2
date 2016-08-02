using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ChessTD2.console;

namespace ChessTD2.Tests
{
    [TestFixture]
    public class GenerateIndividualPreferenceListShould
    {
        Standings standings;
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

            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list001.PreferenceListIDs.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list002.PreferenceListIDs.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list003.PreferenceListIDs.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list004.PreferenceListIDs.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list005.PreferenceListIDs.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list006.PreferenceListIDs.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list007.PreferenceListIDs.Count());
            Assert.AreEqual(standings.SectionPlayers.Count() - 1, list008.PreferenceListIDs.Count());
        }

        [Test]
        public void SortPlayersByScoreAndRating()
        {
            var list005 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 005);

            Assert.AreEqual(008, list005.PreferenceListIDs.ElementAt(0));
            Assert.AreEqual(007, list005.PreferenceListIDs.ElementAt(1));
            Assert.AreEqual(001, list005.PreferenceListIDs.ElementAt(2));
            Assert.AreEqual(002, list005.PreferenceListIDs.ElementAt(3));
            Assert.AreEqual(006, list005.PreferenceListIDs.ElementAt(4));
            Assert.AreEqual(004, list005.PreferenceListIDs.ElementAt(5));
            Assert.AreEqual(003, list005.PreferenceListIDs.ElementAt(6));
        }

        [Test]
        public void SortPlayersCorrectly()
        {
            var list007 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 007);

            Assert.AreEqual(008, list007.PreferenceListIDs.ElementAt(0));
            Assert.AreEqual(001, list007.PreferenceListIDs.ElementAt(1));
            Assert.AreEqual(005, list007.PreferenceListIDs.ElementAt(2));
            Assert.AreEqual(002, list007.PreferenceListIDs.ElementAt(3));
            Assert.AreEqual(006, list007.PreferenceListIDs.ElementAt(4));
            Assert.AreEqual(004, list007.PreferenceListIDs.ElementAt(5));
            Assert.AreEqual(003, list007.PreferenceListIDs.ElementAt(6));
        }

        [Test]
        public void MoveOpponentsToTheBottom()
        {
            standings.SectionPlayers.Where(sp => sp.PlayerID == 007).First().OpponentPlayerIDs.Add(005);
            standings.SectionPlayers.Where(sp => sp.PlayerID == 007).First().OpponentPlayerIDs.Add(001);

            var list007 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 007);

            Assert.AreEqual(008, list007.PreferenceListIDs.ElementAt(0));
            Assert.AreEqual(002, list007.PreferenceListIDs.ElementAt(1));
            Assert.AreEqual(006, list007.PreferenceListIDs.ElementAt(2));
            Assert.AreEqual(004, list007.PreferenceListIDs.ElementAt(3));
            Assert.AreEqual(003, list007.PreferenceListIDs.ElementAt(4));
            Assert.AreEqual(001, list007.PreferenceListIDs.ElementAt(5));
            Assert.AreEqual(005, list007.PreferenceListIDs.ElementAt(6));
        }

        [Test]
        public void SortScoreSectionCorrectly007UpperHalf()
        {
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 009, Rating = 900, RoundResults = new List<double> { 1 }, OpponentPlayerIDs = new List<int> { } });

            var list007 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 007);

            Assert.AreEqual(001, list007.PreferenceListIDs.ElementAt(0));
            Assert.AreEqual(009, list007.PreferenceListIDs.ElementAt(1));
            Assert.AreEqual(008, list007.PreferenceListIDs.ElementAt(2));
            Assert.AreEqual(005, list007.PreferenceListIDs.ElementAt(3));
            Assert.AreEqual(002, list007.PreferenceListIDs.ElementAt(4));
            Assert.AreEqual(006, list007.PreferenceListIDs.ElementAt(5));
            Assert.AreEqual(004, list007.PreferenceListIDs.ElementAt(6));
            Assert.AreEqual(003, list007.PreferenceListIDs.ElementAt(7));
        }

        [Test]
        public void SortScoreSectionCorrectly002LowerHalf()
        {

            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 009, Rating = 1500, RoundResults = new List<double> { .5 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 010, Rating = 1600, RoundResults = new List<double> { .5 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 011, Rating = 1700, RoundResults = new List<double> { .5 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 012, Rating = 1800, RoundResults = new List<double> { .5 }, OpponentPlayerIDs = new List<int> { } });

            var list002 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 002);

            // 1 pt
            Assert.AreEqual(008, list002.PreferenceListIDs.ElementAt(0));
            Assert.AreEqual(007, list002.PreferenceListIDs.ElementAt(1));
            Assert.AreEqual(001, list002.PreferenceListIDs.ElementAt(2));

            // .5 pts
            // upper half
            Assert.AreEqual(012, list002.PreferenceListIDs.ElementAt(3));
            Assert.AreEqual(011, list002.PreferenceListIDs.ElementAt(4));
            Assert.AreEqual(010, list002.PreferenceListIDs.ElementAt(5));

            // lower half
            Assert.AreEqual(009, list002.PreferenceListIDs.ElementAt(6));
            Assert.AreEqual(005, list002.PreferenceListIDs.ElementAt(7));

            // 0 pts
            Assert.AreEqual(006, list002.PreferenceListIDs.ElementAt(8));
            Assert.AreEqual(004, list002.PreferenceListIDs.ElementAt(9));
            Assert.AreEqual(003, list002.PreferenceListIDs.ElementAt(10));
        }

        [Test]
        public void SortScoreSectionCorrectly002UpperHalf()
        {
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 009, Rating = 700, RoundResults = new List<double> { .5 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 010, Rating = 800, RoundResults = new List<double> { .5 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 011, Rating = 900, RoundResults = new List<double> { .5 }, OpponentPlayerIDs = new List<int> { } });
            standings.SectionPlayers.Add(new SectionPlayer { PlayerID = 012, Rating = 1000, RoundResults = new List<double> { .5 }, OpponentPlayerIDs = new List<int> { } });


            var list002 = Program.GenerateIndividualPreferenceList(standings.SectionPlayers, 002);

            // 1 pt
            Assert.AreEqual(008, list002.PreferenceListIDs.ElementAt(0));
            Assert.AreEqual(007, list002.PreferenceListIDs.ElementAt(1));
            Assert.AreEqual(001, list002.PreferenceListIDs.ElementAt(2));

            // .5 pts
            // lower half
            Assert.AreEqual(011, list002.PreferenceListIDs.ElementAt(3));
            Assert.AreEqual(010, list002.PreferenceListIDs.ElementAt(4));
            Assert.AreEqual(009, list002.PreferenceListIDs.ElementAt(5));

            // upper half
            Assert.AreEqual(005, list002.PreferenceListIDs.ElementAt(6));
            Assert.AreEqual(012, list002.PreferenceListIDs.ElementAt(7));

            // 0 pts
            Assert.AreEqual(006, list002.PreferenceListIDs.ElementAt(8));
            Assert.AreEqual(004, list002.PreferenceListIDs.ElementAt(9));
            Assert.AreEqual(003, list002.PreferenceListIDs.ElementAt(10));
        }
    }
}