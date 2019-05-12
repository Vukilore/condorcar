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
        /////////////////////////////////////////////////////////////////////////////////
        ///                               INDEX                                       ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Index()
        {
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Connect                                     ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Connect() // Redirection une fois connecté
        {
            HttpCookie c = new HttpCookie("lastVisit");
            c.Values["Pseudo"] = (string)Session["Pseudo"];
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
            if (Session["Pseudo"] != null) // Si la session n'est pas vide mais qu'il arrive quand même sur ce controller
            {
                ViewBag.Message = "Vous êtes déjà connecté !";
                return Redirect("/Passenger/Index");
            }
            if (ModelState.IsValid) // Les champs sont remplis correctement
            {
                if (!passenger.IsRegistered()) // Si il n'a pas trouvé le pseudo dans la BDD, on en crée un
                {
                    Session["Pseudo"] = passenger.Pseudo;
                    passenger.Register();
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
    }
}