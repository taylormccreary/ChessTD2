using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ChessTD2.Models;

namespace ChessTD2.DAL
{
    public class TDInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<TDContext>
    {
        protected override void Seed(TDContext context)
        {
            var OpenPlayers = new List<Player>
            {
            new Player{FirstName="Carson",LastName="Alexander",Rating=1000},
            new Player{FirstName="Meredith",LastName="Alonso",Rating=1000},
            new Player{FirstName="Arturo",LastName="Anand",Rating=1000},
            new Player{FirstName="Gytis",LastName="Barzdukas",Rating=1000},
            new Player{FirstName="Yan",LastName="Li",Rating=1000},
            new Player{FirstName="Peggy",LastName="Justice",Rating=1000},
            new Player{FirstName="Laura",LastName="Norman",Rating=1000},
            new Player{FirstName="Nino",LastName="Olivetto",Rating=1000}
            };

            OpenPlayers.ForEach(p => context.Players.Add(p));
            context.SaveChanges();

            var Secs = new List<Section>
            {
                new Section {Name="Open",Players=OpenPlayers }
            };

            var Tournaments = new List<Tournament>
            {
                new Tournament {Name = "Championship 2016", Sections = Secs }
            };

            Tournaments.ForEach(t => context.Tournaments.Add(t));
            context.SaveChanges();

        }
    }

}