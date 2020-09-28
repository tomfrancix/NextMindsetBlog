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
    public class ImagesController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Images
        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Img_Id,Image,imageFile,Alt")] HttpPostedFileBase imageFile, Images images)
        {
            if (images?.Image == null)
            {
                //images.Image = "default.png";
                
            }
            if (ModelState.IsValid)
            {
                string imageName = Path.GetFileNameWithoutExtension(images.imageFile.FileName);
                string extension = Path.GetExtension(images.imageFile.FileName);
                imageName += extension;
                images.Image = imageName;
                imageName = Path.Combine(Server.MapPath("~/Content/images/"), imageName);
                images.imageFile.SaveAs(imageName);
                db.Images.Add(images);
                db.SaveChanges();
                return RedirectToAction("Index");
            } 

            return View(images);
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Img_Id,Image,imageFile,Alt")]HttpPostedFileBase imageFile, Images images)
        {
            if (images.Image == null)
            {
                images.Image = "default.png";
            }
            if (ModelState.IsValid)
            {
                string imageName = Path.GetFileNameWithoutExtension(images.imageFile.FileName);
                string extension = Path.GetExtension(images.imageFile.FileName);
                imageName += extension;
                images.Image = imageName;
                imageName = Path.Combine(Server.MapPath("~/Content/images/"), imageName);
                images.imageFile.SaveAs(imageName);
                db.Entry(images).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(images);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Images images = db.Images.Find(id);
            db.Images.Remove(images);
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
