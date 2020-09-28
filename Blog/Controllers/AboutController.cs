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
    public class AboutController : Controller
    {
        private BlogContext db = new BlogContext();
        // GET: About
        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";
            var Subcategories = db.Subcategories;
            ViewBag.subs = Subcategories;
            var Articles = db.Articles;
            ViewBag.Art = Articles;
            return View(db.Categories.ToList());
        }
    }
}