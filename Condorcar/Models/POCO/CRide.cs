using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        public DateTime DepartureTime { get; set; }               // Jour + Heure de départ
        public DateTime ArrivalTime { get; set; }                 // Jour + Heure d'arrivée
        public string Place { get; set; }                         // Lieu de départ
        public float Price { get; set; }                          // Prix de la course
        public virtual CDriver Driver { get; set; }               // Le conducteur du trajet
        public virtual CVehicle Vehicle { get; set; }             // Le véhicule utilisé durant le trajet (modèle + place disponible)
        public virtual List<CPassenger> Passengers { get; set; }  // Le(s) passager(s) du trajet

        // Methods: Add, Delete, View, Edit
    }
}