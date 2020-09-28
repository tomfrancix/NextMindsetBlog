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
    public class PolicyController : Controller
    {
        private BlogContext db = new BlogContext();
        // GET: Policy
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Disclaimer()
        {
            ViewBag.Message = "Your Disclaimer page.";

            return View();
        }
    }
}