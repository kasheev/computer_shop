using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BasaDate.Models;

namespace BasaDate.Controllers
{
    public class personal_computerController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: personal_computer
        [Authorize]
        public ActionResult Index()
        {
            var personal_computer = db.personal_computer.Include(p => p.software);
            return View(personal_computer.ToList());
        }

        // GET: personal_computer/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personal_computer personal_computer = db.personal_computer.Find(id);
            if (personal_computer == null)
            {
                return HttpNotFound();
            }
            return View(personal_computer);
        }

        // GET: personal_computer/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.id_software = new SelectList(db.softwares, "id", "os");
            return View();
        }

        // POST: personal_computer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "id,id_software,keyboard,computer_mouse,monitor")] personal_computer personal_computer)
        {

            if (ModelState.IsValid)
            {
                db.personal_computer.Add(personal_computer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_software = new SelectList(db.softwares, "id", "os", personal_computer.id_software);
            return View(personal_computer);
        }

        // GET: personal_computer/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personal_computer personal_computer = db.personal_computer.Find(id);
            if (personal_computer == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_software = new SelectList(db.softwares, "id", "os", personal_computer.id_software);
            return View(personal_computer);
        }

        // POST: personal_computer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "id,id_software,keyboard,computer_mouse,monitor")] personal_computer personal_computer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personal_computer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_software = new SelectList(db.softwares, "id", "os", personal_computer.id_software);
            return View(personal_computer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Index(string search)
        {
         
             var result = db.personal_computer
                .Where(a => a.keyboard.ToLower().Contains(search.ToLower())
                || a.software.os.ToLower().Contains(search.ToLower()))
                .ToList();
            return View(result);
        }

        // GET: personal_computer/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personal_computer personal_computer = db.personal_computer.Find(id);
            if (personal_computer == null)
            {
                return HttpNotFound();
            }
            return View(personal_computer);
        }

        // POST: personal_computer/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            personal_computer personal_computer = db.personal_computer.Find(id);
            db.personal_computer.Remove(personal_computer);
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
