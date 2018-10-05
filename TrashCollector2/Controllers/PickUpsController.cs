using Microsoft.AspNet.Identity;
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
            Employee employee = db.Employee.Where(e => e.Email == User.Identity.Name).Single();
            var pickUp = db.PickUps.Select(p => p.PickUpId).Distinct().ToList();
            var pickUps = db.Customer.Include(p => p.Address).Include(p => p.PickUps).Where(p => pickUp.Contains(p.PickId)).ToList();
            var customersInZip = pickUps.Where(p => p.Address.Zipcode == employee.ZipCode);
            return View(customersInZip);
        }

        [HttpPost]
        public ActionResult Index(string filterDay)
        {
            Employee employee = db.Employee.Where(e => e.Email == User.Identity.Name).Single();
            var pickUp = db.PickUps.Select(p => p.PickUpId).Distinct().ToList();
            var pickUps = db.Customer.Include(p => p.Address).Include(p => p.PickUps).Where(p => pickUp.Contains(p.PickId)).ToList();
            var customersInZip = pickUps.Where(p => p.Address.Zipcode == employee.ZipCode && p.PickUps.DayOfWeek == filterDay);
            return View(customersInZip);
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
        public ActionResult Create(PickUps pickUps, string Month, string Date, string DayOfWeek)
        {
            Customer customer = db.Customer.Where(c => c.UserName == User.Identity.Name).SingleOrDefault();
            var pickup = db.PickUps.Where(p => p.PickUpId == customer.PickId).Single();
            pickup.PickUpDate = new DateTime(2018, int.Parse(Month), int.Parse(Date));
            pickup.Cost += 50;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Details", "Customers");
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
            Customer customer = db.Customer.Where(c => c.PickId == pickUps.PickUpId).Single();
            customer.AccountBalance = pickUps.Cost;
            pickUps.Cost = 50;
            pickUps.PickUpDate = null;
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
