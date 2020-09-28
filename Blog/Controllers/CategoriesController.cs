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
    public class CategoriesController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cat_Id,Title,Image,imageFile,,Snippet,Summary,Introduction")] HttpPostedFileBase imageFile, Categories categories)
        {
            if (categories?.Image == null)
            {
                categories.Image = "default.png";

            }
            if (ModelState.IsValid)
            {
                string imageName = Path.GetFileNameWithoutExtension(categories.imageFile.FileName);
                string extension = Path.GetExtension(categories.imageFile.FileName);
                imageName += extension;
                categories.Image = imageName;
                imageName = Path.Combine(Server.MapPath("~/Content/images/"), imageName);
                categories.imageFile.SaveAs(imageName);
                db.Categories.Add(categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categories);
        }



        //    if (ModelState.IsValid)
        //    {
        //        string imageName = Path.GetFileNameWithoutExtension(images.imageFile.FileName);
        //        string extension = Path.GetExtension(images.imageFile.FileName);
        //        imageName += extension;
        //        images.Image = imageName;
        //        imageName = Path.Combine(Server.MapPath("~/Content/images/"), imageName);
        //        images.imageFile.SaveAs(imageName);
        //        db.Images.Add(images);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(images);
        //}
        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cat_Id,Title,Image,imageFile, Snippet,Summary,Introduction")]HttpPostedFileBase imageFile, Categories categories)
        {
            if (categories.Image == null)
            {
                categories.Image = "default.png";
            }
            if (ModelState.IsValid)
            {
                string imageName = Path.GetFileNameWithoutExtension(categories.imageFile.FileName);
                string extension = Path.GetExtension(categories.imageFile.FileName);
                imageName += extension;
                categories.Image = imageName;
                imageName = Path.Combine(Server.MapPath("~/Content/images/"), imageName);
                categories.imageFile.SaveAs(imageName);
                db.Entry(categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = db.Categories.Find(id);
            db.Categories.Remove(categories);
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
