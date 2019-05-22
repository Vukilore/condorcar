using Condorcar.Models.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Condorcar.Models.POCO
{
    //
    //  Chaque trajet effectué par un conducteur (Driver) contenant des passagers (Passengers) pour son véhicule (Vehicle)
    //  Avec une date de départ, et une heure d'arrivée et un prix
    //
    [Table("T_CRide")]
    public class CRide
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Heure de départ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        public DateTime DepartureTime { get; set; }               // Jour + Heure de départ
        [Required]
        [Display(Name = "Heure estimée d'arriver")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        public DateTime ArrivalTime { get; set; }                  // Jour + Heure d'arrivée
        [Required]
        [Display(Name = "Point de départ")]
        public string Place { get; set; }                          // Lieu de départ
        [Required]
        [Display(Name ="Prix (en Euro)")]
        public float Price { get; set; }                           // Prix de la course

        public virtual CDriver Driver { get; set; }                // Le conducteur du trajet
        [Display(Name = "Véhicule à utiliser")]
        public virtual CVehicle Vehicle { get; set; }              // Le véhicule utilisé durant le trajet (modèle + place disponible)
        [NotMapped]
        public string VehicleId { get; set; }                      // Id du véhicule récupérer dans le formulaire du trajet
        public virtual List<CPassenger> Passengers { get; set; }   // Le(s) passager(s) du trajet



        public CRide() { Passengers = new List<CPassenger>(); }

        
        /////////////////////////////////////////////////////////////////////////////////
        ///                               Delete                                      ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                Supprime un trajet dans la base de données                ////
        public void Delete()
        {
            if (this.Passengers.Count() > 0)
            {
                this.Passengers.Clear();
                DAL_CRide ride = new DAL_CRide();
                ride.SaveRide(this);
            }

        }
        /////////////////////////////////////////////////////////////////////////////////
        ///                               Add                                         ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                Ajoute un trajet dans la base de données                  ////
        public void Add()
        {
            DAL_CRide ride = new DAL_CRide();
            ride.Add(this);
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               AddPassenger                                ///
        /////////////////////////////////////////////////////////////////////////////////
        ///             Ajoute un passagé dans la liste des passagés du trajet       ////
        public void AddPassenger(CPassenger passenger)
        {
            Passengers.Add(passenger);
            DAL_CRide ride = new DAL_CRide();
            ride.SaveRide(this);
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               GetRide                                     ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                    Retourne le trajet en fonction de son ID              ////
        public CRide GetRide(int rideId)
        {
            DAL_CRide ride = new DAL_CRide();
            return ride.Get(rideId);
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               GetAll                                      ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                     Retourne une liste de tous les trajets               ////
        public static List<CRide> GetAll()
        {
            DAL_CRide rides = new DAL_CRide();
            return rides.GetAll();
        }

        public static List<CRide> GetAll(CDriver user)
        {
            DAL_CRide rides = new DAL_CRide();
            return rides.GetAll(user);
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               GetAllOfDay                                 ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                     Retourne une liste de tous les trajets               ////
        public static List<CRide> GetAllOfDay()
        {
            DAL_CRide rides = new DAL_CRide();
            return rides.GetAllOfDay();
        }

        public static List<CRide> GetAllOfDay(CDriver user)
        {
            DAL_CRide rides = new DAL_CRide();
            return rides.GetAllOfDay(user);
        }

    }
}