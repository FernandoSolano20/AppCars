using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cars.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Agency()
        {
            ViewBag.Title = "Home Page";

            return View("Agency");
        }
        public ActionResult Brand()
        {
            ViewBag.Title = "Home Page";

            return View("Brand");
        }

        public ActionResult Model()
        {
            ViewBag.Title = "Home Page";

            return View("Model");
        }
    }
}
