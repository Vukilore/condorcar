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
            if (!(Session["User"] is CDriver)) return Redirect("../Home/Index");
            Session["persoRides"] = CRide.GetAllOfDay((CDriver)Session["User"]); ;
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               History                                     ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                 Affiche la liste des trajets du véhicule                 ////
        public ActionResult AllPersonalRide()
        {
            if (!(Session["User"] is CDriver)) return Redirect("../Home/Index");
            Session["persoRides"] = CRide.GetAll((CDriver)Session["User"]); ;
            return View("Index");
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
            if (!(Session["User"] is CDriver)) return Redirect("../Home/Index");
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
            if (!(Session["User"] is CDriver)) return Redirect("../Home/Index");
            try
            {
                try
                {
                    ride.Vehicle = CVehicle.GetVehicle(Int32.Parse(ride.VehicleId));
                }
                catch (FormatException)
                {
                    throw new Exception("Impossible de convertir l'ID du véhicule. Est-il correcte ?");
                }
                CDriver driver = new CDriver();
                driver = (CDriver)Session["User"];    // On charge les variables du conducteur dans user
                ride.Driver = driver; 
                ride.AddRide();
                Session["persoRides"] = CRide.GetAll((CDriver)Session["User"]); ;
                ViewBag.Message = "Vous avez bien enregistré un nouveau trajet";
                return View("Index");
            }
            catch
            {
                return View();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               History                                     ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                     Affiche la liste de tous les trajets                 ////
        public ActionResult History()
        {
            if (!(Session["User"] is CPassenger)) return Redirect("../Home/Index");
            CPassenger user = new CPassenger();
            user = (CPassenger)Session["User"];
            Session["smoker"] = user.Smoker;
            Session["userRideList"] = user.RideList;
            Session["rideList"] = CRide.GetAll();
            return View("List");
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               List                                        ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                     Affiche la liste de tous les trajets d'ajd           ////
        public ActionResult List()
        {
            if (!(Session["User"] is CPassenger)) return Redirect("../Home/Index");
            CPassenger user = new CPassenger();
            user = (CPassenger)Session["User"];
            Session["smoker"] = user.Smoker;
            Session["userRideList"] = user.RideList;
            Session["rideList"] = CRide.GetAllOfDay();
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               UnSubscribe                                 ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                     Retire un passager à un trajet                       ////
        public ActionResult UnSubscribe(int idRide)
        {
            if (!(Session["User"] is CPassenger)) return Redirect("../Home/Index");
            CPassenger passenger = (CPassenger)Session["User"];
            CRide ride = new CRide();
            ride = ride.GetRide(idRide);
            passenger.RemoveToRide(ride);
            Session["User"] = passenger;
            ViewBag.Message = "Vous vous êtes bien Désinscris du trajet !";
            Session["smoker"] = passenger.Smoker;
            Session["userRideList"] = passenger.RideList;
            Session["rideList"] = CRide.GetAll();
            return View("List");
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Subscribe                                   ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                     Inscrit un passager à un trajet                      ////
        public ActionResult Subscribe(int idRide)
        {
            if (!(Session["User"] is CPassenger)) return Redirect("../Home/Index");
            CRide ride = new CRide();
            ride = ride.GetRide(idRide);
            if (ride.Vehicle.Seat > ride.Passengers.Count()) // Si la liste des passager n'est pas complet 
            {
               if(DateTime.Compare(ride.ArrivalTime.Date, DateTime.Now) < 0 )
               {
                    if (DateTime.Compare(ride.DepartureTime.Date, DateTime.Now.Date.AddHours(1)) < 0 )
                    {
                        CPassenger passenger = (CPassenger)Session["User"];
                        passenger.AddToRide(ride);
                        Session["User"] = passenger;
                        ViewBag.Message ="Vous vous êtes bien inscrit au trajet ! ID: " + idRide + "()" +ride.Id;
                        Session["smoker"] = passenger.Smoker;
                        Session["userRideList"] = passenger.RideList;
                        Session["rideList"] = CRide.GetAll();
                        return View("List");
                    }
                    else
                    {
                        ViewBag.Message = "Trop tard ! Le départ est déjà en cours ou en préparation !";
                        Session["rideList"] = CRide.GetAll();
                        return View("List");
                    }
               }
               else // Trajet est déjà fini
               {
                    ViewBag.Message = "Trop tard ! Le trajet est déjà fini !";
                    Session["rideList"] = CRide.GetAll();// TODO: catalogue
                    return View("List");
               }
            }
            else // Sinon le nombre de place est atteins !
            {
                ViewBag.Message = "Nombre de place maximum atteinte !";
                Session["rideList"] = CRide.GetAll();// TODO: catalogue
                return View("List");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Delete                                      ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                 Supprime un trajet dans la liste des trajets             ////
        // POST: Ride/Delete/5
        public ActionResult Delete(int id)
        {
            if (!(Session["User"] is CDriver)) return Redirect("../Home/Index");
            CRide ride = new CRide();
            ride = ride.GetRide(id);
            ride.DeleteRide();  
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
