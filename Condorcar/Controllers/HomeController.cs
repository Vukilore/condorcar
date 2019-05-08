using Condorcar.Models.DAL;
using Condorcar.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Condorcar.Controllers
{
    public class HomeController : Controller
    {
        /////////////////////////////////////////////////////////////////////////////////
        ///                               INDEX                                       ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Index() // Arrivée sur la page d'index
        {
            ViewBag.Message = ""; // On reset le message d'erreur de la connexion
            if (Request.Cookies["lastVisit"] != null) // Si la personne s'est déjà connecté (Cookie != null)
            {
                Session["Pseudo"] = Request.Cookies["lastVisit"].Value; // On rajoute le pseudo du cookie dans la session
                return View("Logged");   // On redirige vers la page déjà connecté
            }
            else return View("Login"); // Sinon on lui propose de se connecter/Inscrire
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               LOGIN                                       ///
        /////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        public ActionResult Login(CDriver user) // Réponse du formulaire de connexion
        {
             if(!user.IsRegistered()) // Si il n'a pas trouvé le pseudo dans la BDD
             { 
                 ViewBag.Message = "Ce pseudo n'existe pas dans notre base de donnée !";
                 return View("Login"); // Redirection erreur
             }
             else
             {
                 if(user.IsCorrectPassword())
                 {            
                     Session["Pseudo"] = user.Pseudo;
                     HttpCookie c = new HttpCookie("lastVisit");
                     c.Value = user.Pseudo;
                     c.Expires = DateTime.Now.AddDays(10);
                     Response.Cookies.Add(c);
                     return View("Logged");
                 }
                 else
                 {
                     ViewBag.Message = "Le mot de passe que vous avez entré est incorrecte.";
                     return View("Login");
                 }
             }
            return View("Logged");
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               REGISTRATION                                ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Register() // Lorsqu'on appuie sur le bouton s'enregistrer
        {
            ViewBag.Message = ""; // On nettoie le message d'erreur
            return View("Register");
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               LOGGED                                      ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Logged() // Une fois connecté
        {
            if (Session["Pseudo"] != null) return View(); // Si il est bien connecté on continue sur la page
            return Redirect("/Home/Index"); // Sinon on retourne au point de départ (connexion/inscription)
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               DISCONNECT                                  ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Disconnect() // Le bouton de deconnexion
        {
            Session.Clear(); // On nettoie toutes les variables de session
            HttpCookie c = new HttpCookie("lastVisit"); // On crée le cookie avec le nom lastVisit
            c.Expires = DateTime.Now.AddDays(-1);   // On défini quand le cookie expire (-1 pour être instantané)
            Response.Cookies.Add(c); // On injecte le cookie dans la bouche de l'utilisateur :-)
            return Redirect("/Home/Index"); // On redirige au point de départ (connexion/inscription)
        }
    }
}