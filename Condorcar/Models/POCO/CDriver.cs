using Condorcar.Models.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace Condorcar.Models.POCO
{
    //[Table("T_CDriver")]
    public class CDriver : CUser
    {
        
       // public short GlobalNote { get; set; }        // Note globale (moyenne) attribué par les passager sur 5 à faire plus tard
        public virtual List<CVehicle> Vehicles { get; set; } // Liste des véhicules du conducteur

        /////////////////////////////////////////////////////////////////////////////////
        ///                               AddVehicle                                  ///
        /////////////////////////////////////////////////////////////////////////////////
        ///       Ajoute un véhicule dans la liste des véhicules de l'utilisateur    ////
        public bool AddVehicle(CVehicle vehicle)
        {
            if (!vehicle.Exist(this))
            {
                this.Vehicles.Add(vehicle); // On ajoute le véhicule à la liste de véhicule de l'utilisateur
                DAL_CUser user = new DAL_CUser();
                user.SaveDriver(this);
                return true;
            }
            else return false;            
        }
    }
}