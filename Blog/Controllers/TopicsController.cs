using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.App_Start;
using Blog.Models;
using System.Data.Entity;
using System.Net;
using System.IO;

namespace Blog.Controllers
{
    public class TopicsController : Controller
    {
        private BlogContext db = new BlogContext();
        // GET: Topics
        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";


            return View(db.Categories.ToList());
        }
        public ActionResult Category(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categories = db.Categories.Find(id);
            ViewBag.idc = categories;
            var categoriesall = db.Categories.ToList();
            ViewBag.Categories = categoriesall;
            if (categories == null)
            {
                return HttpNotFound();
            }

            ViewBag.data = id;

            var articles = db.Articles.Include(a => a.Categories).Include(a => a.Subcategories);

            return View(articles.Where(r => r.Cat_Id == id).ToList());

        }
        public ActionResult Subcategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Subcategories = db.Subcategories.Find(id);
            ViewBag.idc = Subcategories;
            var categoriesall = db.Categories.ToList();
            ViewBag.Categories = categoriesall;
            if (Subcategories == null)
            {
                return HttpNotFound();
            }

            ViewBag.data = id;

            var articles = db.Articles.Include(a => a.Categories).Include(a => a.Subcategories);

            return View(articles.Where(r => r.Sub_Id == id).ToList());

        }
        public ActionResult Related(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Tags = db.Tagmap.Find(id);
            if (Tags == null)
            {
                 
                ViewBag.data = Tags.Tags.Name;
            
                return View();

            }
            var articles = db.Articles.Include(a => a.Categories).Include(a => a.Subcategories);
            var categories = db.Categories;
            var Articlesdat = db.Articles.ToList();
            ViewBag.Articlesdata = Articlesdat;
            ViewBag.Categories = categories;
            ViewBag.TagList = db.Tags.ToList();
            ViewBag.Tags = Tags;
            ViewBag.Tago = id;
            return View(articles.ToList());
        }
    }
}