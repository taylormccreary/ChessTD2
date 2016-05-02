using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTD2.Models;

namespace ChessTD2.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player();
            Console.WriteLine("hi!!");
            Console.ReadKey();
        }
    }

    class Init
    {
        public Section GetSection()
        {
            var player1 = new Player { PlayerID = 100, FirstName = "Taylor", LastName = "McCreary", Rating = 2000 };
            var player2 = new Player { PlayerID = 101, FirstName = "Michael", LastName = "McCreary", Rating = 900 };
            var player3 = new Player { PlayerID = 102, FirstName = "Debbie", LastName = "McCreary", Rating = 600 };
            var player4 = new Player { PlayerID = 103, FirstName = "Shannyn", LastName = "McCreary", Rating = 800 };
            var player5 = new Player { PlayerID = 104, FirstName = "Lily", Rating = 200 };




            return
               new Section
               {
                   Name = "championship",
                   SectionID = 1,
                   Players = new List<Player> { player1, player2, player3, player4, player5 },
                   Rounds = new List<Round>
              {
    new Round
    {
     Number = 1,
     Pairings = new List<Pairing>
     {
      new Pairing { White = player1, Black = player2, Result = PairingResult.Draw },
      new Pairing { White = player3, Black = player4, Result = PairingResult.White }
     }
    },




    new Round
    {
     Number = 2,
     Pairings = new List<Pairing>
     {
      new Pairing { White = player3, Black = player2, Result = PairingResult.White },
      new Pairing { White = player4, Black = player1, Result = PairingResult.Black }
     }
    },




    new Round
    {
     Number = 3,
     Pairings = new List<Pairing>
     {
      new Pairing { White = player1, Black = player3, Result = PairingResult.Black },
      new Pairing { White = player2, Black = player4, Result = PairingResult.White }
     }
    },
    new Round
    {
     Number = 4,
     Pairings = new List<Pairing>
     {
      new Pairing { White = player4, Black = player3, Result = PairingResult.Black },
      new Pairing { White = player2, Black = player1, Result = PairingResult.Black }
     }
    }
              }
               };
        }
    }
}
