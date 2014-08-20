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
        public HomeController(IDocumentStore store, IDocumentSession session, ICacheClient cache)
        {
            _store = store;
            _session = session;
            _cache = cache;
        }

        private IDocumentStore _store;
        private IDocumentSession _session;
        private ICacheClient _cache;

        public ActionResult Index()
        {
            ViewBag.Message = "";

            ViewBag.Online = _store.IsServerOnline() ? "Yes" : "No...";

            return View();
        }

        public ActionResult Posts()
        {
            List<Post> posts = _session.Query<Post>().OrderByDescending(p => p.Date).ToList();

            posts.ForEach(p =>
            {
                p.CachedAt = DateTime.Now;
                _cache.Set(p.Id, p);
            });

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
            var post = _cache.Get<Post>(id) ?? _session.Load<Post>(id);

            return View(post);
        }

        public ActionResult FlushPosts()
        {
            var posts = _session.Query<Post>();
            posts.ForEach(p => _session.Delete(p));
            _session.SaveChanges();

            return RedirectToAction("Posts");
        }
    }
}
