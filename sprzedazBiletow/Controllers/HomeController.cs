using sprzedazBiletow.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace sprzedazBiletow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Konto()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Wyszukaj()
        {
            List<SelectListItem> listSelectListItem = new List<SelectListItem>();
            List<City> cities = new List<City>();
            cities.Add(new City(1,"Warszawa"));
            cities.Add(new City(2, "Gdańsk"));
            cities.Add(new City(3, "Wrocław"));
            cities.Add(new City(4, "Kraków"));

            foreach(City city in cities)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = city.Name,
                    Value = city.Id.ToString(),
                    Selected = city.isSelected
                };
                listSelectListItem.Add(selectListItem);
            }

            SearchView searchView = new SearchView();
            searchView.cities = listSelectListItem;

            return View(searchView);
        }

        [HttpPost]
        public ActionResult Wyszukaj(SearchView searchView)
        {
            Rpc rpc = new Rpc();
            var loginResponse = rpc.SendSearchRequest(searchView);

            return View();
        }

        [HttpGet]
        public ActionResult Zakladanie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Zakladanie(RegisterRequest userModel)
        {
            //userModel.Email = "test";
            //userModel.FirstName = "test";
            //userModel.LastName = "test";
            //userModel.Password = "test";
            //userModel.Login = "test";
            Rpc rpc = new Rpc();
            var registerResponse = rpc.SendRegisterRequest(userModel);
            if (registerResponse.Status)
            {
                userModel.AccountErrorMessage = "Konto zostało pomyślnie utworzone.";
                return View("Zakladanie", userModel);
            }
            else
            {
                userModel.AccountErrorMessage = "Konto nie zostało utworzone.";
                return View("Zakladanie", userModel);
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            //dodać obsługę wysyłania widomości do kolejki i odbioru
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginRequest userModel)
        {
            Rpc rpc = new Rpc();
            var loginResponse = rpc.SendLoginRequest(userModel);
            if (loginResponse.Status)
            {
                Session["userID"] = loginResponse.UserID;
                Session["userName"] = loginResponse.FirstName;
                Session["userLastName"] = loginResponse.LastName;
                Session["userEmail"] = loginResponse.Email;
                return RedirectToAction("konto", loginResponse);
            }
            else
            {
                userModel.LoginErrorMessage = "Podałeś zły login lub hasło.";
                return View("Index", userModel);
            }
            //return View(userModel);
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}