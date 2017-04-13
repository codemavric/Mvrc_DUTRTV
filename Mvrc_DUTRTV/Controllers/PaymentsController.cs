using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvrc_DUTRTV.Models;

namespace Mvrc_DUTRTV.Controllers
{
    public class PaymentsController : Controller
    {
        private Tut12Entities db = new Tut12Entities();

        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Presenter);
            return View(payments.ToList());
        }

        // GET: Payments/Details/5s
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.PresenterId = new SelectList(db.Presenters, "PresenterId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        // Your form in Views/Payments/Create.cshtml is posting back a PresenterId from the DropDownList
        public ActionResult Create_Post(int presenterId)
        {
            var payment = new Payment();
            var presenter = db.Presenters.Find(presenterId); // now that we have the presenterId, we have to fetch it from the database
            if (presenter == null) // if the presenter is null, then a presenter with presenterId does not exist in our database and so the presenterId is invalid
            {
                ViewBag.PresenterId = new SelectList(db.Presenters, "PresenterId", "Name", payment.PresenterId);
                return View(payment);
            }

            // now that we have a valid presenter that we know is not null, we can populate the GrossPay variable.
            payment.GrossPay = presenter.MinimumWage;

            // we need to add our payment to the payments list in the presenter we fetched from the database. 
            presenter.Payments.Add(payment);

            // Entity Framework will now automatically populate the PresenterId on the payment when it is saves the payment to the database since it can see
            // that the payment belongs to the presenter since it is in the presenters Payments list.
            // Note we don't need to save the payment directly to the db.Payments DbSet. What we're doing is saving the presenter that includes a payment.
            // Entity framework then takes of the rest for you.
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PresenterId = new SelectList(db.Presenters, "PresenterId", "Name", payment.PresenterId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,GrossPay,Deduction,NettPay,PresenterId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PresenterId = new SelectList(db.Presenters, "PresenterId", "Name", payment.PresenterId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
