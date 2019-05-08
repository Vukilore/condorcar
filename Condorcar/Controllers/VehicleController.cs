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
        public ActionResult Manage()
        {
            ViewBag.Message = "";
            ViewBag.Vehicles = Session["Vehicles"];
            return View("Manage");
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Add                                         ///
        /////////////////////////////////////////////////////////////////////////////////
        public ActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public ActionResult Add(CVehicle vehicle)
        {
            if(ModelState.IsValid)
            {
                CDriver user = (CDriver)Session["Driver"];
                user.AddVehicle(vehicle);
                ViewBag.Message = "Vous avez enregistrez un nouveau véhicule ! (Modèle : " + vehicle.Model + " places : " + vehicle.Seat + " Autorisé à fumer : " + vehicle.CanSmoke+ ")";
                return View("Manage");
            }
            return View();
        }
    }
}