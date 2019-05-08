using Condorcar.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Condorcar.Controllers
{
    public class PassengerController : Controller
    {
        // GET: Passenger
        public ActionResult Index()
        {
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               REGISTRATION                                ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Register() // Réponse du formulaire de connexion
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(CPassenger passenger) // Réponse du formulaire de connexion
        {
            ViewBag.Message = ""; // On nettoie le message d'erreur
            if (Session["Pseudo"] != null) // Si la session n'est pas vide mais qu'il arrive quand même sur ce controller
            {
                ViewBag.Message = "Vous êtes déjà connecté !";
                return Redirect("/Driver/Index");
            }
            if (ModelState.IsValid) // Les champs sont remplis correctement
            {
                if (passenger.IsRegistered()) // Si il n'a pas trouvé le pseudo dans la BDD, on en crée un
                {
                    HttpCookie c = new HttpCookie("lastVisit");
                    Session["Pseudo"] = c.Value = passenger.Pseudo;
                    c.Expires = DateTime.Now.AddDays(10);
                    Response.Cookies.Add(c);
                    passenger.Smoker = true;
                    passenger.Register();
                    return View("../Home/Logged");
                }
                else
                {
                    ViewBag.Message = "Ce pseudo existe déjà dans notre base de donnée !";
                    return View("Register");
                }
            }
            else { return View("Register"); }
        }
    }
}