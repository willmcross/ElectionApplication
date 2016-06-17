using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectionApplication.Models;

namespace ElectionApplication.Controllers
{
    public class ElectionController : Controller
    {
        private ElectionApplicationDb db = new ElectionApplicationDb();

        //
        // GET: /Election/

        public ActionResult Index()
        {
            return View(db.Elections.ToList());
        }

        //
        // GET: /Election/Details/5

        public ActionResult Details(int id = 0)
        {
            Election election = db.Elections.Find(id);
            if (election == null)
            {
                return HttpNotFound();
            }
            return View(election);
        }

        //
        // GET: /Election/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Election/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Election election)
        {
            if (ModelState.IsValid)
            {
                db.Elections.Add(election);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(election);
        }

        //
        // GET: /Election/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Election election = db.Elections.Find(id);
            if (election == null)
            {
                return HttpNotFound();
            }
            return View(election);
        }

        //
        // POST: /Election/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Election election)
        {
            if (ModelState.IsValid)
            {
                db.Entry(election).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(election);
        }

        //
        // GET: /Election/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Election election = db.Elections.Find(id);
            if (election == null)
            {
                return HttpNotFound();
            }
            return View(election);
        }

        //
        // POST: /Election/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Election election = db.Elections.Find(id);
            db.Elections.Remove(election);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}