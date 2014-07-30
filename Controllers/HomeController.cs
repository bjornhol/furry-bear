using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;

namespace FurryBear.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        private IDocumentStore _documentStore;

        public ActionResult Index()
        {
            ViewBag.Message = "";

            ViewBag.Online = _documentStore.IsServerOnline() ? "Yes" : "No...";

            return View();
        }
    }
}
