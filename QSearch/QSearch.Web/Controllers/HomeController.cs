using QSearch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QSearch.Web.Controllers
{
    public class HomeController : Controller
    {
        private IQuestSearchService searchsrv;

        public HomeController()
        {
        }

        public HomeController(IQuestSearchService searchsrv)
        {
            this.searchsrv = searchsrv;
        }

        public ActionResult Index()
        {
            this.searchsrv.Search(new SearchQuery() { QueryText = ".net core" });
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}