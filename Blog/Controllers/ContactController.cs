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
    public class ContactController : Controller
    {
        private BlogContext db = new BlogContext();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
    }
}