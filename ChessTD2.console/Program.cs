using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessTD2.console
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hi!!");

            Console.ReadKey();
        }

        public static List<Pairing> Pair(List<SectionPlayer> sectionPlayers)
        {
            var result = new List<Pairing> { };
            var standingsByRating = sectionPlayers.OrderByDescending(sp => sp.Rating).ToList();
            for (int i = 0; i < standingsByRating.Count / 2; i++)
            {
                result.Add(new Pairing
                {
                    WhitePlayerID = standingsByRating.ElementAt(i).PlayerID,
                    BlackPlayerID = standingsByRating.ElementAt(i + standingsByRating.Count / 2).PlayerID
                });
            }
            return result;
        }

        // creates individual preference list just by listing the players by rank and removing the given player
        public static List<SectionPlayer> GenerateIndividualPreferenceList(List<SectionPlayer> sectionPlayers, int id)
        {
            var currentPlayer = sectionPlayers.Where(sp => sp.PlayerID == id).First();
            var rankedSectionPlayerList = sectionPlayers
                .OrderByDescending(sp => sp.RoundResults.Sum())
                .ThenByDescending(sp => sp.Rating)
                .ToList();


            // sort score section so that opposite half comes first.
            var scoreSection = rankedSectionPlayerList
                .Where(sp => sp.RoundResults.Sum() == currentPlayer.RoundResults.Sum())
                .OrderByDescending(sp => sp.Rating)
                .ToList();

            var firstHalf = scoreSection.Take(scoreSection.Count() / 2);
            var secondHalf = scoreSection.Skip(scoreSection.Count() / 2);
            if (firstHalf.Contains(currentPlayer))
            {
                scoreSection = secondHalf.ToList();
                scoreSection.AddRange(firstHalf);
                
                var firstToReplace = rankedSectionPlayerList
                    .IndexOf
                    (
                        rankedSectionPlayerList
                        .Where(sp => sp.RoundResults.Sum() == currentPlayer.RoundResults.Sum())
                        .First()
                    );

                rankedSectionPlayerList.RemoveRange(firstToReplace, scoreSection.Count());
                rankedSectionPlayerList.InsertRange(firstToReplace, scoreSection);
            }

            // remove the player whose preference list we're generating
            rankedSectionPlayerList.Remove
            (
                rankedSectionPlayerList.Where(sp => sp.PlayerID == id).First()
            );

            // move opponents to the bottom of the list
            rankedSectionPlayerList = rankedSectionPlayerList
                .OrderBy(p => currentPlayer.OpponentPlayerIDs.Contains(p.PlayerID))
                .ThenByDescending(p => p.RoundResults.Sum())
                .ThenByDescending(p => p.Rating)
                .ToList();

            //// move opponents to the bottom of the list
            //foreach (var player in rankedSectionPlayerList.ToList()) // why the ToList? I'm not sure...see http://stackoverflow.com/questions/604831/collection-was-modified-enumeration-operation-may-not-execute
            //{
            //    if (currentPlayer.OpponentPlayerIDs.Contains(player.PlayerID))
            //    {
            //        rankedSectionPlayerList.Remove(player);
            //        rankedSectionPlayerList.Add(player);
            //    }
            //}

            return rankedSectionPlayerList;
        }

        public static List<SectionPlayer> ReducePreferenceLists(List<SectionPlayer> sectionPlayers)
        {

            return sectionPlayers;
        }

        public static void Propose(SectionPlayer proposer, SectionPlayer recipient)
        {
            int indexOfProposerInRecipientList = recipient.OpponentPlayerIDs.IndexOf(proposer.PlayerID);

            if (indexOfProposerInRecipientList > -1)
            {

            }
        }
    }
}

