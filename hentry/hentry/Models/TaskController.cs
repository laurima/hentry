using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hentry.Models;

namespace hentry.Models
{
    public class TaskController : Controller
    {
        private acsm_ff4a6a83158a8e0Entities db = new acsm_ff4a6a83158a8e0Entities();

        // GET: Task
        public ActionResult Index(int? project)
        {
            if (project == null)
            {
                var tasks = db.task.Include(t => t.project1).Include(t => t.status1).Include(t => t.aspnetusers);
                return View(tasks.ToList());
            }
            else
            {
                var tasks = from t in db.task where t.project == project select t;
                return View(tasks.ToList());
            }

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
            ViewBag.creator = new SelectList(db.aspnetusers, "id", "email");
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,creator,project,status,estimate_work,estimate_cost,actual_work,actual_cost,created,modified")] task task)
        {

            task.created = DateTime.Now;
            task.modified = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.task.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index", new { project = task.project });
            }

            ViewBag.project = new SelectList(db.project, "id", "identifier", task.project);
            ViewBag.status = new SelectList(db.status, "id", "status1", task.status);
            ViewBag.creator = new SelectList(db.aspnetusers, "id", "email", task.creator);
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
            ViewBag.creator = new SelectList(db.aspnetusers, "id", "email", task.creator);
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,creator,project,status,estimate_work,estimate_cost,actual_work,actual_cost,modified")] task task)
        {

            task.modified = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.Entry(task).Property("created").IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index", new { project = task.project });
            }
            ViewBag.project = new SelectList(db.project, "id", "identifier", task.project);
            ViewBag.status = new SelectList(db.status, "id", "status1", task.status);
            ViewBag.creator = new SelectList(db.aspnetusers, "id", "email", task.creator);
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
            return RedirectToAction("Index", new { project = task.project });
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
