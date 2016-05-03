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
			Console.WriteLine("hi!!");

			var players =
				from p in GetPlayers()
				where p.Rating > 100
				orderby p.Rating descending
				select new { name = p.FirstName, rating = p.Rating, fullName = p.FirstName + " " + p.LastName };

			var players2 = 
				GetPlayers()
				.Where(p => p.Rating > 100)
				.OrderByDescending(p => p.Rating)
				.Select(p => p.FirstName);

			var players3 =
				(from p in players
				where p.rating > 600
				select p.rating).Average();

			double avgRating =
				(from p in players
				select p.rating).Average();
				

			Console.WriteLine(players.Count());

			Console.ReadKey();
		}

		static List<Player> GetPlayers()
		{
			var player1 = new Player { PlayerID = 100, FirstName = "Taylor", LastName = "McCreary", Rating = 2000 };
			var player2 = new Player { PlayerID = 101, FirstName = "Michael", LastName = "McCreary", Rating = 900 };
			var player3 = new Player { PlayerID = 102, FirstName = "Debbie", LastName = "McCreary", Rating = 600 };
			var player4 = new Player { PlayerID = 103, FirstName = "Shannyn", LastName = "McCreary", Rating = 800 };
			var player5 = new Player { PlayerID = 104, FirstName = "Lily", Rating = 200 };
			var player6 = new Player { PlayerID = 105, FirstName = "Matt", Rating = 1200 };
			var player7 = new Player { PlayerID = 106, FirstName = "Sam", Rating = 1300 };
			var player8 = new Player { PlayerID = 107, FirstName = "John", Rating = 1567 };

			return new List<Player> { player1, player2, player3, player4, player5, player6, player7, player8 };
		}

		static Section GetSection()
		{
			var player1 = new Player { PlayerID = 100, FirstName = "Taylor", LastName = "McCreary", Rating = 2000 };
			var player2 = new Player { PlayerID = 101, FirstName = "Michael", LastName = "McCreary", Rating = 900 };
			var player3 = new Player { PlayerID = 102, FirstName = "Debbie", LastName = "McCreary", Rating = 600 };
			var player4 = new Player { PlayerID = 103, FirstName = "Shannyn", LastName = "McCreary", Rating = 800 };
			var player5 = new Player { PlayerID = 104, FirstName = "Lily", Rating = 200 };
			var player6 = new Player { PlayerID = 105, FirstName = "Matt", Rating = 1200 };
			var player7 = new Player { PlayerID = 106, FirstName = "Sam", Rating = 1300 };
			var player8 = new Player { PlayerID = 107, FirstName = "John", Rating = 1567 };

			return new Section
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
							new Pairing { White = player3, Black = player4, Result = PairingResult.WhiteWins }
						}
					},

					new Round
					{
						Number = 2,
						Pairings = new List<Pairing>
						{
							new Pairing { White = player3, Black = player2, Result = PairingResult.WhiteWins },
							new Pairing { White = player4, Black = player1, Result = PairingResult.BlackWins }
						}
					},

					new Round
					{
						Number = 3,
						Pairings = new List<Pairing>
						{
							new Pairing { White = player1, Black = player3, Result = PairingResult.BlackWins },
							new Pairing { White = player2, Black = player4, Result = PairingResult.WhiteWins }
						}
					},

					new Round
					{
						Number = 4,
						Pairings = new List<Pairing>
						{
							new Pairing { White = player4, Black = player3, Result = PairingResult.BlackWins },
							new Pairing { White = player2, Black = player1, Result = PairingResult.BlackWins }
						}
					}
				}
			};
		}

		static void GetScores()
		{
			var section = GetSection();
			var players2 = from p in section.Players
						   select new
						   {
							   Player = p,

							   Score =
						   (
						   from ro in section.Rounds
						   from pr in ro.Pairings
						   where pr.Black == p || pr.White == p
						   select
						   ((pr.Black == p && pr.Result == PairingResult.BlackWins) || (pr.White == p && pr.Result == PairingResult.WhiteWins)) ? 1 :
						   pr.Result == PairingResult.Draw && (pr.Black == p || pr.White == p) ? 0.5 : 0
						   ).Sum(),

							   opponents =
						   (
						   from ro in section.Rounds
						   from pr in ro.Pairings
						   where pr.Black == p || pr.White == p
						   select pr.Black == p ? pr.White : pr.Black
						   ).Distinct()
						   };
		}
	}
}

