using Condorcar.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Condorcar.Controllers
{
    public class VehicleController : Controller
    {
        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               ManageVehicle                               ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Manage() // Quand on clique sur le bouton gérer ses véhicules
        {
            ViewBag.Message = "";
            CDriver user = (CDriver)Session["User"];
            ViewBag.Vehicles = user.Vehicles; // On stock les véhicules du conducteur dans un Viewbag
            return View("Manage"); // On affiche la vue Manage pour gérer la liste des véhicules
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Delete                                      ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                 Supprime un véhicule dans la liste de l'utilisateur      ////
        [HttpPost]
        public ActionResult Delete(int id)
        {
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Add                                         ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Add() // Quand on appuie sur le bouton ajouter véhicule
        {
            return View("Add"); // Formulaire d'ajout de véhicule
        }

        [HttpPost]
        public ActionResult Add(CVehicle vehicle) // Quand on valide le formulaire pour ajouter le véhicule
        {
            if(ModelState.IsValid) // Si les champs sont valides
            {
                CDriver user = (CDriver)Session["User"];    // On charge les variables du conducteur dans user
                if (user.AddVehicle(vehicle) == true) // Si il a bien pu ajouter le véhicule
                {
                    ViewBag.Message = "Vous avez enregistré un nouveau véhicule ! (Modèle : " + vehicle.Model + " places : " + vehicle.Seat + " Autorisé à fumer : " + vehicle.CanSmoke + ")";
                    ViewBag.Vehicles = user.Vehicles; // Pour l'afficher directe dans la liste
                    Session["User"] = user;
                    return View("Manage"); // Affichage de la liste
                }
                else // Sinon il existe déjà
                {
                    ViewBag.Vehicles = user.Vehicles; // Pour l'afficher directe dans la liste
                    ViewBag.Message = "Vous avez déjà ajouté ce véhicule parmis vos véhicules !";
                    return View("Manage"); // Affichage de la liste
                }
            }
            return View("Add"); // les champs sont pas valides
        }
    }
}