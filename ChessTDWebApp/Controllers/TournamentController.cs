using System.Collections.Generic;
using System.Web.Mvc;
using ChessTD2.console;

namespace ChessTDWebApp.Controllers
{
    public class TournamentController : Controller
    {
        // GET: Tournament
        public ActionResult Index()
        {
            List<SectionPlayer> sPlayers = CreatePlayers();
            SectionPlayer p = new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<double> { } };
            //ViewBag.Player = p;
            return View(p);
        }

        public List<SectionPlayer> CreatePlayers()
        {
            List<SectionPlayer> sectionPlayers = new List<SectionPlayer> ();
            sectionPlayers.Add(new SectionPlayer { PlayerID = 001, Rating = 1000, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 002, Rating = 1100, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 003, Rating = 1200, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 004, Rating = 1300, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 005, Rating = 1400, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 006, Rating = 1500, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 007, Rating = 1600, RoundResults = new List<double> { } });
            sectionPlayers.Add(new SectionPlayer { PlayerID = 008, Rating = 1700, RoundResults = new List<double> { } });

            return sectionPlayers;
        }
    }
}