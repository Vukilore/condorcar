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
            if (Request.Cookies["lastVisit"] != null) // Si la personne s'est déjà connecté on auto-login
            {
                Session["Pseudo"] = Request.Cookies["lastVisit"].Values["Pseudo"]; // On rajoute le pseudo du cookie dans la session
                if (Request.Cookies["lastVisit"].Values["Type"] == "Driver")
                    return Redirect("/Driver/Index");
                else
                    ViewBag.Message2 = Request.Cookies["lastVisit"].Values["Type"];
                    return Redirect("/Passenger/Index");
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
                 if(user.IsCorrectPassword()) // Le mot de passe correspond bien à celui de la BDD
                 {
                    // 1. Création d'un cookie avec expiration
                    HttpCookie c = new HttpCookie("lastVisit");
                    c.Values["Pseudo"] = user.Pseudo;
                    c.Expires = DateTime.Now.AddDays(10);
                    Session["Pseudo"] = user.Pseudo;

                    // 2. Chargement de l'utilisateur et redirection vers les controleurs correspondant
                    var userLoaded = user.LoadUser(); // On charge toute la ligne de la BDD dans un objet
                    if (userLoaded is CDriver)
                    {
                        Session["Driver"] = userLoaded;
                        user = (CDriver)userLoaded;
                        Session["Vehicles"] = user.Vehicles;
                        c.Values["Type"] = "Driver";
                        Response.Cookies.Add(c);
                        return Redirect("../Driver/Index");
                    }
                    else if(userLoaded is CPassenger)
                    {
                        c.Values["Type"] = "Passenger";
                        Response.Cookies.Add(c);
                        return Redirect("../Passenger/Index");
                    }
                    else { ViewBag.Message2 = "Tu es AUCUN DES DEUX!!!"; }
                    return View("Logged");
                 }
                 else
                 {
                     ViewBag.Message = "Le mot de passe que vous avez entré est incorrecte.";
                     return View("Login");
                 }
             }
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