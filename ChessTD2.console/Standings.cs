using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessTD2.console
{
    public class Standings
    {

        public List<SectionPlayer> SectionPlayers { get; set; }

        // sorts standings in order to pair again
        public void Refresh()
        {
            SectionPlayers = SectionPlayers
                .OrderByDescending(p => p.RoundResults.Sum())
                .ThenByDescending(p => p.Rating)
                .ThenByDescending(p => p.PlayerID)
                .ToList();
        }

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
            var colorStatus = currentPlayer.CalculateColorStatus();

            // preference list is the the list of player groupings
            var currentPlayerPreferenceListGroupings = SectionPlayers
                .Select(sp => new PotentialOpponentGroupings()
                {
                    PlayerID = sp.PlayerID,
                    Rating = sp.Rating,
                    Score = sp.RoundResults.Sum(),
                    OpponentPlayerIDs = sp.OpponentPlayerIDs,
                    RelativeScoreGroup = currentPlayerScore.CompareTo(sp.RoundResults.Sum()),
                    RelativeColorGroup = PotentialOpponentGroupings.CalculateRelativeColorGroup(colorStatus, sp.CalculateColorStatus())
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
                .ThenBy(p => p.RelativeColorGroup)
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
            if (allPreferenceLists[recipientID].CurrentProposal != -1)
            {
                allPreferenceLists[proposerID].PreferenceListIDs.Remove(recipientID);
                Propose(proposerID, allPreferenceLists[proposerID].PreferenceListIDs.First(), allPreferenceLists);
            }
            else
            {
                allPreferenceLists[recipientID].CurrentProposal = proposerID;
                allPreferenceLists[recipientID].PreferenceListIDs = new List<int> { proposerID };
            }

        }

        public List<Pairing> CreatePairings()
        {
            // first, Refresh, so that the proposals happen in the right order
            Refresh();

            var round = SectionPlayers.First().RoundResults.Count() + 1;

            // if there's an odd amount of players, temporarily remove a player
            SectionPlayer removedPlayer = new SectionPlayer();
            bool oddPlayerExists = SectionPlayers.Count() % 2 != 0;
            if (oddPlayerExists)
            {
                removedPlayer = SectionPlayers.Last();
                removedPlayer.RoundResults.Add(1);
                SectionPlayers.Remove(SectionPlayers.Last());
            }

            // create preference lists that don't include the removed player, then add him back in
            var prefLists = new Dictionary<int, PreferenceList>();

            for (int i = 0; i < SectionPlayers.Count(); i++)
            {
                int id = SectionPlayers.ElementAt(i).PlayerID;
                prefLists.Add(id, GenerateIndividualPreferenceList(id));
            }

            if (oddPlayerExists)
            {
                SectionPlayers.Add(removedPlayer);
            }


            for (int i = 0; i < prefLists.Count(); i++)
            {
                var proposerID = prefLists.ElementAt(i).Key;
                Propose(proposerID, prefLists[proposerID].PreferenceListIDs.First(), prefLists);
            }

            var pairings = new List<Pairing> { };

            bool higherPlayerGetsWhite = true;
            while (prefLists.Count() > 1)
            {
                var white = prefLists.First().Key;
                var whiteSectionPlayer = SectionPlayers
                        .Where(p => p.PlayerID == white)
                        .First();

                var whiteColorStatus = prefLists[white].PreferredColor;


                var black = prefLists.First().Value.PreferenceListIDs.First();
                var blackSectionPlayer = SectionPlayers
                        .Where(p => p.PlayerID == black)
                        .First();

                var blackColorStatus = prefLists[black].PreferredColor;

                // if they both want the same thing, rating determines color
                if (whiteColorStatus == blackColorStatus)
                {
                    if (higherPlayerGetsWhite)
                    {
                        if (whiteSectionPlayer.Rating < blackSectionPlayer.Rating)
                        {
                            var temp = white;
                            white = black;
                            black = temp;
                        }
                    }
                    else
                    {
                        if (whiteSectionPlayer.Rating > blackSectionPlayer.Rating)
                        {
                            var temp = white;
                            white = black;
                            black = temp;
                        }
                    }

                    // alternate whether higher player gets white
                    higherPlayerGetsWhite = !higherPlayerGetsWhite;
                }
                // if white wants black and black wants white, switch 'em
                else if ((whiteColorStatus == ColorStatus.None || whiteColorStatus == ColorStatus.DueBlack || whiteColorStatus == ColorStatus.NeedsBlack) && (blackColorStatus == ColorStatus.None || blackColorStatus == ColorStatus.DueWhite || blackColorStatus == ColorStatus.NeedsWhite))
                {
                    var temp = white;
                    white = black;
                    black = temp;
                }
                // if black needs white and white does not (or vice versa), switch 'em
                else if (blackColorStatus == ColorStatus.NeedsWhite || whiteColorStatus == ColorStatus.NeedsBlack)
                {
                    var temp = white;
                    white = black;
                    black = temp;
                }
                // otherwise, leave 'em

                pairings.Add(new Pairing()
                {
                    WhitePlayerID = white,
                    BlackPlayerID = black,
                    RoundNumber = round
                });
                prefLists.Remove(prefLists.First().Value.PreferenceListIDs.First());
                prefLists.Remove(prefLists.First().Key);
            }

            return pairings;
        }
    }
}
