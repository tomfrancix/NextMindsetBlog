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
    public class ImagemapsController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Imagemaps
        public ActionResult Index()
        {
            var imagemap = db.Imagemap.Include(i => i.Articles).Include(i => i.Images);
            return View(imagemap.ToList());
        }

        // GET: Imagemaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagemap imagemap = db.Imagemap.Find(id);
            if (imagemap == null)
            {
                return HttpNotFound();
            }
            return View(imagemap);
        }

        // GET: Imagemaps/Create
        public ActionResult Create()
        {
            ViewBag.Art_Id = new SelectList(db.Articles, "Art_Id", "Title");
            ViewBag.Img_Id = new SelectList(db.Images, "Img_Id", "Image");
            return View();
        }

        // POST: Imagemaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Imagemap_Id,Img_Id,Art_Id")] Imagemap imagemap)
        {
            if (ModelState.IsValid)
            {
                db.Imagemap.Add(imagemap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Art_Id = new SelectList(db.Articles, "Art_Id", "Title", imagemap.Art_Id);
            ViewBag.Img_Id = new SelectList(db.Images, "Img_Id", "Image", imagemap.Img_Id);
            return View(imagemap);
        }

        // GET: Imagemaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagemap imagemap = db.Imagemap.Find(id);
            if (imagemap == null)
            {
                return HttpNotFound();
            }
            ViewBag.Art_Id = new SelectList(db.Articles, "Art_Id", "Title", imagemap.Art_Id);
            ViewBag.Img_Id = new SelectList(db.Images, "Img_Id", "Image", imagemap.Img_Id);
            return View(imagemap);
        }

        // POST: Imagemaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Imagemap_Id,Img_Id,Art_Id")] Imagemap imagemap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imagemap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Art_Id = new SelectList(db.Articles, "Art_Id", "Title", imagemap.Art_Id);
            ViewBag.Img_Id = new SelectList(db.Images, "Img_Id", "Image", imagemap.Img_Id);
            return View(imagemap);
        }

        // GET: Imagemaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagemap imagemap = db.Imagemap.Find(id);
            if (imagemap == null)
            {
                return HttpNotFound();
            }
            return View(imagemap);
        }

        // POST: Imagemaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Imagemap imagemap = db.Imagemap.Find(id);
            db.Imagemap.Remove(imagemap);
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
