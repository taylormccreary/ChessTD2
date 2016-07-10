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
            for (int i = 0; i < standings.Count - 1; i += 2)
            {
                result.Add(new Pairing { White = standings.ElementAt(i), Black = standings.ElementAt(i + 1) });
            }
            return result;
        }
    }
}

