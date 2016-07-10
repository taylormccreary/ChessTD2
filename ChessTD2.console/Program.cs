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

        public static List<Pairing> Pair(List<SectionPlayer> standings)
        {
            var result = new List<Pairing> { };
            var standingsByRating = standings.OrderByDescending(sp => sp.Rating).ToList();
            for (int i = 0; i < standingsByRating.Count / 2; i++)
            {
                result.Add(new Pairing { White = standingsByRating.ElementAt(i), Black = standingsByRating.ElementAt(i + standingsByRating.Count / 2) });
            }
            return result;
        }
    }
}

