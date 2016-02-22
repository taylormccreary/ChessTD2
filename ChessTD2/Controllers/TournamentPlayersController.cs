﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChessTD2.DAL;
using ChessTD2.Models;

namespace ChessTD2.Controllers
{
    public class ParticipantsViewModel
    {
        public ICollection<Player> Participants { get; set; }
        public ICollection<Player> NonParticipants { get; set; }
        public int TournamentID { get; set; }
    }
    public class TournamentPlayersController : Controller
    {
        private TDContext db = new TDContext();

        public ActionResult Participants(int id)
        {
            var vm = new ParticipantsViewModel();
            vm.TournamentID = id;
            vm.Participants = db.Tournaments.Where(t => t.TournamentID == id).First().Players.OrderBy(p=>p.LastName).ToList();

            var participantIds = vm.Participants.Select(p => p.PlayerID).ToArray();
            vm.NonParticipants = db.Players.Where(np => !participantIds.Contains(np.PlayerID)).OrderBy(np=>np.LastName).ToList();

            return View(vm);
        }


        // GET: TournamentPlayers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        
        public ActionResult Remove(int playerID, int tournamentID)
        {
            var player = db.Players.Find(playerID);
            db.Tournaments.Include(p=>p.Players).Where(t => t.TournamentID == tournamentID).First().Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction(nameof(TournamentPlayersController.Participants), new { id = tournamentID });
        }

        public ActionResult Add(int playerID, int tournamentID)
        {
            var player = db.Players.Find(playerID);
            db.Tournaments.Include(p => p.Players).Where(t => t.TournamentID == tournamentID).First().Players.Add(player);
            db.SaveChanges();
            return RedirectToAction(nameof(TournamentPlayersController.Participants), new { id = tournamentID });
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}