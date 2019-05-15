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
        ///                 Affiche la liste des trajets du véhicule                  ////
        public ActionResult Index()
        {
            return View("listTrajet");
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
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(CRide ride)
        {
            try
            {
                if (ModelState.IsValid) // Les champs sont remplis correctement
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.Message = "Vous n'avez pas rempli tous les champs";
                    return View("Create");
                }
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ride/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
