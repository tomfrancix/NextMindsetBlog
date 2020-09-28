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
    public class ShopsController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Shops
        public ActionResult Index()
        {
            return View(db.Shop.ToList());
        }

        // GET: Shops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = db.Shop.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // GET: Shops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Shop_Id,Title,Link,Description,Information,Price,Stock,Image1,imageFile, Image2, imageFile2")]HttpPostedFileBase imageFile, HttpPostedFileBase imageFile2, Shop shop)
        {
            if (shop.Image1 == null)
            {
                shop.Image1 = "default.png";
            }
            if (shop.Image2 == null)
            {
                shop.Image2 = "default.png";
            }
            if (ModelState.IsValid)
            {
                string imageName1 = Path.GetFileNameWithoutExtension(shop.imageFile.FileName);
                string extension1 = Path.GetExtension(shop.imageFile.FileName);
                imageName1 += extension1;
                shop.Image1 = imageName1;
                imageName1 = Path.Combine(Server.MapPath("~/Content/images/"), imageName1);
                shop.imageFile.SaveAs(imageName1);

                string imageName2 = Path.GetFileNameWithoutExtension(shop.imageFile2.FileName);
                string extension2 = Path.GetExtension(shop.imageFile2.FileName);
                imageName2 += extension2;
                shop.Image2 = imageName2;
                imageName2 = Path.Combine(Server.MapPath("~/Content/images/"), imageName2);
                shop.imageFile2.SaveAs(imageName2);
                db.Shop.Add(shop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shop);
        }

        // GET: Shops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = db.Shop.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // POST: Shops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Shop_Id,Title,Link,Description,Information,Price,Stock,Image1,imageFile, Image2, imageFile2")]HttpPostedFileBase imageFile, HttpPostedFileBase imageFile2, Shop shop)
        {
            if (shop.Image1 == null)
            {
                shop.Image1 = "default.png";
            }
            if (shop.Image2 == null)
            {
                shop.Image2 = "default.png";
            }
            if (ModelState.IsValid)
            {
                string imageName1 = Path.GetFileNameWithoutExtension(shop.imageFile.FileName);
                string extension1 = Path.GetExtension(shop.imageFile.FileName);
                imageName1 += extension1;
                shop.Image1 = imageName1;
                imageName1 = Path.Combine(Server.MapPath("~/Content/images/"), imageName1);
                shop.imageFile.SaveAs(imageName1);

                string imageName2 = Path.GetFileNameWithoutExtension(shop.imageFile2.FileName);
                string extension2 = Path.GetExtension(shop.imageFile2.FileName);
                imageName2 += extension2;
                shop.Image2 = imageName2;
                imageName2 = Path.Combine(Server.MapPath("~/Content/images/"), imageName2);
                shop.imageFile2.SaveAs(imageName2);
                db.Entry(shop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shop);
        }

        // GET: Shops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = db.Shop.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shop shop = db.Shop.Find(id);
            db.Shop.Remove(shop);
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
