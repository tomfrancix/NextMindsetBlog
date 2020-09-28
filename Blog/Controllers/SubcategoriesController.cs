using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.App_Start;
using Blog.Models;

namespace Blog.Controllers
{
    [Authorize]
    public class SubcategoriesController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Subcategories
        public ActionResult Index()
        {
            var subcategories = db.Subcategories.Include(s => s.Categories);
            return View(subcategories.ToList());
        }

        // GET: Subcategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategories subcategories = db.Subcategories.Find(id);
            if (subcategories == null)
            {
                return HttpNotFound();
            }
            return View(subcategories);
        }

        // GET: Subcategories/Create
        public ActionResult Create()
        {
            ViewBag.Cat_Id = new SelectList(db.Categories, "Cat_Id", "Title");
            return View();
        }

        // POST: Subcategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sub_Id,Cat_Id,Title,Image,imageFile,Snippet,Summary,Introduction")]HttpPostedFileBase imageFile, Subcategories subcategories)
        {
            if (subcategories.Image == null)
            {
                subcategories.Image = "default.png";
            }
            if (ModelState.IsValid)
            {
                string imageName1 = Path.GetFileNameWithoutExtension(subcategories.imageFile.FileName);
                string extension1 = Path.GetExtension(subcategories.imageFile.FileName);
                imageName1 += extension1;
                subcategories.Image = imageName1;
                imageName1 = Path.Combine(Server.MapPath("~/Content/images/"), imageName1);
                subcategories.imageFile.SaveAs(imageName1);
                db.Subcategories.Add(subcategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cat_Id = new SelectList(db.Categories, "Cat_Id", "Title", subcategories.Cat_Id);
            return View(subcategories);
        }

        // GET: Subcategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategories subcategories = db.Subcategories.Find(id);
            if (subcategories == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat_Id = new SelectList(db.Categories, "Cat_Id", "Title", subcategories.Cat_Id);
            return View(subcategories);
        }

        // POST: Subcategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sub_Id,Cat_Id,Title,Image,imageFile,Snippet,Summary,Introduction")]HttpPostedFileBase imageFile, Subcategories subcategories)
        {
            if (subcategories.Image == null)
            {
                subcategories.Image = "default.png";
            }
            if (ModelState.IsValid)
            {
                string imageName1 = Path.GetFileNameWithoutExtension(subcategories.imageFile.FileName);
                string extension1 = Path.GetExtension(subcategories.imageFile.FileName);
                imageName1 += extension1;
                subcategories.Image = imageName1;
                imageName1 = Path.Combine(Server.MapPath("~/Content/images/"), imageName1);
                subcategories.imageFile.SaveAs(imageName1);
                db.Entry(subcategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cat_Id = new SelectList(db.Categories, "Cat_Id", "Title", subcategories.Cat_Id);
            return View(subcategories);
        }

        // GET: Subcategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategories subcategories = db.Subcategories.Find(id);
            if (subcategories == null)
            {
                return HttpNotFound();
            }
            return View(subcategories);
        }

        // POST: Subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subcategories subcategories = db.Subcategories.Find(id);
            db.Subcategories.Remove(subcategories);
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
