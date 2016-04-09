using System;
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
    public class SectionController : Controller
    {
        private TDContext db = new TDContext();
        

        public ActionResult SectionList(int id)
        {
            return View(db.Tournaments.Where(t => t.TournamentID == id).First());
        }

        // GET: Section/Details/5
        public ActionResult Details(int? sId, int? tId)
        {
            if (sId == null || tId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Section section = db.Tournaments.Find(tId).Sections.Where(s => s.SectionID == sId).First();

            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: Section/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Section/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SectionID,Name")] Section section, int tID)
        {
            if (ModelState.IsValid)
            {
                section.Tournament = db.Tournaments.Where(t => t.TournamentID == tID).First();
                db.Tournaments.Where(t => t.TournamentID == tID).First().Sections.Add(section);
                //db.Sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("SectionList", new { id = tID });
            }

            return View(section);
        }

        // GET: Section/Edit/5
        public ActionResult Edit(int? sId, int? tId)
        {
            if (sId == null || tId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Section section = db.Tournaments.Find(tId).Sections.Where(s => s.SectionID == sId).First();

            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Section/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SectionID,Name,Tournament")] Section section)
        {

            if (ModelState.IsValid)
            {
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SectionList", new { id = section.Tournament.TournamentID });
            }
            return View(section);
        }

        // GET: Section/Delete/5
        public ActionResult Delete(int? sId, int? tId)
        {
            if (sId == null || tId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Section section = db.Tournaments.Find(tId).Sections.Where(s => s.SectionID == sId).First();

            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Section/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "SectionID,Tournament")] Section section)
        {
            //Section section = db.Sections.Find(sId);
            db.Sections.Remove(db.Sections.Where(s=>s.SectionID == section.SectionID).First());
            db.SaveChanges();
            return RedirectToAction("SectionList", new { id = section.Tournament.TournamentID });
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
