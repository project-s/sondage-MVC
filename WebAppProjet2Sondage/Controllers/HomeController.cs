using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppProjet2Sondage.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Information = TempData["Information"];
            return View();
        }

        [HttpPost]
        public ActionResult DoSomething()
        {
            return View("Error");
        }

    }

}