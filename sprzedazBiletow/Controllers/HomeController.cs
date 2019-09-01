using sprzedazBiletow.Models;
using sprzedazBiletow.Models.Responses;
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

        [HttpPost]
        public ActionResult Konto(BuyResponse response)
        {
            return View(response);
        }

        [HttpGet]
        public ActionResult ZnalezionePolaczenia(SerachResponseView searchModel)
        {
            return View(searchModel);
        }

        [HttpPost]
        public ActionResult ZnalezionePolaczenia(FormCollection form)
        {
            BuyResponse response = new BuyResponse();
            string train = form["checkTrain"].ToString();
            Rpc rpc = new Rpc();
            bool buyResponse = rpc.SendBuyRequest(train, Session["userID"].ToString(), Session["from"].ToString(), Session["to"].ToString());

            if (buyResponse)
                response.resultText = "Zakup został dokonany pomyślnie.";
            else
                response.resultText = "Nie udało się zrealizować żądania zakupu";

            return View("Konto", response);
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
            Session["from"] = searchModel.startStation;
            Session["to"] = searchModel.endStation;
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