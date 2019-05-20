using Condorcar.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Condorcar.Controllers
{
    public class RideController : Controller
    {

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Index                                       ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                 Affiche la liste des trajets du véhicule                 ////
        public ActionResult Index()
        {
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Details                                     ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                      Affiche le détail d'un trajet                       ////
        public ActionResult Details(int id)
        {
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Create                                      ///
        /////////////////////////////////////////////////////////////////////////////////
        ///            Ajoute un nouveau trajet dans la liste des trajets            ////
        public ActionResult Create()
        {
            CDriver user = new CDriver();
            user = (CDriver)Session["User"];    // On charge les variables du conducteur dans user
            if (user.Vehicles == null)
            {
                ViewBag.Message = "Vous n'avez aucun véhicule pour proposer un trajet !";
                return View("Index");
            }
            Session["listVehicle"] = ToSelectList(user.Vehicles); // On stock dans une session ainsi si il y a une erreur dans le formulaire, la liste de se vide pas
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(CRide ride)
        {
            try
            {
                try
                {
                    ride.Vehicle = CVehicle.Get(Int32.Parse(ride.VehicleId));
                }
                catch (FormatException)
                {
                    throw new Exception("Impossible de convertir l'ID du véhicule. Est-il correcte ?");
                }
                CDriver driver = new CDriver();
                driver = (CDriver)Session["User"];    // On charge les variables du conducteur dans user
                ride.Driver = driver; 
                ride.Add();
                ViewBag.Message = "Vous avez bien enregistré un nouveau trajet";
                return View("Index");
            }
            catch
            {
                return View();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Edit                                        ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                 Edit un trajet dans la liste des trajets                 ////
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ride/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Delete                                      ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                 Supprime un trajet dans la liste des trajets             ////
        // POST: Ride/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
                return View();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               ToSelectList                                ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                 Transforme une liste de véhicule en SelectList            ////
        [NonAction]
        public SelectList ToSelectList(List<CVehicle> vehicles)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var veh in vehicles)
            {
                list.Add(new SelectListItem()
                {
                    Text = veh.Model + ", (place(s): " + veh.Seat + ")",
                    Value = veh.Id.ToString()
                });
            }
            return new SelectList(list, "Value", "Text", 1);
        }
    }
}
