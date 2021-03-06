﻿using System;
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
    public class PresentersController : Controller
    {
        private Tut12Entities db = new Tut12Entities();

        // GET: Presenters
        public ActionResult Index()
        {
            return View(db.Presenters.ToList());
        }

        // GET: Presenters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presenter presenter = db.Presenters.Find(id);
            if (presenter == null)
            {
                return HttpNotFound();
            }
            return View(presenter);
        }

        // GET: Presenters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Presenters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PresenterId,Name,Contact,Email,MinimumWage,TaxDeduction")] Presenter presenter)
        {
            if (ModelState.IsValid)
            {
                db.Presenters.Add(presenter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(presenter);
        }

        // GET: Presenters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presenter presenter = db.Presenters.Find(id);
            if (presenter == null)
            {
                return HttpNotFound();
            }
            return View(presenter);
        }

        // POST: Presenters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PresenterId,Name,Contact,Email,MinimumWage,TaxDeduction")] Presenter presenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presenter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(presenter);
        }

        // GET: Presenters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presenter presenter = db.Presenters.Find(id);
            if (presenter == null)
            {
                return HttpNotFound();
            }
            return View(presenter);
        }

        // POST: Presenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Presenter presenter = db.Presenters.Find(id);
            db.Presenters.Remove(presenter);
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
