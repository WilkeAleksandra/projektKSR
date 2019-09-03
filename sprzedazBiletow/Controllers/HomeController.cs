using sprzedazBiletow.Models;
using sprzedazBiletow.Models.DataProvider;
using sprzedazBiletow.Models.Requests;
using sprzedazBiletow.Models.Responses;
using System.Collections.Generic;
using System.Web.Mvc;

namespace sprzedazBiletow.Controllers
{
    public class HomeController : Controller
    {
        public Cities Cities = new Cities();
        public Tickets Tickets = new Tickets();

        [HttpGet]
        public ActionResult Konto()
        {
            if (Session["userID"] != null)
            {
                string userId = Session["userID"].ToString();
                if (userId != null)
                {
                    Rpc rpc = new Rpc();
                    TicketResponseView ticketResponseView = new TicketResponseView();
                    ticketResponseView.list = rpc.SendTicketRequest(userId);

                    return View(ticketResponseView);
                }
            }
            return View();
        }

        public ActionResult ZnalezionePolaczenia(SerachResponseView searchModel)
        {
            return View(searchModel);
        }

        [HttpPost]
        public ActionResult ZnalezionePolaczenia(FormCollection form)
        {
            BuyResponse response = new BuyResponse();
            string train = form["checkTrain"].ToString();
            int amount = int.Parse(form["ILOSC"]);
            string ticket_type = form["ticket"].ToString();
            Rpc rpc = new Rpc();

            BuyRequest buyRequest = new BuyRequest()
            {
                TravelId = train,
                UserId = Session["userID"].ToString(),
                FromStation = Session["from"].ToString(),
                ToStation = Session["to"].ToString(),
                Amount = amount.ToString(),
                TicketType = ticket_type
            };

            bool buyResponse = rpc.SendBuyRequest(buyRequest);

            return RedirectToAction("konto");
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

            List<SelectListItem> listSelectListItem = new List<SelectListItem>();

            foreach (Ticket ticket in Tickets.list)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = ticket.Name,
                    Value = ticket.Id.ToString(),
                    Selected = ticket.isSelected
                };
                listSelectListItem.Add(selectListItem);
            }

            searchList.tickets = listSelectListItem;

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