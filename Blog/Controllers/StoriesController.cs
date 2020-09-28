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

namespace Stories.Controllers
{
    public class StoriesController : Controller
    {
        private BlogContext db = new BlogContext();
        // GET: Topics
        public ActionResult Article(int? id)
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




            ViewBag.Commentsdata = comments.ToList().AsEnumerable().Reverse();
            ViewBag.data = id;
            return View(articles);
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

            var theId = comments.Art_Id;
            var addComment = new Comments();
            if (ModelState.IsValid)
            {
                db.Comments.Add(comments);
                db.SaveChanges();
                return RedirectToAction("Article", new { id = theId });
            }
            else
            {
                Console.WriteLine("failure");
                return RedirectToAction("Article", new { id = theId });
            }

        }
        public ActionResult _ViewComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }
        
    }
}