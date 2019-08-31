using sprzedazBiletow.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace sprzedazBiletow.Controllers
{
    public class HomeController : Controller
    {
        public Cities Cities = new Cities();

        public ActionResult Konto()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ZnalezionePolaczenia(SerachResponseView searchModel)
        {
            return View(searchModel);
        }

        [HttpPost]
        public ActionResult ZnalezionePolaczenia(FormCollection form)
        {
            string train = form["checkTrain"].ToString();
            Rpc rpc = new Rpc();
            SerachResponseView searchList = new SerachResponseView();
            bool searchResponse = rpc.SendBuyRequest(train, Session["userID"].ToString());

            return View();
        }

        [HttpGet]
        public ActionResult Wyszukaj()
        {
            List<SelectListItem> listSelectListItem = new List<SelectListItem>();
            
            foreach (City city in Cities.list)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = city.Name,
                    Value = city.Id.ToString(),
                    Selected = city.isSelected
                };
                listSelectListItem.Add(selectListItem);
            }

            SearchRequest searchRequest = new SearchRequest();
            searchRequest.cities = listSelectListItem;

            return View(searchRequest);
        }

        [HttpPost]
        public ActionResult Wyszukaj(SearchRequest searchModel)
        {
            Rpc rpc = new Rpc();
            SerachResponseView searchList = new SerachResponseView();
            List<SearchResponse> searchResponse = rpc.SendSearchRequest(searchModel);
            searchList.list = searchResponse;

            return View("ZnalezionePolaczenia", searchList);
        }

        [HttpGet]
        public ActionResult Zakladanie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Zakladanie(RegisterRequest userModel)
        {
            Rpc rpc = new Rpc();
            var registerResponse = rpc.SendRegisterRequest(userModel);
            if (registerResponse.Status)
                userModel.AccountErrorMessage = "Konto zostało pomyślnie utworzone.";
            else
                userModel.AccountErrorMessage = "Konto nie zostało utworzone.";
            return View("Zakladanie", userModel);
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
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}