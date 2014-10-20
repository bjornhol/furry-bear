using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurryBear.Models;
using Raven.Abstractions.Data;
using Raven.Abstractions.Extensions;
using Raven.Client;
using Raven.Client.Document;
using ServiceStack.Caching;

namespace FurryBear.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IDocumentStore store, IDocumentSession session)
        {
            _store = store;
            _session = session;
        }

        private IDocumentStore _store;
        private IDocumentSession _session;

        public ICacheClient Cache { get; set; }
        
        public ActionResult Index()
        {
            ViewBag.Message = "";

            ViewBag.Online = _store.IsServerOnline() ? "Yes" : "No...";

            return View();
        }

        public ActionResult Posts()
        {
            List<Post> posts = _session.Query<Post>().OrderByDescending(p => p.Date).ToList();

            if (Cache != null)
            {
                posts.ForEach(p =>
                {
                    p.CachedAt = DateTime.Now;
                    Cache.Set(p.Id, p);
                });
            }

            return View(posts);
        }
        
        public ActionResult AddPost(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Title) && string.IsNullOrWhiteSpace(post.Message))
            {
                return View(new Post());
            }

            post.Date = DateTime.Now;

            _session.Store(post);
            _session.SaveChanges();

            return RedirectToAction("Posts");
        }

        public ActionResult Post(string id)
        {
            Post post = null;

            if (Cache != null)
            {
                post = Cache.Get<Post>(id);
            }

            if (post == null)
            {
                post = _session.Load<Post>(id);
            }

            return View(post);
        }

        public ActionResult FlushPosts()
        {
            var posts = _session.Query<Post>();
            posts.ForEach(p => _session.Delete(p));
            _session.SaveChanges();

            return RedirectToAction("Posts");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
