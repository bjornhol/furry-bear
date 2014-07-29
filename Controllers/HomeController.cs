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
        public ActionResult Index()
        {
            ViewBag.Message = "";

            var store = Store.Init();

            ViewBag.Online = store.IsServerOnline() ? "Yes" : "No...";

            return View();
        }
    }
}
