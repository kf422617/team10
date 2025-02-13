﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using team10.DAL;
using team10.Models;

namespace team10.Controllers
{
    public class CentricUsersController : Controller
    {
        private Team10Context db = new Team10Context();

        // GET: CentricUsers
        public ActionResult Index()
        {
            return View(db.CentricUser.ToList());
        }

        // GET: CentricUsers/Details/5
        [Authorize]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentricUser centricUser = db.CentricUser.Find(id);
            if (centricUser == null)
            {
                return HttpNotFound();
            }
            return View(centricUser);
        }

        // GET: CentricUsers/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CentricUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "CentricUserID,firstName,lastName,birthday,UserTitle,UserLocation")] CentricUser centricUser)
        {
            if (ModelState.IsValid)
            {
                Guid memberID;
                Guid.TryParse(User.Identity.GetUserId(), out memberID);
                centricUser.CentricUserID = memberID;
                db.CentricUser.Add(centricUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(centricUser);
        }

        // GET: CentricUsers/Edit/5
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentricUser centricUser = db.CentricUser.Find(id);
            if (centricUser == null)
            {
                return HttpNotFound();
            }
            return View(centricUser);
        }

        // POST: CentricUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "CentricUserID,firstName,lastName,birthday,UserTitle,UserLocation")] CentricUser centricUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(centricUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(centricUser);
        }

        // GET: CentricUsers/Delete/5
        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentricUser centricUser = db.CentricUser.Find(id);
            if (centricUser == null)
            {
                return HttpNotFound();
            }
            return View(centricUser);
        }

        // POST: CentricUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CentricUser centricUser = db.CentricUser.Find(id);
            db.CentricUser.Remove(centricUser);
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
