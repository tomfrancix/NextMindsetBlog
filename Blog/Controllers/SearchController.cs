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
    public class SearchController : Controller
    {
        private BlogContext db = new BlogContext();
        // GET: Search
        public ActionResult Index(string input)
        {

            List<int> SearchResults = new List<int>();
            ViewBag.Input = input;
            var tags = db.Tags.ToList();
            foreach (var tag in tags)
            {
                string taga = Convert.ToString(tag).ToLower();
                if (taga == input.ToLower())
                {
                    foreach (var tagmapper in db.Tagmap.ToList())
                    {
                        if (tagmapper.Tag_Id == tag.Tag_Id)
                        {
                            foreach (var article in db.Articles.ToList())
                            {
                                if (article.Art_Id == tagmapper.Art_Id)
                                {
                                    SearchResults.Add(article.Art_Id);
                                }
                            }
                        }
                    }
                }
            }
            ViewBag.Search = SearchResults.ToList();
            var articles = db.Articles.Include(a => a.Categories).Include(a => a.Subcategories);
            var categories = db.Categories;
            ViewBag.Tags = db.Tags.ToList();
            ViewBag.tagmap = db.Tagmap.ToList();
            ViewBag.Categories = categories;
            return View(articles.ToList());
        }
    }
}