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
    public class RecentController : Controller
    {
        private BlogContext db = new BlogContext();
        // GET: Recent
        public ActionResult Index()
        {
            var articles = db.Articles.Include(a => a.Categories).Include(a => a.Subcategories);
            var categories = db.Categories;
            var Articlesdat = db.Articles.ToList();
            ViewBag.Articlesdata = Articlesdat;
            ViewBag.Categories = categories;
            ViewBag.Tags = db.Tags.ToList();
            return View(articles.ToList());
        }
    }
}