﻿using System;
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
            var currentPlayerPreferenceList = sectionPlayers
                .Select(sp => new PotentialOpponentGroupings()
                {
                    PlayerID = sp.PlayerID,
                    Rating = sp.Rating,
                    Score = sp.RoundResults.Sum(),
                    OpponentPlayerIDs = sp.OpponentPlayerIDs
                })
                .Where(sp => sp.PlayerID != currentPlayer.PlayerID)
                .ToList();

            // Assign relative score group to each player
            currentPlayerPreferenceList.ForEach(player => player.RelativeScoreGroup = currentPlayer.RoundResults.Sum().CompareTo(player.Score));

            // Assign opponents lower preference
            // Null coalescing operator along with elvis operator in case there are no opponents
            currentPlayerPreferenceList
                .FindAll(player => currentPlayer.OpponentPlayerIDs?.Contains(player.PlayerID) ?? false)
                .ForEach(player => player.OpponentGroup = 1);

            // Order players in preference list based on groupings
            currentPlayerPreferenceList = currentPlayerPreferenceList
                .OrderBy(p => p.OpponentGroup)
                .ThenBy(p => p.RelativeScoreGroup)
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

