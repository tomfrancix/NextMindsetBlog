using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.App_Start;
using Blog.Models;

namespace Blog.Controllers
{
    [Authorize]
    public class TagmapsController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Tagmaps
        public ActionResult Index()
        {
            var tagmap = db.Tagmap.Include(t => t.Articles).Include(t => t.Tags);
            return View(tagmap.ToList());
        }

        // GET: Tagmaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tagmap tagmap = db.Tagmap.Find(id);
            if (tagmap == null)
            {
                return HttpNotFound();
            }
            return View(tagmap);
        }

        // GET: Tagmaps/Create
        public ActionResult Create()
        {
            ViewBag.Art_Id = new SelectList(db.Articles, "Art_Id", "Title");
            ViewBag.Tag_Id = new SelectList(db.Tags, "Tag_Id", "Name");
            return View();
        }

        // POST: Tagmaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tagmap_Id,Tag_Id,Art_Id")] Tagmap tagmap)
        {
            if (ModelState.IsValid)
            {
                db.Tagmap.Add(tagmap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Art_Id = new SelectList(db.Articles, "Art_Id", "Title", tagmap.Art_Id);
            ViewBag.Tag_Id = new SelectList(db.Tags, "Tag_Id", "Name", tagmap.Tag_Id);
            return View(tagmap);
        }

        // GET: Tagmaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tagmap tagmap = db.Tagmap.Find(id);
            if (tagmap == null)
            {
                return HttpNotFound();
            }
            ViewBag.Art_Id = new SelectList(db.Articles, "Art_Id", "Title", tagmap.Art_Id);
            ViewBag.Tag_Id = new SelectList(db.Tags, "Tag_Id", "Name", tagmap.Tag_Id);
            return View(tagmap);
        }

        // POST: Tagmaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tagmap_Id,Tag_Id,Art_Id")] Tagmap tagmap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tagmap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Art_Id = new SelectList(db.Articles, "Art_Id", "Title", tagmap.Art_Id);
            ViewBag.Tag_Id = new SelectList(db.Tags, "Tag_Id", "Name", tagmap.Tag_Id);
            return View(tagmap);
        }

        // GET: Tagmaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tagmap tagmap = db.Tagmap.Find(id);
            if (tagmap == null)
            {
                return HttpNotFound();
            }
            return View(tagmap);
        }

        // POST: Tagmaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tagmap tagmap = db.Tagmap.Find(id);
            db.Tagmap.Remove(tagmap);
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
