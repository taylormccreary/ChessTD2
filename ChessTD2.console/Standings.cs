using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTD2.console
{
    public class Standings
    {

        public List<SectionPlayer> SectionPlayers { get; set; }

        public void AddRoundResults(List<Pairing> round)
        {
            foreach (var pairing in round)
            {
                var white = SectionPlayers.Where(p => p.PlayerID == pairing.WhitePlayerID).First();
                var black = SectionPlayers.Where(p => p.PlayerID == pairing.BlackPlayerID).First();

                white.OpponentPlayerIDs.Add(pairing.BlackPlayerID);
                black.OpponentPlayerIDs.Add(pairing.WhitePlayerID);

                if (pairing.Result == PairingResult.WhiteWins)
                {
                    white.RoundResults.Add(1);
                    black.RoundResults.Add(0);
                }
                else if (pairing.Result == PairingResult.BlackWins)
                {
                    white.RoundResults.Add(0);
                    black.RoundResults.Add(1);
                }
                else if (pairing.Result == PairingResult.Draw)
                {
                    white.RoundResults.Add(.5);
                    black.RoundResults.Add(.5);
                }
            }
        }

        public PreferenceList GenerateIndividualPreferenceList(int id)
        {
            var currentPlayer = SectionPlayers.Where(sp => sp.PlayerID == id).First();
            var currentPlayerScore = currentPlayer.RoundResults.Sum();

            // preference list is the the list of player groupings
            var currentPlayerPreferenceListGroupings = SectionPlayers
                .Select(sp => new PotentialOpponentGroupings()
                {
                    PlayerID = sp.PlayerID,
                    Rating = sp.Rating,
                    Score = sp.RoundResults.Sum(),
                    OpponentPlayerIDs = sp.OpponentPlayerIDs,
                    RelativeScoreGroup = currentPlayerScore.CompareTo(sp.RoundResults.Sum())
                })
                // we need to keep the current player in the list to see which half of the score section he's in
                //.Where(sp => sp.PlayerID != currentPlayer.PlayerID)
                .ToList();

            var sameScoreSection = currentPlayerPreferenceListGroupings
                .Where(p => p.Score == currentPlayerScore)
                .OrderByDescending(p => p.Rating)
                .ThenBy(p => p.PlayerID)
                .ToList();

            var topOfLowerHalf = sameScoreSection.Count() / 2;

            var currentPlayerIsInUppperHalf = sameScoreSection
                .IndexOf(
                    sameScoreSection
                    .Where(p => p.PlayerID == currentPlayer.PlayerID)
                    .First()
                ) < topOfLowerHalf;

            var upperHalfValue = currentPlayerIsInUppperHalf ? 2 : 1;
            var lowerHalfValue = currentPlayerIsInUppperHalf ? 1 : 2;

            for (int i = 0; i < sameScoreSection.Count(); i++)
            {
                if (i < topOfLowerHalf)
                {
                    sameScoreSection.ElementAt(i).SameScoreGroupHalf = upperHalfValue;
                }
                else
                {
                    sameScoreSection.ElementAt(i).SameScoreGroupHalf = lowerHalfValue;
                }
            }

            // Assign opponents lower preference
            // Null coalescing operator along with elvis operator in case there are no opponents
            currentPlayerPreferenceListGroupings
                .FindAll(player => currentPlayer.OpponentPlayerIDs?.Contains(player.PlayerID) ?? false)
                .ForEach(player => player.OpponentGroup = 1);

            // Order players in preference list based on groupings
            currentPlayerPreferenceListGroupings = currentPlayerPreferenceListGroupings
                .Where(sp => sp.PlayerID != currentPlayer.PlayerID)
                .OrderBy(p => p.OpponentGroup)
                .ThenBy(p => p.RelativeScoreGroup)
                .ThenBy(p => p.SameScoreGroupHalf)
                .ThenByDescending(p => p.Score)
                .ThenByDescending(p => p.Rating)
                .ThenBy(p => p.PlayerID)
                .ToList();

            var preferenceList = new PreferenceList()
            {
                PlayerID = currentPlayer.PlayerID,
                PreferenceListIDs = currentPlayerPreferenceListGroupings
                    .Select(p => p.PlayerID)
                    .ToList(),
                CurrentProposal = -1
            };


            return preferenceList;
        }


        public void Propose(int proposerID, int recipientID, Dictionary<int, PreferenceList> allPreferenceLists)
        {
            // check if proposer is in recipient's list
            if (allPreferenceLists[proposerID].PreferenceListIDs.Contains(recipientID))
            {
                // check if the recipient has a current proposal
                if (allPreferenceLists[recipientID].CurrentProposal != -1)
                {
                    // if he does, remove them from each others' lists and have that guy propose to his first choice
                    var currProp = allPreferenceLists[recipientID].CurrentProposal;
                    allPreferenceLists[currProp].PreferenceListIDs.Remove(recipientID);
                    allPreferenceLists[recipientID].PreferenceListIDs.Remove(currProp);
                    Propose(currProp, allPreferenceLists[currProp].PreferenceListIDs.First(), allPreferenceLists);
                }

                // set new current proposal of the recipient to the proposer
                allPreferenceLists[recipientID].CurrentProposal = proposerID;

                // find where proposer id is so we can drop everything after that
                var idsBeforeAndIncludingProposer = allPreferenceLists[recipientID].PreferenceListIDs
                    .IndexOf(proposerID)
                    + 1;

                // for all the ids we are about to drop, delete recipient id from their pref list
                for (int i = idsBeforeAndIncludingProposer; i < allPreferenceLists[recipientID].PreferenceListIDs.Count(); i++)
                {
                    var idOfOtherGuy = allPreferenceLists[recipientID].PreferenceListIDs[i];
                    allPreferenceLists[idOfOtherGuy].PreferenceListIDs.Remove(recipientID);
                }

                // drop the bottom of the recipients list
                allPreferenceLists[recipientID].PreferenceListIDs = allPreferenceLists[recipientID].PreferenceListIDs
                    .Take(idsBeforeAndIncludingProposer)
                    .ToList();
            }
            // else remove recipient from proposer's list
            else
            {
                allPreferenceLists[proposerID].PreferenceListIDs.Remove(recipientID);

                // have proposer propose to the next person on their list (the new first person)
                if (allPreferenceLists[proposerID].PreferenceListIDs.Count() > 0)
                {
                    Propose(proposerID, allPreferenceLists[proposerID].PreferenceListIDs.First(), allPreferenceLists);

                }
                else
                {
                    throw new Exception("Preference list is empty");
                }
            }
        }

        public List<Pairing> PairNextRound()
        {
            return new List<Pairing> { };
        }
    }
}
