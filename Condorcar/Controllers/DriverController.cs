using Condorcar.Models.DAL;
using Condorcar.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Condorcar.Controllers
{
    public class DriverController : Controller
    {
        /////////////////////////////////////////////////////////////////////////////////
        ///                               INDEX                                       ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Index()
        {
            return View("Index");
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Connect                                     ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Connect() // Redirection une fois connecté
        {
            HttpCookie c = new HttpCookie("lastVisit");
            c.Values["Pseudo"] = (string)Session["Pseudo"];
            c.Expires = DateTime.Now.AddDays(10);
            c.Values["Type"] = "Driver";
            Response.Cookies.Add(c);
            return View("Index");
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               REGISTER                                    ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Register() // Quand on clique sur le bouton s'inscrire comme conducteur
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(CDriver driver) // Réponse du formulaire de connexion
        {
            ViewBag.Message = ""; // On nettoie le message d'erreur
            if (Session["Pseudo"] != null) // Si la session n'est pas vide mais qu'il arrive quand même sur ce controller
            {
                ViewBag.Message = "Vous êtes déjà connecté !";
                return Redirect("/Driver/Index");
            }
            if (ModelState.IsValid) // Les champs sont remplis correctement
            {
                if (!driver.IsRegistered()) // Si il n'a pas trouvé le pseudo dans la BDD, on en crée un
                {
                    Session["Pseudo"] = driver.Pseudo;
                    driver.Register();
                    return Redirect("../Driver/Connect");
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