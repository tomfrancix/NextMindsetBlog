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
    public class ArticlesController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Articles
        public ActionResult Index()
        {
            
            var articles = db.Articles.Include(a => a.Categories).Include(a => a.Subcategories);
            return View(articles.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            StringWriter myWriter = new StringWriter();
            HttpUtility.HtmlDecode(articles.Content, myWriter);
            string myDecodedString = myWriter.ToString();
            ViewBag.ContentData = myDecodedString;
            return View(articles);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.Cat_Id = new SelectList(db.Categories, "Cat_Id", "Title");
            ViewBag.Sub_Id = new SelectList(db.Subcategories, "Sub_Id", "Title");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Art_Id,Cat_Id,Sub_Id,Status,Title,Image,imageFile,Author_Name,Author_Image,imageFile2,Date,Lead,Snippet,Content")] HttpPostedFileBase imageFile, HttpPostedFileBase imageFile2, Articles articles)
        {
            string myString = articles.Content;
            string myEncodedString = HttpUtility.HtmlEncode(myString);
            articles.Content = myEncodedString;
            articles.Date = DateTime.Now;

            if (articles.Image == null)
            {
                articles.Image = "default.png";
            }
            if (articles.Author_Image == null)
            {
                articles.Author_Image = "default.png";
            }
            if (ModelState.IsValid)
            {
                string imageName = Path.GetFileNameWithoutExtension(articles.imageFile.FileName);
                string extension = Path.GetExtension(articles.imageFile.FileName);
                imageName += extension;
                articles.Image = imageName;
                imageName = Path.Combine(Server.MapPath("~/Content/images/"), imageName);
                articles.imageFile.SaveAs(imageName);

                string imageName2 = Path.GetFileNameWithoutExtension(articles.imageFile2.FileName);
                string extension2 = Path.GetExtension(articles.imageFile2.FileName);
                imageName2 += extension2;
                articles.Author_Image = imageName2;
                imageName2 = Path.Combine(Server.MapPath("~/Content/images/"), imageName2);
                articles.imageFile2.SaveAs(imageName2);

                db.Articles.Add(articles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cat_Id = new SelectList(db.Categories, "Cat_Id", "Title", articles.Cat_Id);
            ViewBag.Sub_Id = new SelectList(db.Subcategories, "Sub_Id", "Title", articles.Sub_Id);
            return View(articles);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.Cat_Id = new SelectList(db.Categories, "Cat_Id", "Title", articles.Cat_Id);
            ViewBag.Sub_Id = new SelectList(db.Subcategories, "Sub_Id", "Title", articles.Sub_Id);
            StringWriter myWriter = new StringWriter();
            HttpUtility.HtmlDecode(articles.Content, myWriter);
            string myDecodedString = myWriter.ToString();
            ViewBag.ContentData = myDecodedString;
            return View(articles);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Art_Id,Cat_Id,Sub_Id,Status,Title,Image, imageFile,Author_Name,Author_Image, imageFile2,Date,Lead,Snippet,Content")] HttpPostedFileBase imageFile, HttpPostedFileBase imageFile2, Articles articles)
        {
            string myString = articles.Content;
            string myEncodedString = HttpUtility.HtmlEncode(myString);
            articles.Content = myEncodedString;
            articles.Date = DateTime.Now;
            if (articles.Image == null)
            {
                articles.Image = "default.png";
            }
            if (articles.Author_Image == null)
            {
                articles.Author_Image = "default.png";
            }
            if (ModelState.IsValid)
            {
                string imageName = Path.GetFileNameWithoutExtension(articles.imageFile.FileName);
                string extension = Path.GetExtension(articles.imageFile.FileName);
                imageName += extension;
                articles.Image = imageName;
                imageName = Path.Combine(Server.MapPath("~/Content/images/"), imageName);
                articles.imageFile.SaveAs(imageName);

                string imageName2 = Path.GetFileNameWithoutExtension(articles.imageFile2.FileName);
                string extension2 = Path.GetExtension(articles.imageFile2.FileName);
                imageName2 += extension2;
                articles.Author_Image = imageName2;
                imageName2 = Path.Combine(Server.MapPath("~/Content/images/"), imageName2);
                articles.imageFile2.SaveAs(imageName2);

                db.Entry(articles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cat_Id = new SelectList(db.Categories, "Cat_Id", "Title", articles.Cat_Id);
            ViewBag.Sub_Id = new SelectList(db.Subcategories, "Sub_Id", "Title", articles.Sub_Id);
            return View(articles);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articles articles = db.Articles.Find(id);
            db.Articles.Remove(articles);
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
