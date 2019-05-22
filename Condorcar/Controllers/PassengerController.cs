﻿using Condorcar.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Condorcar.Controllers
{
    public class PassengerController : Controller
    {
        /////////////////////////////////////////////////////////////////////////////////
        ///                               INDEX                                       ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Index()
        {
            if (!(Session["User"] is CPassenger)) return Redirect("../Home/Index");
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Connect                                     ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Connect() // Redirection une fois connecté
        {
            if (!(Session["User"] is CPassenger)) return Redirect("../Home/Index");
            HttpCookie c = new HttpCookie("lastVisit");
            var user = (CPassenger)Session["User"];
            c.Values["Pseudo"] = user.Pseudo;
            c.Expires = DateTime.Now.AddDays(10);
            c.Values["Type"] = "Passenger";
            Response.Cookies.Add(c);
            return View("Index");
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               REGISTER                                    ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Register() // Quand on clique sur le bouton s'inscrire comme passager
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(CPassenger passenger) // Réponse du formulaire de connexion
        {
            ViewBag.Message = ""; // On nettoie le message d'erreur
            if (Session["User"] != null) // Si la session n'est pas vide mais qu'il arrive quand même sur ce controller
            {
                ViewBag.Message = "Vous êtes déjà connecté !";
                return Redirect("/Passenger/Index");
            }
            if (ModelState.IsValid) // Les champs sont remplis correctement
            {
                if (!passenger.IsRegistered()) // Si il n'a pas trouvé le pseudo dans la BDD, on en crée un
                {
                    passenger.Register();   // On enregistre le passager
                    Session["User"] = CUser.LoadUser(passenger.Pseudo);  // On ajoute l'objet récupérer de la BDD de l'utilisateur dans la session
                    return Redirect("../Passenger/Connect");
                }
                else
                {
                    ViewBag.Message = "Ce pseudo existe déjà dans notre base de donnée !";
                    return View("Register");
                }
            }
            else { return View("Register"); }
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Edit                                        ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Edit()
        {
            if (Session["User"] is CPassenger)
                return View();
            else return Redirect("Index");
        }

        public ActionResult Edit(int id)
        {
            if (Session["User"] is CPassenger)
                return View();
            else return Redirect("Index");
        }
    }
}