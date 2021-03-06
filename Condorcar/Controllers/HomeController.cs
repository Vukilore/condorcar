﻿using Condorcar.Models.DAL;
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
                Session["User"] = CUser.LoadUser(Request.Cookies["lastVisit"].Values["Pseudo"]);  // On ajoute l'objet récupérer de la BDD de l'utilisateur dans la session
                if(Session["User"] == null) return View("Login");
                if (Session["User"] is CDriver) // On redirige selon qu'il soit Driver ou Passenger
                    return Redirect("/Driver/Index");
                else
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
                    // 1. Chargement de l'utilisateur et redirection vers les controleurs correspondant
                    Session["User"] = CUser.LoadUser(user.Pseudo);  // On ajoute l'objet récupérer de la BDD de l'utilisateur dans la session
                    if (Session["User"] is CDriver) // Si c'est un conducteur
                    {
                        return Redirect("../Driver/Connect");
                    }
                    else if(Session["User"] is CPassenger) // Si c'est un passagé 
                    {
                        return Redirect("../Passenger/Connect");
                    }
                    else { ViewBag.Message2 = "Tu es AUCUN DES DEUX!!!"; } //todo <= gérer si aucun des deux
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