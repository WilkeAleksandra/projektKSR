using sprzedazBiletow.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace sprzedazBiletow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Konto()
        {
            return View();
        }

        public ActionResult Wyszukaj()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(User userModel)
        {
            Rpc rpc = new Rpc();
            var loginResponse = rpc.sendMessage(userModel.Login, userModel.Password);
            if (loginResponse.Status)
                return RedirectToAction("konto", loginResponse);
            return View(userModel);
        }
    }
}