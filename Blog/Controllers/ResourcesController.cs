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
    public class ResourcesController : Controller
    {
        private BlogContext db = new BlogContext();
        // GET: Resources
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Tools()
        {
            return View();
        }
        public ActionResult Recipes()
        {
            return View();
        }
        public ActionResult Covid()
        {
            return View();
        }
    }
}