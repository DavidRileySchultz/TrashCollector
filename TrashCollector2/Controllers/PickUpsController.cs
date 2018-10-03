using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector2.Models;

namespace TrashCollector2.Controllers
{
    public class PickUpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PickUps
        public ActionResult Index()
        {
            return View(db.PickUps.ToList());
        }

        // GET: PickUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUps pickUps = db.PickUps.Find(id);
            if (pickUps == null)
            {
                return HttpNotFound();
            }
            return View(pickUps);
        }

        // GET: PickUps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PickUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PickUpId,PickCustomerId,DayOfWeek,PickUpDate,Cost,Zipcode,SuspendPickUpStart,SuspendPickUpEnd")] PickUps pickUps)
        {
            if (ModelState.IsValid)
            {
                db.PickUps.Add(pickUps);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pickUps);
        }

        // GET: PickUps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUps pickUps = db.PickUps.Find(id);
            if (pickUps == null)
            {
                return HttpNotFound();
            }
            return View(pickUps);
        }

        // POST: PickUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PickUpId,PickCustomerId,DayOfWeek,PickUpDate,Cost,Zipcode,SuspendPickUpStart,SuspendPickUpEnd")] PickUps pickUps)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickUps).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pickUps);
        }

        // GET: PickUps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUps pickUps = db.PickUps.Find(id);
            if (pickUps == null)
            {
                return HttpNotFound();
            }
            return View(pickUps);
        }

        // POST: PickUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PickUps pickUps = db.PickUps.Find(id);
            db.PickUps.Remove(pickUps);
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
