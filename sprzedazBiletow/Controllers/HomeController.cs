using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sprzedazBiletow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        /*
        public string Index(string index)
        {
            return "Witam na stronie!" + index;
        }*/

        public ActionResult Wyszukaj()
        {
            ViewBag.Message = ":)";

            return View();
        }

        public ActionResult Konto()
        {
            ViewBag.Message = ":)";

            return View();
        }
    }
}