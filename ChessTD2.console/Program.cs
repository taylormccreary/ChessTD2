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
                result.Add(new Pairing { WhitePlayerID = standingsByRating.ElementAt(i).PlayerID, BlackPlayerID = standingsByRating.ElementAt(i + standingsByRating.Count / 2).PlayerID });
            }
            return result;
        }

        // creates individual preference list just by listing the players by rank and removing the given player
        public static List<SectionPlayer> GenerateIndividualPreferenceList(List<SectionPlayer> sectionPlayers, int id)
        {
            var rankedSectionPlayerList = sectionPlayers.OrderByDescending(sp => sp.RoundResults.Sum()).ThenByDescending(sp => sp.Rating).ToList();
            rankedSectionPlayerList.Remove(rankedSectionPlayerList.Where(sp => sp.PlayerID == id).First());
            return rankedSectionPlayerList;
        }
    }
}

