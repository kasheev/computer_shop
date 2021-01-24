using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BasaDate.Models;
using Npgsql;

namespace BasaDate.Controllers
{
    public class sessionsController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: sessions
        [Authorize]
        public ActionResult Index()
        {
            var sessions = db.sessions.Include(s => s.admin).Include(s => s.personal_computer).Include(s => s.service).Include(s => s.visitor);
            return View(sessions.ToList());
        }

        // GET: sessions/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            session session = db.sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: sessions/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.id_admin = new SelectList(db.admins, "id", "full_name");
            ViewBag.id_personal_computer = new SelectList(db.personal_computer, "id", "keyboard");
            ViewBag.id_services = new SelectList(db.services, "id", "title");
            ViewBag.id_visitors = new SelectList(db.visitors, "id", "full_name");
            return View();
        }

        // POST: sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "id,id_visitors,id_personal_computer,id_services,id_admin,date,start_time,end_time")] session session)
        {
            if (ModelState.IsValid)
            {
                db.sessions.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_admin = new SelectList(db.admins, "id", "full_name", session.id_admin);
            ViewBag.id_personal_computer = new SelectList(db.personal_computer, "id", "keyboard", session.id_personal_computer);
            ViewBag.id_services = new SelectList(db.services, "id", "title", session.id_services);
            ViewBag.id_visitors = new SelectList(db.visitors, "id", "full_name", session.id_visitors);
            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Index(string search)
        {
         
             var result = db.sessions
                .Where(a => a.visitor.full_name.ToLower().Contains(search.ToLower())
                || a.admin.full_name.ToLower().Contains(search.ToLower())
                || a.service.title.ToLower().Contains(search.ToLower())
                || a.date.ToString().Contains(search))
                .ToList();
            return View(result);
        }


        // GET: sessions/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            session session = db.sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_admin = new SelectList(db.admins, "id", "full_name", session.id_admin);
            ViewBag.id_personal_computer = new SelectList(db.personal_computer, "id", "keyboard", session.id_personal_computer);
            ViewBag.id_services = new SelectList(db.services, "id", "title", session.id_services);
            ViewBag.id_visitors = new SelectList(db.visitors, "id", "full_name", session.id_visitors);
            return View(session);
        }

        // POST: sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "id,id_visitors,id_personal_computer,id_services,id_admin,date,start_time,end_time")] session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_admin = new SelectList(db.admins, "id", "full_name", session.id_admin);
            ViewBag.id_personal_computer = new SelectList(db.personal_computer, "id", "keyboard", session.id_personal_computer);
            ViewBag.id_services = new SelectList(db.services, "id", "title", session.id_services);
            ViewBag.id_visitors = new SelectList(db.visitors, "id", "full_name", session.id_visitors);
            return View(session);
        }

        // GET: sessions/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            session session = db.sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            session session = db.sessions.Find(id);
            db.sessions.Remove(session);
            db.SaveChanges();
            return RedirectToAction("Index");
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
