﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hentry.Models;
using System.Web.Security;

namespace hentry.Controllers
{
    [Authorize(Roles = "Admin, Projectmanager, Projectworker")]
    public class ProjectController : Controller
    {
        private acsm_ff4a6a83158a8e0Entities db = new acsm_ff4a6a83158a8e0Entities();

        // GET: Project
        public ActionResult Index()
        {
            ViewBag.EditRights = false;
            if (User.Identity.IsAuthenticated)
            {
                var roles = Roles.GetRolesForUser(User.Identity.Name);
                if (roles.Contains<string>("Admin") || roles.Contains<string>("Projectmanager"))
                {
                    ViewBag.EditRights = true;
                    return View(db.project.ToList());
                }
                else
                {
                    return View(db.project.ToList());
                }
            }
            return View(db.project.ToList());
        }

        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,identifier,name,info,budget,start_date,end_date,created,modified")] project project)
        {

            project.created = DateTime.Now;
            project.modified = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.project.Add(project);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,identifier,name,info,budget,start_date,end_date,modified")] project project)
        {

            project.modified = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.Entry(project).Property("created").IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            project project = db.project.Find(id);
            db.project.Remove(project);
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
