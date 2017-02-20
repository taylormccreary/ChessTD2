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

            // begin pairing setup
            Standings standings;
            List<Pairing> pairings;

            // creates SectionPlayers, puts them into standings, then performs initial sort
            standings = new Standings { SectionPlayers = new List<SectionPlayer>() };
            for (int i = 1; i <= 17; i++)
            {
                standings.SectionPlayers.Add(new SectionPlayer { PlayerID = i, Rating = i * 100, RoundResults = new List<double> { }, OpponentPlayerIDs = new List<int> { } });
            }
            

            // creates initial pairings
            pairings = standings.CreatePairings();

            PrintPairings(pairings);

            standings.AddRoundResults(pairings);
            pairings = standings.CreatePairings();

            PrintPairings(pairings);
            standings.AddRoundResults(pairings);
            pairings = standings.CreatePairings();

            PrintPairings(pairings);
            standings.AddRoundResults(pairings);
            pairings = standings.CreatePairings();

            PrintPairings(pairings);
            standings.AddRoundResults(pairings);
            pairings = standings.CreatePairings();

            PrintPairings(pairings);
            standings.AddRoundResults(pairings);
            pairings = standings.CreatePairings();

            PrintPairings(pairings);

            Console.ReadKey();
        }

        public static void PrintPairings(List<Pairing> pairings)
        {
            Console.WriteLine("Round " + pairings[0].RoundNumber + " Pairings");

            foreach (var pairing in pairings)
            {
                Console.WriteLine(pairing.WhitePlayerID + " vs. " + pairing.BlackPlayerID);
            }
        }

        // idk what the following methods are for
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


        public static List<SectionPlayer> ReducePreferenceLists(List<SectionPlayer> sectionPlayers)
        {

            return sectionPlayers;
        }

    }
}

