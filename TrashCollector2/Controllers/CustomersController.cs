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
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customer.Include(c => c.Address).Include(c => c.PickUps);
            return View(customers.ToList());
        }
        public ActionResult SuspendPickUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuspendPickUp(string StartMonth, string StartDate, string EndMonth, string EndDate)
        {
            var customer = db.Customer.Where(c => c.UserName == User.Identity.Name).Single();
            var pickup = db.PickUps.Where(p => p.PickUpId == customer.PickId).Single();
            pickup.SuspendPickUpStart = new DateTime(2018, int.Parse(StartMonth), int.Parse(StartDate));
            pickup.SuspendPickUpEnd = new DateTime(2018, int.Parse(EndMonth), int.Parse(EndDate));
            db.SaveChanges();
            return RedirectToAction("Details", "Customers", new { id = customer.ID });
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            AddressViewModel addressViewModel = new AddressViewModel();
            Customer customer = null;
            if (id == null)
            {
                var FoundUserId = User.Identity.GetUserId();

                customer = db.Customer.Where(c => c.ApplicationUserId == FoundUserId).FirstOrDefault();
                return View(customer);
            }
            addressViewModel.customer = db.Customer.Find(id);
            addressViewModel.address = db.Address.Find(addressViewModel.customer.AddressID);
            addressViewModel.pickUps = db.PickUps.Where(c => c.PickUpId == addressViewModel.customer.PickId).Single();
            if (addressViewModel == null)
            {
                return HttpNotFound();
            }
            return View(addressViewModel);
        }

        // GET: Customers/Create
        [HttpGet]
        public ActionResult Create()
        {
            AddressViewModel addressViewModel = new AddressViewModel()
            {
                address = new Address(),
                customer = new Customer()
            };
            return View(addressViewModel);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddressViewModel addressViewModel, string DayOfWeek)
        {
            addressViewModel.pickUps = new PickUps();
            addressViewModel.pickUps.DayOfWeek = DayOfWeek;
            addressViewModel.pickUps.Zipcode = addressViewModel.address.Zipcode;
            addressViewModel.pickUps.PickCustomerId = addressViewModel.customer.ID;
            addressViewModel.address.ID = addressViewModel.customer.ID;
            addressViewModel.pickUps.Cost = 50;
            addressViewModel.customer.UserName = User.Identity.Name;
            addressViewModel.customer.Email = db.Users.Where(e => e.UserName == addressViewModel.customer.UserName).Select(e => e.Email).ToString();
            if (ModelState.IsValid)
            {
                db.Customer.Add(addressViewModel.customer);
                db.Address.Add(addressViewModel.address);
                db.PickUps.Add(addressViewModel.pickUps);
                db.SaveChanges();
                return RedirectToAction("Details", "Customers");
            }

            return View(addressViewModel.customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            AddressViewModel addressViewModel = new AddressViewModel();
            if (id == null)
            {
                return RedirectToAction("Create");
            }
            addressViewModel.customer = db.Customer.Find(id);
            addressViewModel.address = db.Address.Find(addressViewModel.customer.AddressID);
            if (addressViewModel.customer == null)
            {
                return HttpNotFound();
            }
            return View(addressViewModel.customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
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
