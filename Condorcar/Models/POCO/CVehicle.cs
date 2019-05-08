using Condorcar.Models.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Condorcar.Models.POCO
{
    [Table("T_CVehicle")]
    public class CVehicle
    {
        public int Id { get; set; }
        [Display(Name = "Nombre de place")]
        public int Seat { get; set; }       // Places maximum 
        [Display(Name = "Modèle de la voiture")]
        public string Model { get; set; }   // Modèle de la voiture
        [Display(Name = "Peut fumer à l'interieur ?")]
        public bool CanSmoke { get; set; }

        public CVehicle(){ }

        public CVehicle(int id, int seat, string model, bool canSmoke)
        {
            Id = id;
            Seat = seat;
            Model = model;
            CanSmoke = canSmoke;
        }

        public void Register()
        {
            DAL_CVehicle veh = new DAL_CVehicle();
            veh.Add(this);
        }
    }
}