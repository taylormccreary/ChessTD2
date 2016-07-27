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
        public static List<PotentialOpponentGroupings> GenerateIndividualPreferenceList(List<SectionPlayer> sectionPlayers, int id)
        {
            var currentPlayer = sectionPlayers.Where(sp => sp.PlayerID == id).First();

            // preference list is the the list of player groupings
            // we order it so that we can handle the same score section
            var currentPlayerPreferenceList = sectionPlayers
                .Select(sp => new PotentialOpponentGroupings()
                {
                    PlayerID = sp.PlayerID,
                    Rating = sp.Rating,
                    Score = sp.RoundResults.Sum(),
                    OpponentPlayerIDs = sp.OpponentPlayerIDs
                })
                // we need to keep the current player in the list to see which half of the score section he's in
                //.Where(sp => sp.PlayerID != currentPlayer.PlayerID)
                .OrderByDescending(p => p.Score)
                .ThenByDescending(p => p.Rating)
                .ThenBy(p => p.PlayerID)
                .ToList();

            // Assign relative score group to each player
            currentPlayerPreferenceList.ForEach(player => player.RelativeScoreGroup = currentPlayer.RoundResults.Sum().CompareTo(player.Score));

            // Order same score group halves
            // find index of first (highest ranked) player in score group
            int firstInScoreGroup = currentPlayerPreferenceList
                .IndexOf
                (
                    currentPlayerPreferenceList
                    .Where(p => p.Score == currentPlayer.RoundResults.Sum())
                    .First()
                );
                
            // find index of last (lowest ranked) player in score group
            int lastInScoreGroup = currentPlayerPreferenceList
                .IndexOf
                (
                    currentPlayerPreferenceList
                    .Where(p => p.Score == currentPlayer.RoundResults.Sum())
                    .Last()
                );

            // use the two indexes to determine index of first player in lower half
            int firstInLowerHalf = lastInScoreGroup - (lastInScoreGroup - firstInScoreGroup) / 2;
            
            // if the current player is before that player, he/she is in upper half
            bool currentPlayerIsInUpperHalf = currentPlayerPreferenceList
                .IndexOf
                (
                    currentPlayerPreferenceList
                    .Where(p => p.PlayerID == currentPlayer.PlayerID)
                    .First()
                )
                < firstInLowerHalf;

            int upperHalfRank = currentPlayerIsInUpperHalf ? 2 : 1;
            int lowerHalfRank = currentPlayerIsInUpperHalf ? 1 : 2;
            

            for (int i = firstInScoreGroup; i < firstInLowerHalf; i++)
            {
                currentPlayerPreferenceList.ElementAt(i).SameScoreGroupHalf = upperHalfRank;
            }

            for (int i = firstInLowerHalf; i <= lastInScoreGroup; i++)
            {
                currentPlayerPreferenceList.ElementAt(i).SameScoreGroupHalf = lowerHalfRank;
            }

            currentPlayerPreferenceList
                .FindAll(p => p.Score != currentPlayer.RoundResults.Sum())
                .ForEach(p => p.SameScoreGroupHalf = 999);

            // Assign opponents lower preference
            // Null coalescing operator along with elvis operator in case there are no opponents
            currentPlayerPreferenceList
                .FindAll(player => currentPlayer.OpponentPlayerIDs?.Contains(player.PlayerID) ?? false)
                .ForEach(player => player.OpponentGroup = 1);

            // Order players in preference list based on groupings
            currentPlayerPreferenceList = currentPlayerPreferenceList
                .Where(sp => sp.PlayerID != currentPlayer.PlayerID)
                .OrderBy(p => p.OpponentGroup)
                .ThenBy(p => p.RelativeScoreGroup)
                .ThenBy(p => p.SameScoreGroupHalf)
                .ThenByDescending(p => p.Score)
                .ThenByDescending(p => p.Rating)
                .ToList();
           
            return currentPlayerPreferenceList;
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

