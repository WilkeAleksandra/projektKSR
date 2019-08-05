using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
            return View();
        }

        public ActionResult Konto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Models.User userModel)
        {
            Models.Rpc rpc = new Models.Rpc();
            var result = rpc.sendMessage(userModel.Login, userModel.Password);
            return Konto();
        }
    }
}