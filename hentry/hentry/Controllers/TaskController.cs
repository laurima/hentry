using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hentry.Models;

namespace hentry.Controllers
{
    public class TaskController : Controller
    {
        private main_database db = new main_database();

        // GET: Task
        public ActionResult Index()
        {
            var task = db.task.Include(t => t.project1).Include(t => t.status1).Include(t => t.user);
            return View(task.ToList());
        }

        // GET: Task/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task task = db.task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            ViewBag.project = new SelectList(db.project, "id", "identifier");
            ViewBag.status = new SelectList(db.status, "id", "status1");
            ViewBag.creator = new SelectList(db.user, "id", "email");
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,creator,project,status,estimate_work,estimate_cost,actual_work,actual_cost,created,modified")] task task)
        {
            if (ModelState.IsValid)
            {
                db.task.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.project = new SelectList(db.project, "id", "identifier", task.project);
            ViewBag.status = new SelectList(db.status, "id", "status1", task.status);
            ViewBag.creator = new SelectList(db.user, "id", "email", task.creator);
            return View(task);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task task = db.task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.project = new SelectList(db.project, "id", "identifier", task.project);
            ViewBag.status = new SelectList(db.status, "id", "status1", task.status);
            ViewBag.creator = new SelectList(db.user, "id", "email", task.creator);
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,creator,project,status,estimate_work,estimate_cost,actual_work,actual_cost,created,modified")] task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.project = new SelectList(db.project, "id", "identifier", task.project);
            ViewBag.status = new SelectList(db.status, "id", "status1", task.status);
            ViewBag.creator = new SelectList(db.user, "id", "email", task.creator);
            return View(task);
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task task = db.task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            task task = db.task.Find(id);
            db.task.Remove(task);
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
