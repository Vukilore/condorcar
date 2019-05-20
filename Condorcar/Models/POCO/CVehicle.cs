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
        [Display(Name = "Autorisé à fumer ?")]
        public bool CanSmoke { get; set; }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Get                                         ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                 Obtient les infos de la base de données                  ////   
        public static CVehicle Get(int id)
        {
            DAL_CVehicle veh = new DAL_CVehicle();
            return veh.Get(id);
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Exist                                       ///
        /////////////////////////////////////////////////////////////////////////////////
        ///  Vérifie si le véhicule n'existe pas déjà dans la liste de l'utilisateur ////
        public bool Exist(CDriver driver)
        {
            var user = (CDriver)CUser.LoadUser(driver.Pseudo); // On charge les variables de l'utilisateur
            if (user.Vehicles != null) // Si il a bien au moins un véhicule dans sa liste
            {
                foreach (CVehicle veh in user.Vehicles) // on parcours tous ses véhicules
                {
                    if (this.Model == veh.Model && this.Seat == veh.Seat) // Si le véhicule existe déjà on retourne true
                        return true;
                }
            }
            return false; // Si il arrive ici c'est qu'il n'a pas de véhicule ou aucun correspond
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               BoolToYesNo                                 ///
        /////////////////////////////////////////////////////////////////////////////////
        ///                 Obtient les infos de la base de données                  ////   
        public string BoolToYesNo()
        {
            if (CanSmoke) return "Oui";
            else return "Non";
        }
    }
}