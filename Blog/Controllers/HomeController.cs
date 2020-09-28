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
    public class HomeController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult Index()
        {
            var articles = db.Articles.Include(a => a.Categories).Include(a => a.Subcategories);
            var categories = db.Categories;
            ViewBag.Tags = db.Tags.ToList();
            ViewBag.tagmap = db.Tagmap.ToList();
            ViewBag.Categories = categories;
            ViewBag.Tags = db.Tags.ToList();
            return View(articles.ToList());
        }
        public ActionResult Resources()
        {
            
            return View();
        }
        public ActionResult Search(string input)
        {

            List<int> SearchResults = new List<int>();
            ViewBag.Input = input;
            var tags = db.Tags.ToList();
            foreach (var tag in tags)
            {
                string taga = Convert.ToString(tag).ToLower();
                if(taga == input.ToLower())
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

        
        public ActionResult Related(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Tags = db.Tagmap.Find(id);
            if (Tags == null)
            {
                return HttpNotFound();
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
            var comments = db.Comments;
            var categoriesall = db.Categories;
            ViewBag.Categories = categoriesall;
            var articledat = db.Tagmap;
            var articlelist = db.Articles.Include(a => a.Categories).Include(a => a.Subcategories);
            ViewBag.Articles = articlelist.ToList();
            ViewBag.Tagdata = articledat.ToList();
            
                StringWriter myWriter = new StringWriter();
                HttpUtility.HtmlDecode(articles.Content, myWriter);
                string myDecodedString = myWriter.ToString();
               ViewBag.ContentData = myDecodedString;
            var Replieslist = db.Replies.Include(a => a.Comments);
            ViewBag.Replies = Replieslist.ToList();
            



            ViewBag.Commentsdata = comments.ToList();
            ViewBag.data = id;
            return View(articles);
        }
       

        public ActionResult Category_List(int? id)
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
        public ActionResult Subcategory_List(int? id)
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
        // GET: Comments/Create
        public ActionResult AddComment(int? id)
        {
            //ViewBag.Art_Id = new SelectList(db.Articles, "Art_Id", "Title");
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            ViewBag.data = articles;
            return View(articles);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment([Bind(Include = "Com_Id,Art_Id,First_Name,Last_Name,Email,Date,Content,Replies_Count")] Comments comments)
        {

            var addComment = new Comments();
            if (ModelState.IsValid)
            {
                db.Comments.Add(comments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("failure");
                return RedirectToAction("Index");
            }

        }


    }
}