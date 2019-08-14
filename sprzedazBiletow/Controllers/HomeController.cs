﻿using sprzedazBiletow.Models;
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
        public ActionResult Index(LoginRequest userModel)
        {
            Rpc rpc = new Rpc();
            var loginResponse = rpc.sendMessage(userModel.Login, userModel.Password);
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
            return View(userModel);
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}